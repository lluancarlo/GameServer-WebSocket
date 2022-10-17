from SimpleWebSocketServer import SimpleWebSocketServer, WebSocket
from player import Player
import json

wss = []
players = []

def array_to_json(arr):
    arr_string = "["
    for item in arr:
        arr_string += item.toJSON()
        arr_string += ","
    arr_string += "]"
    return arr_string

def search(name: str):
    for i in range(len(players)):
        if players[i].Name == name:
            return i
    return -1

def handler_players(p: Player):
    index = search(p.Name)
    if index > -1:
        players[index].Position = p.Position
    else:
        players.append(p)
    print("Players online: " + str(len(players)))

class SimpleServer(WebSocket):
    def handleMessage(self):
        if self.data is None:
            self.data = ''
        
        clientMessage: dict = json.loads(self.data)
        if clientMessage.get("Name") != None:
            p = Player(
                clientMessage.get("Name"),
                clientMessage.get("Position"),
                clientMessage.get("Type")
            )
            handler_players(p)
        arr = array_to_json(players)

        for ws in wss:
            ws.sendMessage(arr)

    def handleConnected(self):
        print(self.address, 'connected')
        if self not in wss:
            wss.append(self)

    def handleClose(self):
        print(self.address, 'closed')
        wss.remove(self)

if __name__ == "__main__":
    server = SimpleWebSocketServer('localhost', 8080, SimpleServer)
    server.serveforever()