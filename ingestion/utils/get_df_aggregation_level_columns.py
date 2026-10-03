from utils.aggregation_level import AggregationLevel
import utils.constants as consts


def get_df_aggregation_level_columns(aggregation_level: AggregationLevel):
    match aggregation_level:
        case AggregationLevel.MUNICIPALITY:
            return [consts.DF_MUNICIPALTY_ID_COLUMN, consts.DF_METROPOLITAN_AREA_ID_COLUMN, consts.DF_STATE_ID_COLUMN, consts.DF_MACROREGION_ID_COLUMN]
        case AggregationLevel.METROPOLITAN_AREA:
            return [consts.DF_METROPOLITAN_AREA_ID_COLUMN, consts.DF_STATE_ID_COLUMN, consts.DF_MACROREGION_ID_COLUMN]
        case AggregationLevel.STATE:
            return [consts.DF_STATE_ID_COLUMN, consts.DF_MACROREGION_ID_COLUMN]
        case AggregationLevel.MACROREGION:
            return [consts.DF_MACROREGION_ID_COLUMN]
        case _:
            return []