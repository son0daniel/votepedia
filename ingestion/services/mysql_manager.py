import logging
import math
from typing import Optional
from dotenv import dotenv_values
import mysql.connector
import pandas as pd
from services.tse_csv_file_handler import TseCsvFileHandler
import utils.constants as consts
import uuid

logger = logging.getLogger("mysql")

class MySqlManager:
    def __init__(self):
        self.config = dotenv_values("settings/.db_credentials.env")
        

    def __enter__(self):
        self.conn = mysql.connector.connect(
            host=self.config.get("DB_HOST"),
            user=self.config.get("DB_USER"),
            password=self.config.get("DB_USER_PASSWORD"),
            database=self.config.get("DB")
        )
        return self

    def select_one(self, query: str, params: list | None = None):
            cursor = self.conn.cursor()
            try:
                cursor.execute(query, params or ())
                return cursor.fetchone()
            finally:
                cursor.close()

    def select_all(self, query: str, params: list | None = None):
        cursor = self.conn.cursor()
        try:
            cursor.execute(query, params or ())
            return cursor.fetchall()
        finally:
            cursor.close()

    def batch_insert(self, query: str, entities: list[tuple], batch_size = 1000):
        cursor = self.conn.cursor()

        qt_registries = len(entities)
        total_batches = math.ceil(qt_registries / batch_size)
        batch = 1

        logger.info(f"Preparing to insert {qt_registries} registries across {total_batches} batches")

        for i in range(0, qt_registries, batch_size):
            logger.info(f"Inserting batch {batch} of {total_batches} batches")
            cursor.executemany(query, entities[i:i + batch_size])
            self.conn.commit()
            batch += 1

        cursor.close()

    def get_election_round_id_by_params(self, election_year: int, nr_round: int, role: str) -> Optional[int]:
        role = role.upper()

        query = f"""
            SELECT 
                er.id 
            FROM election_round er
            INNER JOIN election e ON er.election_id = e.id
            WHERE er.nr_round = {nr_round}
            AND e.year = {election_year} 
            AND e.role = '{role}'
        """

        return self.select_one(query)[0]

    def get_tickets_main_candidates_from_election_round(self, election_round_id: int):
        query = f"""
        SELECT
            t.id,
            c.tse_name, 
            c.birth_date
        FROM ticket_election_round ter
        INNER JOIN election_round er ON ter.election_round_id = er.id
        INNER JOIN ticket t ON ter.ticket_id = t.id
        INNER JOIN ticket_candidate_party tcp ON t.id = tcp.ticket_id
        INNER JOIN candidate_party cp ON tcp.candidate_party_id = cp.id
        INNER JOIN candidate c ON cp.candidate_id = c.id
        WHERE er.id = {election_round_id}
        AND tcp.nr_level = 1;
        """

        candidates = self.select_all(query)

        return {name: (ticket_id, birth_date) for ticket_id, name, birth_date in candidates}

    def get_macroregion_ids_by_state_ids(self, state_ids: list[int]):
        if len(state_ids) == 0:
            logger.warning("State id list is empty, aborting state query")
            return {}
        
        placeholder = ", ".join(["%s"] * len(state_ids))
                
        query = f"""
            SELECT 
                id, 
                macroregion_id 
            FROM state
            WHERE id IN ({placeholder})
        """

        state_ids_with_macroregion_ids = self.select_all(query, state_ids)

        return {state_id: macroregion_id for state_id, macroregion_id in state_ids_with_macroregion_ids}

    def get_complete_location_by_tse_ids(self, tse_ids: list[int]):
        if len(tse_ids) == 0:
            logger.warning("TSE id list is empty, aborting complete location query")
            return {}
        
        placeholder = ", ".join(["%s"] * len(tse_ids))
        
        query = f"""
            SELECT 
                tse_id, 
                id, 
                metropolitan_area_id, 
                state_id 
            FROM municipality
            WHERE tse_id IN ({placeholder})
        """

        municipalities = self.select_all(query, tse_ids)

        return {tse_id: (municipality_id, metropolitan_area_id, state_id) for tse_id, municipality_id, metropolitan_area_id, state_id in municipalities}

    def get_existing_municipalities_tse_ids(self):
        query = """
            SELECT tse_id FROM municipality
        """

        ids = self.select_all(query)

        return [row[0] for row in ids]

    def get_states_by_abbr_list(self, abbr_list: list[str]):
        if len(abbr_list) == 0:
            logger.warning("State abbr list is empty, aborting state query")
            return {}

        placeholder = ", ".join(["%s"] * len(abbr_list))
        
        query = f"""
            SELECT id, abbr FROM state
            WHERE abbr in ({placeholder})
        """

        states = self.select_all(query, abbr_list)

        return { abbr: id_ for id_, abbr in states}

    def get_metropolitan_areas_by_name_list(self, metropolitan_area_names_list: list[str]):
        if len(metropolitan_area_names_list) == 0:
            logger.warning("Metropolitan area name list is empty, aborting metropolitan area query")
            return {}

        placeholder = ", ".join(["%s"] * len(metropolitan_area_names_list))
        
        query = f"""
            SELECT id, name FROM metropolitan_area
            WHERE name in ({placeholder})
        """

        metropolitan_areas = self.select_all(query, metropolitan_area_names_list)

        return { name: id_ for id_, name in metropolitan_areas}

    def insert_municipalities(self, df_municipalities_to_add: pd.DataFrame):
        df_municipalities_to_add = df_municipalities_to_add[[consts.DF_TSE_ID_COLUMN, consts.DF_IBGE_ID_COLUMN, consts.DF_MUNICIPALTY_NAME_COLUMN, consts.DF_METROPOLITAN_AREA_ID_COLUMN, consts.DF_STATE_ID_COLUMN, consts.DF_IS_CAPITAL_COLUMN]]

        municipalities_to_add = [
            (str(uuid.uuid4()), int(tse_id), int(ibge_id) if pd.notna(ibge_id) else None, str(name), int(metro_id) if pd.notna(metro_id) else None, int(state_id), bool(is_capital)) for tse_id, ibge_id, name, metro_id, state_id, is_capital in df_municipalities_to_add.itertuples(index=False, name=None)
        ]

        query = """
            INSERT INTO municipality (uid, tse_id, ibge_id, name, metropolitan_area_id, state_id, is_capital)
            VALUES (%s, %s, %s, %s, %s, %s, %s)
        """

        self.batch_insert(query, municipalities_to_add)

    def insert_ticket_election_round_statistics(self, df_statistics: pd.DataFrame, _election_round_id: int):
        _candidates_count = len(TseCsvFileHandler.get_list_by_df_column(df_statistics, consts.DF_TICKET_ID_COLUMN))

        df_statistics[consts.DF_ELECTION_ROUND_ID_COLUMN] = _election_round_id
        df_statistics[consts.DF_QT_CANDIDATES_COLUMN] = _candidates_count

        df_statistics = df_statistics[
            [
                consts.DF_TICKET_ID_COLUMN,
                consts.DF_QT_VOTES_COLUMN,
                consts.DF_NR_POSITION_COLUMN,
                consts.DF_TICKET_STATUS_COLUMN,

                consts.DF_ELECTION_ROUND_ID_COLUMN,

                consts.DF_MUNICIPALTY_ID_COLUMN,
                consts.DF_METROPOLITAN_AREA_ID_COLUMN,
                consts.DF_STATE_ID_COLUMN,
                consts.DF_MACROREGION_ID_COLUMN,
                
                consts.DF_QT_REGISTERED_VOTERS_COLUMN,
                consts.DF_QT_TURNOUT_COLUMN,
                consts.DF_QT_ABSTENTION_COLUMN,
                consts.DF_QT_ELECTORAL_ALIENATION_COLUMN,

                consts.DF_QT_VALID_VOTES_COLUMN,
                consts.DF_INVALID_VOTES_COLUMN,
                consts.DF_QT_NULL_VOTES_COLUMN,
                consts.DF_QT_BLANK_VOTES_COLUMN,
                consts.DF_QT_ANULLED_VOTES_COLUMN,
                
                consts.DF_PP_TICKET_VALID_VOTES_COLUMN,
                consts.DF_PP_TICKET_TOTAL_VOTES_COLUMN,
                consts.DF_PP_TICKET_REGISTERED_VOTES_COLUMN,
                
                consts.DF_PP_TURNOUT_COLUMN,
                consts.DF_PP_ABSTENTION_COLUMN,
                consts.DF_PP_ELECTORAL_ALIENATION_COLUMN,
                
                consts.DF_PP_VALID_VOTES_COLUMN,
                consts.DF_PP_VALID_VOTES_REGISTERED_COLUMN,
                consts.DF_PP_INVALID_VOTES_COLUMN,
                consts.DF_PP_INVALID_VOTES_REGISTERED_COLUMN,
                consts.DF_PP_NULL_VOTES_COLUMN,
                consts.DF_PP_NULL_VOTES_REGISTERED_COLUMN,
                consts.DF_PP_BLANK_VOTES_COLUMN,
                consts.DF_PP_BLANK_VOTES_REGISTERED_COLUMN,
                consts.DF_PP_ANULLED_VOTES_COLUMN,
                consts.DF_PP_ANULLED_VOTES_REGISTERED_COLUMN,

                consts.DF_QT_CANDIDATES_COLUMN
            ]
        ]

        ticket_election_round_statistics = [
            (
                int(ticket_id),
                qt_votes,
                nr_position,
                ticket_status,

                election_round_id,

                int(municipality_id) if pd.notna(municipality_id) else None,
                int(metropolitan_area_id) if pd.notna(metropolitan_area_id) else None,
                int(state_id) if pd.notna(state_id) else None,
                int(macroregion_id) if pd.notna(macroregion_id) else None,

                qt_registered_voters,
                qt_turnout,
                qt_abstention,
                qt_electoral_alienation,

                qt_valid_votes,
                qt_invalid_votes,
                qt_null_votes,
                qt_blank_votes,
                qt_anulled_votes,

                pp_ticket_valid_votes if pd.notna(pp_ticket_valid_votes) else None,
                pp_ticket_total_votes if pd.notna(pp_ticket_total_votes) else None,
                pp_ticket_registered_votes if pd.notna(pp_ticket_registered_votes) else None,

                pp_turnout,
                pp_abstention,
                pp_electoral_alienation,

                pp_valid_votes if pd.notna(pp_valid_votes) else None,
                pp_valid_votes_registered,
                pp_invalid_votes if pd.notna(pp_invalid_votes) else None,
                pp_invalid_votes_registered,
                pp_null_votes if pd.notna(pp_null_votes) else None,
                pp_null_votes_registered,
                pp_blank_votes if pd.notna(pp_blank_votes) else None,
                pp_blank_votes_registered,
                pp_anulled_votes if pd.notna(pp_anulled_votes) else None,
                pp_anulled_votes_registered,

                qt_candidates
            )

            for
            ticket_id,
            qt_votes,
            nr_position,
            ticket_status,

            election_round_id,

            municipality_id,
            metropolitan_area_id,
            state_id,
            macroregion_id,

            qt_registered_voters,
            qt_turnout,
            qt_abstention,
            qt_electoral_alienation,

            qt_valid_votes,
            qt_invalid_votes,
            qt_null_votes,
            qt_blank_votes,
            qt_anulled_votes,

            pp_ticket_valid_votes,
            pp_ticket_total_votes,
            pp_ticket_registered_votes,

            pp_turnout,
            pp_abstention,
            pp_electoral_alienation,

            pp_valid_votes,
            pp_valid_votes_registered,
            pp_invalid_votes,
            pp_invalid_votes_registered,
            pp_null_votes,
            pp_null_votes_registered,
            pp_blank_votes,
            pp_blank_votes_registered,
            pp_anulled_votes,
            pp_anulled_votes_registered,

            qt_candidates
            in df_statistics.itertuples(index=False, name=None)
        ]

        placeholder = ", ".join(["%s"] * len(df_statistics.columns))
        print(len(df_statistics.columns))
        query = f"""
        INSERT INTO ticket_election_round_statistic 
        (
            ticket_id, 
            ticket_votes_count, 
            ticket_position_nr, 
            ticket_status, 
            
            election_round_id, 

            municipality_id, 
            metropolitan_area_id, 
            state_id, 
            macroregion_id,

            registered_voters_count, 
            turnout_count, 
            abstention_count, 
            electoral_alienation_count, 
            
            valid_votes_count, 
            invalid_votes_count, 
            null_votes_count, 
            blank_votes_count, 
            anulled_votes_count, 
            
            ticket_valid_votes_pp, 
            ticket_total_votes_pp, 
            ticket_registered_votes_pp, 
            
            turnout_pp, 
            abstention_pp, 
            electoral_alienation_pp,

            valid_votes_pp, 
            valid_votes_registered_pp, 
            invalid_votes_pp, 
            invalid_votes_registered_pp, 
            null_votes_pp, 
            null_votes_registered_pp, 
            blank_votes_pp, 
            blank_votes_registered_pp, 
            anulled_votes_pp, 
            anulled_votes_registered_pp,

            candidates_count
        )
        VALUES ({placeholder})
        """

        self.batch_insert(query, ticket_election_round_statistics)

    def __exit__(self, exc_type, exc_val, exc_tb):
        self.conn.close()