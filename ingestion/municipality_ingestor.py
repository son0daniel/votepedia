from services.brazil_gdf_as_dataframe import BrazilGdfAsDataframe
from services.mysql_manager import MySqlManager
from services.tse_csv_file_handler import TseCsvFileHandler
from utils.load_election_context import load_election_context
import utils.constants as consts
import logging
from utils.load_logging import configure_logging


if __name__ == "__main__":
    configure_logging()
    logger = logging.getLogger("municipality_ingestor")

    (election_year, nr_round, role) = load_election_context()
    
    df_service = TseCsvFileHandler(election_year, nr_round, role)

    df_municipalities = df_service.get_municipalities_df()

    with MySqlManager() as sql:
        existing_municipalities_tse_ids = sql.get_existing_municipalities_tse_ids()
        logger.info(f"Found {len(existing_municipalities_tse_ids)} existing municipality registries")
        df_municipalities_to_add = df_municipalities[~df_municipalities[consts.DF_TSE_ID_COLUMN].isin(existing_municipalities_tse_ids)]
        logger.info(f"{len(df_municipalities_to_add)} municipality registries to be inserted")

        if df_municipalities_to_add.any:
            gdf_service = BrazilGdfAsDataframe()
            df_municipalities_to_add = TseCsvFileHandler.merge_df_and_previously_gdf(df_municipalities_to_add, gdf_service.df)

            states = TseCsvFileHandler.get_df_states(df_municipalities_to_add)
            metropolitan_areas = TseCsvFileHandler.get_df_metropolitan_areas(df_municipalities_to_add)

            state_id_by_abbr = sql.get_states_by_abbr_list(states)
            metropolitan_area_id_by_name = sql.get_metropolitan_areas_by_name_list(metropolitan_areas)

            df_municipalities_to_add[consts.DF_STATE_ID_COLUMN] = df_municipalities_to_add[consts.DF_STATE_ABBR_COLUMN].map(state_id_by_abbr)
            df_municipalities_to_add[consts.DF_METROPOLITAN_AREA_ID_COLUMN] = df_municipalities_to_add[consts.DF_METROPOLITAN_AREA_NAME_COLUMN].map(metropolitan_area_id_by_name)
            df_municipalities_to_add[consts.DF_IS_CAPITAL_COLUMN] = df_municipalities_to_add[consts.DF_IBGE_ID_COLUMN].fillna(-1).astype(int).isin(consts.CAPITAL_IBGE_IDS)
            sql.insert_municipalities(df_municipalities_to_add)