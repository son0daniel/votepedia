import logging
import math
from dotenv import dotenv_values
import mysql.connector
import pandas as pd
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

    def __exit__(self, exc_type, exc_val, exc_tb):
        self.conn.close()