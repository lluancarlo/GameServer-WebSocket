using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class ServerClient : MonoBehaviour
{
    string serverUrl = $"ws://{GlobalSettings.Address}:{GlobalSettings.port}";
    WebSocket ws;
    bool playersOnlineChange;

    public GameObject playerPrefab;
    private List<Player> PlayersOnline;
    private List<GameObject> ObjectPlayersOnline;
    private bool updateLocalPlayers;
    
    // PlayerRef
    GameObject localPlayerRef;
    Vector3 oldPosition;

    void Start()
    {
        this.oldPosition = transform.position;
        this.localPlayerRef = GameObject.Find("LocalPlayer");
        this.localPlayerRef.GetComponent<PlayerObject>().SetName(GlobalSettings.Name);
        this.localPlayerRef.GetComponent<PlayerObject>().SetType(0);
        this.PlayersOnline = new List<Player>();
        this.ObjectPlayersOnline = new List<GameObject>();

        try
        {
            ws = new WebSocket(serverUrl);
            ws.Connect();
            ws.OnMessage += receiveFromServer;
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
            throw;
        }
    }

    void Update()
    {
        if(ws.IsAlive)
        {
            if (this.localPlayerRef.transform.position != oldPosition){
                this.oldPosition = this.localPlayerRef.transform.position;
                var locPlayer = new Player(GlobalSettings.Name, this.localPlayerRef.transform.position, 0);
                this.SendToServer(locPlayer);
            }

            if (this.updateLocalPlayers)
            {
                this.updateLocalPlayers = false;
                foreach (var player in PlayersOnline)
                {
                    var index = this.ObjectPlayersOnline.FindIndex(f => f.name == player.Name);
                    if (index > -1){
                        this.ObjectPlayersOnline[index].transform.position = player.Position;
                    }else{
                        var newP = this.CreatePlayer(player);
                        this.ObjectPlayersOnline.Add(newP);
                    }
                }
            }
        }
    }

    public void SendToServer(object message)
    {
        var json = JsonUtility.ToJson(message);
        ws.Send(json);
    }

    private void receiveFromServer(object sender, MessageEventArgs e)
    {
        var data = e.Data;
        var pList = JsonConvert.DeserializeObject<List<Player>>(data);

        // Remove local player from list
        pList = pList.Where(w => w.Name != GlobalSettings.Name).ToList();

        foreach (var player in pList)
        {
            var index = this.PlayersOnline.FindIndex(f => f.Name == player.Name);
            if (index > -1){
                this.PlayersOnline[index].Position = player.Position;
                this.PlayersOnline[index].Type = player.Type;
            }else{
                this.PlayersOnline.Add(player);
            }
            this.updateLocalPlayers = true;
        }
    }

    private GameObject CreatePlayer(Player p){
        var instance = Instantiate(this.playerPrefab);
        instance.GetComponent<PlayerObject>().SetName(p.Name);
        instance.GetComponent<PlayerObject>().SetType(p.Type);
        instance.transform.position = p.Position;
        return instance;
    }
}
