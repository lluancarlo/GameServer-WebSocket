import json
from typing import Tuple

class Player:
    Name = ""
    Position = {}
    Type = 0

    def __init__(self, Name: str, Position: Tuple, Type: int):
        self.Name = str(Name)
        self.Position = Position
        self.Type = Type
    
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, sort_keys=True, indent=4)