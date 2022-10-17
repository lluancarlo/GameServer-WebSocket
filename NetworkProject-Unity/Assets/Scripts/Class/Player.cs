using UnityEngine;

public class Player
{
    public string Name;
    public Vector3 Position;
    public int Type;

    public Player(){}
    public Player(string name, Vector3 position, int type){
        this.Name = name;
        this.Position = position;
        this.Type = type;
    }
}