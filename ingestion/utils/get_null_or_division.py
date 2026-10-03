def get_null_or_division(dividend: int, divider: int):
    try:
        return dividend / divider
    except ZeroDivisionError:
        return None