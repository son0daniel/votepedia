

import logging
import pandas as pd
import numpy as np

from services.mysql_manager import MySqlManager
from services.tse_csv_file_handler import TseCsvFileHandler
from utils.aggregation_level import AggregationLevel
from utils.get_df_aggregation_level_columns import get_df_aggregation_level_columns
from utils.get_null_or_division import get_null_or_division
from utils.load_election_context import load_election_context
from utils.load_logging import configure_logging
import utils.constants as consts


if __name__ == "__main__":
    configure_logging()
    logger = logging.getLogger("municipality_ingestor")

    (election_year, nr_round, role) = load_election_context()

    with MySqlManager() as sql:
        election_round_id = sql.get_election_round_id_by_params(election_year, nr_round, role)

    if not election_round_id:
        logger.error(f"No election round with year {election_year}, round {nr_round} and role {role}")
        raise ValueError

    df_service = TseCsvFileHandler(election_year, nr_round, role)

    df_municipalities_votes = TseCsvFileHandler.group_votes_by_municipality(df_service.df)
    df_municipalities_votes = TseCsvFileHandler.normalize_candidate_names(df_municipalities_votes)
    df_municipalities_registered_voters = TseCsvFileHandler.group_registered_voters_by_municipality(df_service.df)

    tse_ids = TseCsvFileHandler.get_df_tse_ids(df_municipalities_votes)
    
    with MySqlManager() as sql:
        candidates_infos = sql.get_tickets_main_candidates_from_election_round(election_round_id)
        municipalities_infos = sql.get_complete_location_by_tse_ids(tse_ids)

        df_municipalities_votes[consts.DF_MUNICIPALTY_ID_COLUMN] = (df_municipalities_votes[consts.DF_TSE_ID_COLUMN].map(municipalities_infos).str[0])
        df_municipalities_votes[consts.DF_METROPOLITAN_AREA_ID_COLUMN] = (df_municipalities_votes[consts.DF_TSE_ID_COLUMN].map(municipalities_infos).str[1])
        df_municipalities_votes[consts.DF_STATE_ID_COLUMN] = (df_municipalities_votes[consts.DF_TSE_ID_COLUMN].map(municipalities_infos).str[2])

        df_municipalities_registered_voters[consts.DF_MUNICIPALTY_ID_COLUMN] = (df_municipalities_registered_voters[consts.DF_TSE_ID_COLUMN].map(municipalities_infos).str[0])
        df_municipalities_registered_voters[consts.DF_METROPOLITAN_AREA_ID_COLUMN] = (df_municipalities_registered_voters[consts.DF_TSE_ID_COLUMN].map(municipalities_infos).str[1])
        df_municipalities_registered_voters[consts.DF_STATE_ID_COLUMN] = (df_municipalities_registered_voters[consts.DF_TSE_ID_COLUMN].map(municipalities_infos).str[2])

        states_ids = TseCsvFileHandler.get_df_states_ids(df_municipalities_votes)
        states_infos = sql.get_macroregion_ids_by_state_ids(states_ids)

        df_municipalities_votes[consts.DF_MACROREGION_ID_COLUMN] = (df_municipalities_votes[consts.DF_STATE_ID_COLUMN].map(states_infos))

        df_municipalities_registered_voters[consts.DF_MACROREGION_ID_COLUMN] = (df_municipalities_registered_voters[consts.DF_STATE_ID_COLUMN].map(states_infos))

        df_municipalities_votes[consts.DF_TICKET_ID_COLUMN] = (df_municipalities_votes[consts.DF_CANDIDATE_NAME_COLUMN].map(candidates_infos).str[0])
        df_municipalities_votes[consts.DF_CANDIDATE_BIRTH_DATE_COLUMN] = (df_municipalities_votes[consts.DF_CANDIDATE_NAME_COLUMN].map(candidates_infos).str[1])

    df_municipalities_tickets_votes = df_municipalities_votes[~df_municipalities_votes[consts.DF_CANDIDATE_NAME_COLUMN].isin(consts.DF_INVALID_VOTES)]
    df_municipalities_valid_votes = TseCsvFileHandler.group_votes_by_aggregation_level(df_municipalities_tickets_votes, get_df_aggregation_level_columns(AggregationLevel.MUNICIPALITY), by_candidate=False)
    df_municipalities_null_votes = df_municipalities_votes[df_municipalities_votes[consts.DF_CANDIDATE_NAME_COLUMN] == consts.DF_NULL_VOTES_NAME]
    df_municipalities_blank_votes = df_municipalities_votes[df_municipalities_votes[consts.DF_CANDIDATE_NAME_COLUMN] == consts.DF_BLANK_VOTES_NAME]
    df_municipalities_anulled_votes = df_municipalities_votes[df_municipalities_votes[consts.DF_CANDIDATE_NAME_COLUMN] == consts.DF_ANULLED_VOTES_NAME]

    dict_registered_voters = {}
    dict_valid_votes = {}
    dict_null_votes = {}
    dict_blank_votes = {}
    dict_anulled_votes = {}

    ticket_election_round_municipality_statistics: list[pd.DataFrame] = []

    for level in AggregationLevel:
        location_columns = get_df_aggregation_level_columns(level)

        dict_registered_voters = TseCsvFileHandler.get_grouped_by_location_df_registered_voters_as_dict(location_columns, df_municipalities_registered_voters, dict_registered_voters)
        dict_valid_votes = TseCsvFileHandler.get_grouped_by_location_df_votes_as_dict(location_columns, df_municipalities_valid_votes, dict_valid_votes)
        dict_null_votes = TseCsvFileHandler.get_grouped_by_location_df_votes_as_dict(location_columns, df_municipalities_null_votes, dict_null_votes)
        dict_blank_votes = TseCsvFileHandler.get_grouped_by_location_df_votes_as_dict(location_columns, df_municipalities_blank_votes, dict_blank_votes)
        dict_anulled_votes = TseCsvFileHandler.get_grouped_by_location_df_votes_as_dict(location_columns, df_municipalities_anulled_votes, dict_anulled_votes)

        grouped_df_as_dict_list = []

        for [*location, ticket_id, birth_date], df in df_municipalities_tickets_votes.groupby([*location_columns, consts.DF_TICKET_ID_COLUMN, consts.DF_CANDIDATE_BIRTH_DATE_COLUMN], dropna=level == AggregationLevel.METROPOLITAN_AREA):
            location_dict = dict(zip(location_columns, location))
            key = tuple(None if pd.isna(x) else x for x in location) if len(location_columns) > 0 else None

            try:
                qt_registered_voters = dict_registered_voters[key]
            except KeyError:
                logger.exception(f"Couldn't find location at {key} with level {level.name}")
                raise
            
            qt_votes = df[consts.DF_QT_VOTES_COLUMN].sum()
            qt_valid_votes = dict_valid_votes.get(key, 0)
            qt_null_votes = dict_null_votes.get(key, 0)
            qt_blank_votes = dict_blank_votes.get(key, 0)
            qt_anulled_votes = dict_anulled_votes.get(key, 0)
            qt_invalid_votes = qt_null_votes + qt_blank_votes + qt_anulled_votes
            qt_turnout = qt_valid_votes + qt_invalid_votes
            qt_abstention = qt_registered_voters - qt_turnout
            qt_electoral_alienation = qt_invalid_votes + qt_abstention

            pp_ticket_valid_votes = get_null_or_division(qt_votes, qt_valid_votes)
            pp_ticket_total_votes = get_null_or_division(qt_votes, qt_turnout)
            pp_ticket_registered_votes = qt_votes / qt_registered_voters

            pp_turnout = qt_turnout / qt_registered_voters
            pp_abstention = qt_abstention / qt_registered_voters
            pp_electoral_alienation = qt_electoral_alienation / qt_registered_voters

            pp_valid_votes = get_null_or_division(qt_valid_votes, qt_turnout)
            pp_valid_votes_registered = qt_valid_votes / qt_registered_voters
            pp_invalid_votes = get_null_or_division(qt_invalid_votes, qt_turnout)
            pp_invalid_votes_registered = qt_invalid_votes / qt_registered_voters
            pp_null_votes = get_null_or_division(qt_null_votes, qt_turnout)
            pp_null_votes_registered = qt_null_votes / qt_registered_voters
            pp_blank_votes = get_null_or_division(qt_blank_votes, qt_turnout)
            pp_blank_votes_registered = qt_blank_votes / qt_registered_voters
            pp_anulled_votes = get_null_or_division(qt_anulled_votes, qt_turnout)
            pp_anulled_votes_registered = qt_anulled_votes / qt_registered_voters

            grouped_df_as_dict = {
                consts.DF_MUNICIPALTY_ID_COLUMN: location_dict.get(consts.DF_MUNICIPALTY_ID_COLUMN, np.nan),
                consts.DF_METROPOLITAN_AREA_ID_COLUMN: location_dict.get(consts.DF_METROPOLITAN_AREA_ID_COLUMN, np.nan),
                consts.DF_STATE_ID_COLUMN: location_dict.get(consts.DF_STATE_ID_COLUMN, np.nan),
                consts.DF_MACROREGION_ID_COLUMN: location_dict.get(consts.DF_MACROREGION_ID_COLUMN, np.nan),
                consts.DF_TICKET_ID_COLUMN: ticket_id,
                consts.DF_CANDIDATE_BIRTH_DATE_COLUMN: birth_date,
                consts.DF_QT_VOTES_COLUMN: qt_votes,
                consts.DF_QT_REGISTERED_VOTERS_COLUMN: qt_registered_voters,
                consts.DF_QT_TURNOUT_COLUMN: qt_turnout,
                consts.DF_QT_ABSTENTION_COLUMN: qt_abstention,
                consts.DF_QT_ELECTORAL_ALIENATION_COLUMN: qt_electoral_alienation,
                consts.DF_QT_VALID_VOTES_COLUMN: qt_valid_votes,
                consts.DF_INVALID_VOTES_COLUMN: qt_invalid_votes,
                consts.DF_QT_NULL_VOTES_COLUMN: qt_null_votes,
                consts.DF_QT_BLANK_VOTES_COLUMN: qt_blank_votes,
                consts.DF_QT_ANULLED_VOTES_COLUMN: qt_anulled_votes,
                consts.DF_PP_TICKET_VALID_VOTES_COLUMN: pp_ticket_valid_votes,
                consts.DF_PP_TICKET_TOTAL_VOTES_COLUMN: pp_ticket_total_votes,
                consts.DF_PP_TICKET_REGISTERED_VOTES_COLUMN: pp_ticket_registered_votes,
                consts.DF_PP_TURNOUT_COLUMN: pp_turnout,
                consts.DF_PP_ABSTENTION_COLUMN: pp_abstention,
                consts.DF_PP_ELECTORAL_ALIENATION_COLUMN: pp_electoral_alienation,
                consts.DF_PP_VALID_VOTES_COLUMN: pp_valid_votes,
                consts.DF_PP_VALID_VOTES_REGISTERED_COLUMN: pp_valid_votes_registered,
                consts.DF_PP_INVALID_VOTES_COLUMN: pp_invalid_votes,
                consts.DF_PP_INVALID_VOTES_REGISTERED_COLUMN: pp_invalid_votes_registered,
                consts.DF_PP_NULL_VOTES_COLUMN: pp_null_votes,
                consts.DF_PP_NULL_VOTES_REGISTERED_COLUMN: pp_null_votes_registered,
                consts.DF_PP_BLANK_VOTES_COLUMN: pp_blank_votes,
                consts.DF_PP_BLANK_VOTES_REGISTERED_COLUMN: pp_blank_votes_registered,
                consts.DF_PP_ANULLED_VOTES_COLUMN: pp_anulled_votes,
                consts.DF_PP_ANULLED_VOTES_REGISTERED_COLUMN: pp_anulled_votes_registered
            }

            grouped_df_as_dict_list.append(grouped_df_as_dict)

        grouped_dict_list_as_df = pd.DataFrame(grouped_df_as_dict_list)

        grouped_dict_list_as_df = grouped_dict_list_as_df.sort_values(by=[consts.DF_QT_VOTES_COLUMN, consts.DF_CANDIDATE_BIRTH_DATE_COLUMN], ascending=[False, True]).reset_index(drop=True)

        if len(location_columns) > 0:
            grouped_dict_list_as_df[consts.DF_NR_POSITION_COLUMN] = (
                grouped_dict_list_as_df
                .groupby(location_columns, dropna=False)
                .cumcount()
                + 1
            )
        else:
            grouped_dict_list_as_df[consts.DF_NR_POSITION_COLUMN] = grouped_dict_list_as_df.index + 1

        ticket_election_round_municipality_statistics.append(grouped_dict_list_as_df)

    df_ticket_election_round_municipality_statistics = pd.concat(ticket_election_round_municipality_statistics, ignore_index=True)

    df_ticket_election_round_overall_statistics = df_ticket_election_round_municipality_statistics[df_ticket_election_round_municipality_statistics[consts.DF_MUNICIPALTY_ID_COLUMN].isna() & df_ticket_election_round_municipality_statistics[consts.DF_METROPOLITAN_AREA_ID_COLUMN].isna() & df_ticket_election_round_municipality_statistics[consts.DF_STATE_ID_COLUMN].isna() & df_ticket_election_round_municipality_statistics[consts.DF_MACROREGION_ID_COLUMN].isna()].reset_index(drop=True)

    biggest_valid_vote_percentage = df_ticket_election_round_overall_statistics[consts.DF_PP_TICKET_VALID_VOTES_COLUMN].max()
    df_ticket_election_round_overall_statistics[consts.DF_TICKET_STATUS_COLUMN] = np.select(
        [
            df_ticket_election_round_overall_statistics[
                consts.DF_PP_TICKET_VALID_VOTES_COLUMN
            ] > 0.5,

            (df_ticket_election_round_overall_statistics.index <= 1)
            & (biggest_valid_vote_percentage <= 0.5),
        ],
        [
            consts.ELECTED,
            consts.RUNOFF,
        ],
        default=consts.NOT_ELECTED,
    )
    status_by_ticket = df_ticket_election_round_overall_statistics[[consts.DF_TICKET_ID_COLUMN, consts.DF_TICKET_STATUS_COLUMN]].drop_duplicates().set_index(consts.DF_TICKET_ID_COLUMN)[consts.DF_TICKET_STATUS_COLUMN].to_dict()

    df_ticket_election_round_municipality_statistics[consts.DF_TICKET_STATUS_COLUMN] = df_ticket_election_round_municipality_statistics[consts.DF_TICKET_ID_COLUMN].map(status_by_ticket)

    with MySqlManager() as sql:
        sql.insert_ticket_election_round_statistics(df_ticket_election_round_municipality_statistics, election_round_id)