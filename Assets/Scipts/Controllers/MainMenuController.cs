using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _load;
    [SerializeField] private GameObject _scenes;

    private GameObject _activeMenu;
    // Start is called before the first frame update
    void Start()
    {
        _activeMenu = _mainMenu;
    }

    public void OnClickNewGame()
    {

    }
    public void OnClickLoad()
    {
        ShowMenu(_load);
    }
    public void OnClickScenes()
    {
        ShowMenu(_scenes);
    }
    public void OnClickSettings()
    {
        ShowMenu(_settings);
    }
    public void OnClickBackMainMenu()
    {
        ShowMenu(_mainMenu);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickTestSceneAI()
    {
        Managers.GameSceneManager.SwitchToScene(Scenes.TEST_AI);
    }

    private void ShowMenu(GameObject menu)
    {
        _activeMenu.SetActive(false);
        _activeMenu = menu;
        _activeMenu.SetActive(true);
    }
}
