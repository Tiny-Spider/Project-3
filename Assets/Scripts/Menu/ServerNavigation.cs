using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ServerNavigation : MonoBehaviour {

    public InputField serverName;
    public InputField maxPlayers;
    public InputField port;
    public Toggle useNAT;

    private string hostServerName;
    private InputField selectedDirectConnectField;

    public void StartServer() {
        string errorMessage = "";

        int tempMaxPlayers;
        int tempPort;

        if (serverName.text.Equals("")) {
            errorMessage += "Enter a server name\n";
        }
        if (!int.TryParse(maxPlayers.text, out tempMaxPlayers)) {
            errorMessage += "Enter amount of maximum players\n";
        }
        if (!int.TryParse(port.text, out tempPort)) {
            errorMessage += "Enter port number\n";
        }

        if (errorMessage.Equals("")) {
            NetworkConnectionError networkError = Network.InitializeServer(int.Parse(maxPlayers.text), int.Parse(port.text), useNAT.isOn);
            MasterServer.RegisterHost(GameManager.instance.uniqueGameType, serverName.text);
            //TODO move this section to NetworkManager StartServer();
        }

    }

    public void TryConnectToServer(InputField port) {
        //Tries connecting to the given address. Also handles any errors that may occur, and sends the feedback through into the GUI.
        int tempPort;

        if (int.TryParse(port.text, out tempPort)) {
            NetworkConnectionError networkError = Network.Connect(selectedDirectConnectField.text, tempPort);

        }
    }

    public void SelectDirectConnectInputField(InputField inputField) {
        selectedDirectConnectField = inputField;
    }
}
