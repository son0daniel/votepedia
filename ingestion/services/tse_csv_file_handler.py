import pandas as pd
import logging
import utils.constants as consts

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
            logger.info("DataFrame loaded successfully")
            return df
        except FileNotFoundError:
            logger.exception("TSE result file with path %s not found", path)
            raise

    def get_municipalities_df(self) -> pd.DataFrame:
        df_municipalities = self.df[[consts.DF_TSE_ID_COLUMN, consts.DF_MUNICIPALTY_NAME_COLUMN, consts.DF_STATE_ABBR_COLUMN]].drop_duplicates()

        return TseCsvFileHandler.df_municipality_name_normalization_and_correction(df_municipalities)

    @staticmethod
    def df_municipality_name_normalization_and_correction(df: pd.DataFrame) -> pd.DataFrame:
        if df.empty:
            logger.warning("Dataframe is empty. Skipping municipality name normalization and correction.")
            return df
        
        try:
            df[consts.DF_MUNICIPALTY_NAME_COLUMN] = df[consts.DF_MUNICIPALTY_NAME_COLUMN].replace("ERERÊ", "ERERÉ").replace("ARÊS", "AREZ").replace("FORTALEZA DO TABOCÃO", "TABOCÃO").replace("QUINJINGUE", "QUIJINGUE").replace("WESTFALIA", "WESTFÁLIA").replace("ESPIGÃO DO OESTE", "ESPIGÃO D'OESTE").replace("ANHANGÜERA", "ANHANGUERA").replace("DONA EUSÉBIA", "DONA EUZÉBIA").replace("SANTA ISABEL DO PARÁ", "SANTA IZABEL DO PARÁ").replace("CAMACÃ","CAMACAN").replace("SANTO ANTÔNIO DO LEVERGER", "SANTO ANTÔNIO DE LEVERGER").replace("AMPARO DE SÃO FRANCISCO", "AMPARO DO SÃO FRANCISCO").replace("ALVORADA DO OESTE", "ALVORADA D'OESTE").replace("LUIS CORREIA", "LUÍS CORREIA").replace("SEM PEIXE", "SEM-PEIXE").replace("CAEM", "CAÉM").replace("ELDORADO DOS CARAJÁS", "ELDORADO DO CARAJÁS").replace("SÃO LUÍS DO PARAITINGA", "SÃO LUIZ DO PARAITINGA").replace("SANTO ANTONIO DO CAIUÁ", "SANTO ANTÔNIO DO CAIUÁ").replace("ANTONIO OLINTO", "ANTÔNIO OLINTO").replace("SANTO ESTEVÃO", "SANTO ESTÊVÃO").replace("BELÉM DE SÃO FRANCISCO", "BELÉM DO SÃO FRANCISCO").replace("IGUARACI", "IGUARACY").replace("SÃO VICENTE FERRÉR", "SÃO VICENTE FÉRRER").replace("AÇU", "ASSÚ").replace("GRÃO PARÁ", "GRÃO-PARÁ").replace("AUGUSTO SEVERO", "CAMPO GRANDE").replace("GRACHO CARDOSO", "GRACCHO CARDOSO").replace("JANUÁRIO CICCO", "BOA SAÚDE").replace("ATÍLIO VIVACQUA", "ATÍLIO VIVÁCQUA").replace("BARÃO DE MONTE ALTO", "BARÃO DO MONTE ALTO")
            
            return df  
        except KeyError:
            logger.exception(f"No {consts.DF_MUNICIPALTY_NAME_COLUMN} found on DataFrame")
            raise

    @staticmethod
    def get_list_by_df_column(df: pd.DataFrame, column: str):
        series = df[column].drop_duplicates().dropna()

        return series.tolist()
    
    @staticmethod
    def get_df_states(df: pd.DataFrame):
        return TseCsvFileHandler.get_list_by_df_column(df, consts.DF_STATE_ABBR_COLUMN)

    @staticmethod
    def get_df_metropolitan_areas(df: pd.DataFrame):
        return TseCsvFileHandler.get_list_by_df_column(df, consts.DF_METROPOLITAN_AREA_NAME_COLUMN)

    @staticmethod
    def merge_df_and_previously_gdf(df: pd.DataFrame, previously_gdf_df: pd.DataFrame):
        df = df.merge(previously_gdf_df, left_on=[consts.DF_MUNICIPALTY_NAME_COLUMN, consts.DF_STATE_ABBR_COLUMN], right_on=[consts.GDF_MUNICIPALTY_NAME_COLUMN, consts.GDF_STATE_ABBR_COLUMN], how="left")
        df = df.drop(columns=[consts.GDF_MUNICIPALTY_NAME_COLUMN, consts.GDF_STATE_ABBR_COLUMN])
        df = df.rename(columns={consts.GDF_METROPOLITAN_AREA_NAME_COLUMN : consts.DF_METROPOLITAN_AREA_NAME_COLUMN, consts.GDF_IBGE_ID_COLUMN : consts.DF_IBGE_ID_COLUMN})
        df[consts.DF_METROPOLITAN_AREA_NAME_COLUMN] = df[consts.DF_METROPOLITAN_AREA_NAME_COLUMN].str.upper()
        df[consts.DF_IBGE_ID_COLUMN] = df[consts.DF_IBGE_ID_COLUMN].astype("Int64")

        return df

    
