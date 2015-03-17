using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance { private set; get; }
    public MenuNavigation menuNavigation;
    public ServerNavigation serverNavigation;
    public SaveData[] saveData;

    void Awake()
    {
        instance = this;
        menuNavigation = GetComponent<MenuNavigation>();
        serverNavigation = GetComponent<ServerNavigation>();
    }

    void Start()
    {
        CheckPlayerNameSet();
    }

    /// <summary>
    /// Checks if the player has already set a name. If the player hasn't already set a name the Name Ask popup panel will open.
    /// </summary>
    void CheckPlayerNameSet()
    {
        if (GameManager.instance.playerName == "player")
        {
            menuNavigation.OpenPanel(menuNavigation.enterNamePanel);
        }
    }

    public void UpdatePlayerNameText()
    {
        if(menuNavigation.playerNameText)
        menuNavigation.playerNameText.text = GameManager.instance.playerName;
    }

    /// <summary>
    /// This is Used to set the values from inputfields.
    /// </summary>
    public void SaveData()
    {
        foreach (SaveData _saveData in saveData)
        {
            PlayerPrefs.SetString(_saveData.SaveTag, _saveData.inputField.text);
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// This is used to load the data back into the inputfields.
    /// </summary>
    public void LoadData()
    {
        foreach (SaveData _saveData in saveData)
        {
            _saveData.inputField.text = PlayerPrefs.GetString(_saveData.SaveTag);
        }
    }
}
