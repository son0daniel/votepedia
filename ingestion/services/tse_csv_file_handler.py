import pandas as pd
import logging
import utils.constants as consts
from utils.get_df_aggregation_level_columns import get_df_aggregation_level_columns

logger = logging.getLogger("tse_csv_file_handler")

class TseCsvFileHandler:
    def __init__(self, year: int, nr_round: int, role: str):
        role = role.lower()

        path = f"src/{year}/{role}/{nr_round}º turno/votacao_secao-{role}_t{nr_round}_{year}.csv"

        self.df = self.load_dataframe(path)

    def load_dataframe(self, path: str) -> pd.DataFrame:
        try:
            logger.info(f"Preparing to load TSE election result file {path}")
            df = pd.read_csv(path, delimiter=";", encoding="iso-8859-1")
            logger.info("Dataframe loaded successfully")
            return df
        except FileNotFoundError:
            logger.exception("TSE result file with path %s not found", path)
            raise

    def get_municipalities_df(self) -> pd.DataFrame:
        df_municipalities = self.df[[consts.DF_TSE_ID_COLUMN, consts.DF_MUNICIPALTY_NAME_COLUMN, consts.DF_STATE_ABBR_COLUMN]].drop_duplicates()

        return TseCsvFileHandler.df_municipality_name_normalization_and_correction(df_municipalities)
        
    @staticmethod
    def group_votes_by_municipality(df: pd.DataFrame):
        candidates = TseCsvFileHandler.get_candidates_as_series(df).unique()
        tse_ids = TseCsvFileHandler.get_tse_ids_as_series(df).unique()

        complete_index = pd.MultiIndex.from_product(
            [candidates, tse_ids], names=[consts.DF_CANDIDATE_NAME_COLUMN, consts.DF_TSE_ID_COLUMN]
        )

        grouped_df = TseCsvFileHandler.group_votes_by_aggregation_level(df, [consts.DF_TSE_ID_COLUMN], by_candidate=True)
        return (
            grouped_df.set_index([consts.DF_CANDIDATE_NAME_COLUMN, consts.DF_TSE_ID_COLUMN])
            .reindex(complete_index, fill_value=0)
            .reset_index()
        )

    @staticmethod
    def group_registered_voters_by_municipality(df: pd.DataFrame):
        return TseCsvFileHandler.group_registered_voters_by_aggregation_level(df, consts.DF_TSE_ID_COLUMN)    
    
    @staticmethod
    def group_votes_by_aggregation_level(df: pd.DataFrame, level_columns: list[str], by_candidate: bool):
        if by_candidate:
            level_columns.append(consts.DF_CANDIDATE_NAME_COLUMN)

        return df.groupby(level_columns, dropna=False)[consts.DF_QT_VOTES_COLUMN].sum().reset_index()

    @staticmethod
    def group_registered_voters_by_aggregation_level(df: pd.DataFrame, level_column: str):
        return df.groupby([level_column, consts.DF_CANDIDATE_NAME_COLUMN])[consts.DF_QT_REGISTERED_VOTERS_COLUMN].sum().reset_index()[[level_column, consts.DF_QT_REGISTERED_VOTERS_COLUMN]].drop_duplicates().groupby(level_column)[consts.DF_QT_REGISTERED_VOTERS_COLUMN].max().reset_index()


    @staticmethod
    def get_series_from_column(df: pd.DataFrame, column: str):
        try:
            return df[column].drop_duplicates().dropna()
        except KeyError:
            logger.exception("%s column not found on dataframe", column)
            raise

    @staticmethod
    def get_candidates_as_series(df: pd.DataFrame):
        return TseCsvFileHandler.get_series_from_column(df, consts.DF_CANDIDATE_NAME_COLUMN)

    @staticmethod
    def get_tse_ids_as_series(df: pd.DataFrame):
        return TseCsvFileHandler.get_series_from_column(df, consts.DF_TSE_ID_COLUMN)
    
    @staticmethod
    def get_list_by_df_column(df: pd.DataFrame, column: str):
        return TseCsvFileHandler.get_series_from_column(df, column).tolist()

    @staticmethod
    def get_df_tse_ids(df: pd.DataFrame):
        return TseCsvFileHandler.get_list_by_df_column(df, consts.DF_TSE_ID_COLUMN)
    
    @staticmethod
    def get_df_states(df: pd.DataFrame):
        return TseCsvFileHandler.get_list_by_df_column(df, consts.DF_STATE_ABBR_COLUMN)

    @staticmethod
    def get_df_states_ids(df: pd.DataFrame):
        return TseCsvFileHandler.get_list_by_df_column(df, consts.DF_STATE_ID_COLUMN)

    @staticmethod
    def get_df_metropolitan_areas(df: pd.DataFrame):
        return TseCsvFileHandler.get_list_by_df_column(df, consts.DF_METROPOLITAN_AREA_NAME_COLUMN)

    @staticmethod
    def get_grouped_by_location_df_summed_as_dict(location_columns: list[str], df: pd.DataFrame, grouped_by_location_dict: dict, column_to_be_summed: str):
        if len(location_columns) > 0:
            for location, df in df.groupby(location_columns, dropna=False):
                key = tuple(None if pd.isna(x) else x for x in location)
                grouped_by_location_dict[key] = df[column_to_be_summed].sum()
        else:
            grouped_by_location_dict[None] = df[column_to_be_summed].sum()
        
        return grouped_by_location_dict

    @staticmethod
    def get_grouped_by_location_df_votes_as_dict(location_columns: list[str], df: pd.DataFrame, grouped_by_location_dict: dict):
        return TseCsvFileHandler.get_grouped_by_location_df_summed_as_dict(location_columns, df, grouped_by_location_dict, consts.DF_QT_VOTES_COLUMN)

    @staticmethod
    def get_grouped_by_location_df_registered_voters_as_dict(location_columns: list[str], df: pd.DataFrame, grouped_by_location_dict: dict):
        return TseCsvFileHandler.get_grouped_by_location_df_summed_as_dict(location_columns, df, grouped_by_location_dict, consts.DF_QT_REGISTERED_VOTERS_COLUMN)

    @staticmethod
    # def get_computed_results_dict(location_hashable: list[Hashable], location_columns: list[str], ticket_id: int, qt_registered_voters: int, qt_null_votes):
        

    @staticmethod
    def merge_df_and_previously_gdf(df: pd.DataFrame, previously_gdf_df: pd.DataFrame):
        df = df.merge(previously_gdf_df, left_on=[consts.DF_MUNICIPALTY_NAME_COLUMN, consts.DF_STATE_ABBR_COLUMN], right_on=[consts.GDF_MUNICIPALTY_NAME_COLUMN, consts.GDF_STATE_ABBR_COLUMN], how="left")
        df = df.drop(columns=[consts.GDF_MUNICIPALTY_NAME_COLUMN, consts.GDF_STATE_ABBR_COLUMN])
        df = df.rename(columns={consts.GDF_METROPOLITAN_AREA_NAME_COLUMN : consts.DF_METROPOLITAN_AREA_NAME_COLUMN, consts.GDF_IBGE_ID_COLUMN : consts.DF_IBGE_ID_COLUMN})
        df[consts.DF_METROPOLITAN_AREA_NAME_COLUMN] = df[consts.DF_METROPOLITAN_AREA_NAME_COLUMN].str.upper()
        df[consts.DF_IBGE_ID_COLUMN] = df[consts.DF_IBGE_ID_COLUMN].astype("Int64")

        return df

    @staticmethod
    def df_municipality_name_normalization_and_correction(df: pd.DataFrame) -> pd.DataFrame:
        if df.empty:
            logger.warning("Dataframe is empty. Skipping municipality name normalization and correction.")
            return df
        
        try:
            df[consts.DF_MUNICIPALTY_NAME_COLUMN] = df[consts.DF_MUNICIPALTY_NAME_COLUMN].replace("ERERÊ", "ERERÉ").replace("ARÊS", "AREZ").replace("FORTALEZA DO TABOCÃO", "TABOCÃO").replace("QUINJINGUE", "QUIJINGUE").replace("WESTFALIA", "WESTFÁLIA").replace("ESPIGÃO DO OESTE", "ESPIGÃO D'OESTE").replace("ANHANGÜERA", "ANHANGUERA").replace("DONA EUSÉBIA", "DONA EUZÉBIA").replace("SANTA ISABEL DO PARÁ", "SANTA IZABEL DO PARÁ").replace("CAMACÃ","CAMACAN").replace("SANTO ANTÔNIO DO LEVERGER", "SANTO ANTÔNIO DE LEVERGER").replace("AMPARO DE SÃO FRANCISCO", "AMPARO DO SÃO FRANCISCO").replace("ALVORADA DO OESTE", "ALVORADA D'OESTE").replace("LUIS CORREIA", "LUÍS CORREIA").replace("SEM PEIXE", "SEM-PEIXE").replace("CAEM", "CAÉM").replace("ELDORADO DOS CARAJÁS", "ELDORADO DO CARAJÁS").replace("SÃO LUÍS DO PARAITINGA", "SÃO LUIZ DO PARAITINGA").replace("SANTO ANTONIO DO CAIUÁ", "SANTO ANTÔNIO DO CAIUÁ").replace("ANTONIO OLINTO", "ANTÔNIO OLINTO").replace("SANTO ESTEVÃO", "SANTO ESTÊVÃO").replace("BELÉM DE SÃO FRANCISCO", "BELÉM DO SÃO FRANCISCO").replace("IGUARACI", "IGUARACY").replace("SÃO VICENTE FERRÉR", "SÃO VICENTE FÉRRER").replace("AÇU", "ASSÚ").replace("GRÃO PARÁ", "GRÃO-PARÁ").replace("AUGUSTO SEVERO", "CAMPO GRANDE").replace("GRACHO CARDOSO", "GRACCHO CARDOSO").replace("JANUÁRIO CICCO", "BOA SAÚDE").replace("ATÍLIO VIVACQUA", "ATÍLIO VIVÁCQUA").replace("BARÃO DE MONTE ALTO", "BARÃO DO MONTE ALTO")
            
            return df  
        except KeyError:
            logger.exception(f"No {consts.DF_MUNICIPALTY_NAME_COLUMN} found on dataframe")
            raise

    @staticmethod
    def normalize_candidate_names(df: pd.DataFrame):
        try:
            df[consts.DF_CANDIDATE_NAME_COLUMN] = df[consts.DF_CANDIDATE_NAME_COLUMN].replace({
                "LUIZ INACIO LULA DA SILVA": "LUIZ INÁCIO LULA DA SILVA",
                "LUIZ FELIPE CHAVES D'AVILA": "LUIZ FELIPE CHAVES D'ÁVILA",
                "VERA LUCIA PEREIRA DA SILVA SALGADO": "VERA LÚCIA PEREIRA DA SILVA SALGADO",
                "JOSE MARIA EYMAEL": "JOSÉ MARIA EYMAEL",
            })

            return df
        except KeyError:
            logger.exception(f"{consts.DF_CANDIDATE_NAME_COLUMN} column not found on dataframe")
            raise

    
