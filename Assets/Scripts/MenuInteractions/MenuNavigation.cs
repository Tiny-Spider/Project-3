using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuNavigation : MonoBehaviour {
/*MenuNavigation
 * 
 * Used for the navigation of the menu. With mostly public functions which can be used with Unity's UI system.
 * */

    public GameObject canvas;
    public GameObject firstPanel;
    public GameObject enterNamePanel;
    public Text playerNameText;

    private List<GameObject> panelList = new List<GameObject>();

    void Awake()
    {
        InitializePanels();
    }

    public void InitializePanels()
    {
        foreach (Transform panel in canvas.transform)
        {
            if(panel.name.Contains("Panel"))
            panelList.Add(panel.gameObject);
            panel.gameObject.SetActive(false);
        }
        firstPanel.SetActive(true);
    }

    public void SwitchPanel(GameObject panel)
    {
        foreach (GameObject go in panelList)
        {
            if (go.Equals(panel))
                go.SetActive(true);
            else
                go.SetActive(false);
        }
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void LoadScene(Object scene)
    {
        string levelName = scene.name;
        print((string)levelName);
        Application.LoadLevel(levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CheckName(InputField _inputField)
    {
        string _playerName = _inputField.text;
        if (_playerName.Length >= 2)
        {
            GameManager.instance.SetPlayerName(_playerName);
            enterNamePanel.SetActive(false);
            playerNameText.text = _playerName;
        }
        //TODO handle error feedback here
    } 

}
