using UnityEngine;
using System.Collections;

public class LobbyManager : MonoBehaviour {

    public static LobbyManager instance { private set; get; }

    void Awake()
    {
        instance = this;
    }


}
