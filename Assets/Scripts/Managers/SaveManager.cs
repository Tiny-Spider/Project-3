using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour {

    public string playerNameSaveTag;
    public static SaveManager instance { private set; get; }


    void Awake() {
        instance = this;
    }

    public void SaveLevels()
    {
        foreach (levels ls in GameManager.instance.levels)
        {
            PlayerPrefs.SetInt(ls.levelName, int.Parse(ls.unlocked.ToString()));
        }
    }

    public void LoadLevels()
    {
        foreach (levels ls in GameManager.instance.levels)
        {
            PlayerPrefs.GetInt(ls.levelName);
        }
    }
}
