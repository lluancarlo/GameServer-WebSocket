using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    
    public string Name { private set; get; }
    public int Type { private set; get; }

    // Start is called before the first frame update
    void Awake()
    {
        this.Name = string.Empty;
    }

    public void SetName(string name)
    {
        this.gameObject.name = name;
        this.GetComponentInChildren<TextMesh>().text = name;
    }

    public void SetType(int type){
        this.Type = type;
        var mesh = this.transform.Find("Capsule").GetComponent<MeshRenderer>();
        if (type == 0){
            mesh.material.SetColor("_Color", new Color(1f, 0f, 0f, 1f));
        }else if (type == 1){
            mesh.material.SetColor("_Color", new Color(0f, 0f, 1f, 1f));
        }
    }
}
