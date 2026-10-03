from enum import Enum


class AggregationLevel(Enum):
    MUNICIPALITY = 0
    METROPOLITAN_AREA = 1
    STATE = 2
    MACROREGION = 3
    COMPLETE = 4