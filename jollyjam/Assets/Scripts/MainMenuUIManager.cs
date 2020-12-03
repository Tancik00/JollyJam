using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject panel;
    public GameObject settings;
    public GameObject offSettings;
    public GameObject sound;
    public GameObject music;
    public GameObject info;
    public GameObject infoPanel;

    public Button playBtn;

    public LeaderboardHandler leaderboardHandler;

    //public AudioSource hitSound;

    public void OpenSelectLevelPanel()
    {
        SceneManager.LoadScene(1);
        AppStateData.IsRestartButtonPressed = false;
        SendSelectButtonName("Select Level Button");
    }

    public void OpenSettingsPanel()
    {
        AppStateData.IsPause = true;
        settingsPanel.SetActive(true);
        playBtn.gameObject.SetActive(false);
        SendSelectButtonName("Settings Button");
    }
    public void CloseSettingsPanel()
    {
        AppStateData.IsPause = false;
        settingsPanel.SetActive(false);
        playBtn.gameObject.SetActive(true);
    }

    public void OpenMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickMusicBtn()
    {
        AppStateData.isMusicOn = !AppStateData.isMusicOn;
        SendSelectButtonName("Music Button");
    }

    public void ClickSoundBtn()
    {
        AppStateData.isSoundOn = !AppStateData.isSoundOn;
        SendSelectButtonName("Sound Button");
    }
    
    public void ClickVibrationBtn()
    {
        AppStateData.isVibrationOn = !AppStateData.isVibrationOn;
        SendSelectButtonName("Vibration Button");
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://jollybattle.com/docs/privacypolicy_jollyjam.pdf");
    }

    public void ShowLeaderboard()
    {
        leaderboardHandler.SubmitScore();
    }

    public void OpenSettings()
    {
        panel.SetActive(true);
        settings.SetActive(false);
        offSettings.SetActive(true);
        sound.SetActive(true);
        music.SetActive(true);
        info.SetActive(true);
    }

    public void CloseSettings()
    {
        panel.SetActive(false);
        settings.SetActive(true);
        offSettings.SetActive(false);
        sound.SetActive(false);
        music.SetActive(false);
        info.SetActive(false);
    }

    public void OpenInfoPanel()
    {
        infoPanel.SetActive(true);
    }

    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
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
