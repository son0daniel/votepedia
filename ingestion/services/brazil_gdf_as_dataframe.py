import geopandas as gpd
import pandas as pd
import utils.constants as consts
import logging

logger = logging.getLogger("brazil_gdf_as_dataframe")

class BrazilGdfAsDataframe:
    def __init__(self):
        logger.info("Preparing to load Brazil municipality-level gdf")
        path = "src/gdf/BR_municipalities_fixed.shp"
        try:
            gdf = gpd.read_file(path)[[consts.GDF_IBGE_ID_COLUMN, consts.GDF_MUNICIPALTY_NAME_COLUMN, consts.GDF_STATE_ABBR_COLUMN, consts.GDF_METROPOLITAN_AREA_NAME_COLUMN]]
            self.df = pd.DataFrame(BrazilGdfAsDataframe.gdf_municipality_name_normalization_and_correction(gdf))
            logger.info("Geodataframe loaded successfully.")
        except FileNotFoundError:
            logger.exception(f"No found with path {path} found.")
            raise

    @staticmethod
    def get_metro_areas(df: pd.DataFrame) -> list[str]:
        series_metro_areas = df[consts.GDF_METROPOLITAN_AREA_NAME_COLUMN].drop_duplicates().dropna()
        
        return series_metro_areas.tolist()

    @staticmethod
    def gdf_municipality_name_normalization_and_correction(gdf: gpd.GeoDataFrame):
        gdf[consts.GDF_MUNICIPALTY_NAME_COLUMN] = gdf[consts.GDF_MUNICIPALTY_NAME_COLUMN].str.upper()
        gdf[consts.GDF_MUNICIPALTY_NAME_COLUMN] = gdf[consts.GDF_MUNICIPALTY_NAME_COLUMN].replace("JANUÁRIO CICCO", "BOA SAÚDE").replace("OLHOS-D'ÁGUA", "OLHOS D'ÁGUA").replace("PINGO-D'ÁGUA", "PINGO D'ÁGUA").replace("SÃO TOMÉ DAS LETRAS", "SÃO THOMÉ DAS LETRAS").replace("ARÊS", "AREZ").replace("AÇU", "ASSÚ").replace("GRACHO CARDOSO", "GRACCHO CARDOSO")

        return gdf