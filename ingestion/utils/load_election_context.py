import yaml

def load_election_context():
    with open("settings/election.yml", "r") as file:
        data = yaml.safe_load(file)
    return (data["YEAR"], data["NR_ROUND"], data["ROLE"])