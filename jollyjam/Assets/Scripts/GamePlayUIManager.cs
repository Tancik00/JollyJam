using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class GamePlayUIManager : MonoBehaviour
{
    public Button restartBtn;
    public Button levelsBtn;
    public Button pause;
    public Button offPause;
    public Button map;
    public Button music;

    public GameObject panel;

    public void StartNextLevel()
    {
        SelectLevel(LevelManager.localCurrentLevel);
    }
    
    public void GoToMap()
    {
        AppStateData.IsPause = false;
        SceneManager.LoadScene(1);
        AppStateData.IsRestartButtonPressed = false;
    }

    public void RestartLevel()
    {
        AppStateData.IsPause = false;
        SceneManager.LoadScene(2);
        AppStateData.IsRestartButtonPressed = true;
        SendSelectButtonName("Restart Button");
    }

    public void RestartLevelDuringTheGame()
    {
        AppStateData.IsPause = false;
        SceneManager.LoadScene(2);
        LevelManager.localCurrentLevel--;
        AppStateData.IsRestartButtonPressed = true;
        SendSelectButtonName("Restart Button");
    }

    public void OpenSelectLevelPanel()
    {
        SceneManager.LoadScene(1);
        AppStateData.IsRestartButtonPressed = false;
        SendSelectButtonName("Select Level Button");
    }

    public void SelectLevel(int level)
    {
        SceneManager.LoadScene(2);
        LevelManager.localCurrentLevel = level;
        SendSelectLevel(level);
    }

    public void StartCurrentLevel()
    {
        SelectLevel(LevelManager.localCurrentLevel);
    }

    public void MakeButtonsInactive()
    {
        restartBtn.interactable = false;
        levelsBtn.interactable = false;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickMusicBtn()
    {
        AppStateData.isMusicOn = !AppStateData.isMusicOn;
        SendSelectButtonName("Music Button");
    }

    public void OnPause()
    {
        AppStateData.IsPause = true;
        panel.SetActive(true);
        map.gameObject.SetActive(true);
        music.gameObject.SetActive(true);
        offPause.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
    }

    public void OffPause()
    {
        AppStateData.IsPause = false;
        panel.SetActive(false);
        map.gameObject.SetActive(false);
        music.gameObject.SetActive(false);
        offPause.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
    }

    void SendSelectLevel(int levelNum)
    {
        AnalyticsHandler.SendAnalyticsEvent("LevelSelected", new Dictionary<string, object>
        {
            {"Level", levelNum}
        });
        Debug.Log("UI Manager event: level number = " + levelNum);
    }

    void SendSelectButtonName(string name)
    {
        AnalyticsHandler.SendAnalyticsEvent("Button Click", new Dictionary<string, object>
        {
            {name, 0}
        });
        Debug.Log("Button name event: " + name);
    }
}
