import logging
import yaml
import constants as consts

logger = logging.getLogger("load_election_context")

def load_election_context():
    with open("settings/election.yml", "r") as file:
        data = yaml.safe_load(file)

    year: int = data["YEAR"]
    nr_round: int = data["NR_ROUND"]
    role: str = data["ROLE"]
    role = role.upper()

    if not isinstance(year, int):
        logger.error(f"Invalid election year {repr(year)}")
        raise ValueError

    if not isinstance(nr_round, int) or nr_round > 2:
        logger.error(f"Invalid round number {repr(nr_round)}")
        raise ValueError

    if role not in consts.ROLES:
        logger.error(f"Invalid role {role}")
        raise ValueError
    
    return (year, nr_round, role)