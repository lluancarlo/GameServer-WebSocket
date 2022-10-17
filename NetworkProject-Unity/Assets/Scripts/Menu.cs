using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    InputField NameField;
    InputField AddressField;
    InputField PortField;
    Button ConnectButton;
    
    void Start()
    {
        this.NameField = GameObject.Find("NameInput").GetComponent<InputField>();
        this.AddressField = GameObject.Find("AddressInput").GetComponent<InputField>();
        this.PortField = GameObject.Find("PortInput").GetComponent<InputField>();
        this.ConnectButton = GameObject.Find("Button").GetComponent<Button>();

        this.ConnectButton.onClick.AddListener(onConnect);
        this.AddressField.text = GlobalSettings.Address;
        this.PortField.text = GlobalSettings.port;
    }

    public void onConnect(){
        GlobalSettings.Name = this.NameField.text;
        GlobalSettings.Address = this.AddressField.text;
        GlobalSettings.port = this.PortField.text;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
