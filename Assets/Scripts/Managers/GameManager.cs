using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    
    public string playerName;
    public static GameManager instance { private set; get; }
    public string uniqueGameType = "brawl_battles";
    public List<levels> levels = new List<levels>();

    void Awake()
    {
        instance = this;
        playerName = PlayerPrefs.GetString(SaveManager.instance.playerNameSaveTag, "player");
        MenuManager.instance.UpdatePlayerNameText();
    }

    public void SetPlayerName(string _playerName)
    {
        playerName = _playerName;
        PlayerPrefs.SetString("playerName", playerName);
        MenuManager.instance.UpdatePlayerNameText();
    }
}
