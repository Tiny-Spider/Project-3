using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NetworkView))]
public class NetworkManager : MonoBehaviour {

    public static NetworkManager instance { private set; get; }
    public Object lobbyScene;

    void Awake()
    {
        instance = this;
        P
    }

    void StartServer()
    {
        //TODO 
    }

    #region Connecting
    void OnConnectedToServer()
    {
        //What to do when connected with host.
    }

    void OnServerInitialized()
    {
        Application.LoadLevel(lobbyScene.name);
    }


    #endregion

}
