
# GameServer WebSocket

<p align="center">
    <a href="https://imgur.com/cgSwc9Y" target="_blank">
    <img src="http://i.imgur.com/cgSwc9Yh.gif" alt="Databay showcase gif" title="Databay showcase gif" width="500"/>
</p>

A bundle of a project that applying the concept of game server using WebSocket, connecting two different type of clients in the same server, sharing the same informations.

- **Server** `Python`: A simple Python server using *SimpleWebSocketServer*. The server just receive the information and share to all clients.
- **Godot Engine** `2D Game`: A basic 2D game that the player can move around the map.
- **Unity 3D** `3D Game`: A basic 3D game that the player can move around the map.

## Architecture

It's really simple and peace of cake to understand. We have a server that receive all the updates from the player (both Godot and Unity clients). Every change that the player has (Position, in that case) will be send to the server.

<p align="center">
    <img width="400" src="https://i.imgur.com/vEF6o0N.png" alt="Architecture">
</p>

After that, the server will receive the information and add the player to the list of online players. Therefore, every time that a player has your position updated, the server receive it from the client, update the position on the list of online players and send that list to all the clients.

<p align="center">
    <img width="400" src="https://i.imgur.com/kVpCp43.png" alt="Architecture">
</p>

## Lessons Learned

WebSocket use TCP connection. It means that it has high confiability but usually high latency, that is bad to open-world or multiplayer games for example. In the other hand, it could be nice to use in a chat or turn-based games like card games or table games, where we don't care so much about latency.

## Project Details

- [![Python](https://img.shields.io/badge/Python-FFD43B?style=for-the-badge&logo=python&logoColor=blue)](https://docs.unity3d.com/Manual/index.html) Version: 3.10 (Framework [here](https://github.com/dpallot/simple-websocket-server/))

- [![Godot Engine](https://img.shields.io/badge/Godot-478CBF?style=for-the-badge&logo=GodotEngine&logoColor=white)](https://docs.godotengine.org/en/3.5/) Version: 3.5 Stable

- [![Unity 3D](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)](https://docs.unity3d.com/Manual/index.html) Version: 2021.3.10f
## License

[MIT](https://choosealicense.com/licenses/mit/)
