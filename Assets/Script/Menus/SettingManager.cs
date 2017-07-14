using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour {
    public Toggle fullscreenToggle;
    public Dropdown resolutionDrop;
    public Dropdown aADrop;
    public Dropdown VsyncDrop;
    public Slider Soundvol;
    public Button applyButton;

    public AudioSource musicSource;
    public Resolution[] resolutions;
    public GameSettings gameSettings;

    void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle();  });
        resolutionDrop.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        aADrop.onValueChanged.AddListener(delegate { OnAAChange(); });
        VsyncDrop.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        Soundvol.onValueChanged.AddListener(delegate { OnMusicChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick();  });

        resolutions = Screen.resolutions;
        foreach (Resolution resoltion in resolutions)
        {
            resolutionDrop.options.Add(new Dropdown.OptionData(resoltion.ToString()));
        }

        LoadSettings();
    }

    public void OnFullScreenToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDrop.value].width, resolutions[resolutionDrop.value].height, Screen.fullScreen);
        //gameSettings.resolutionIndex = resolutionDrop.value;
    }

    public void OnAAChange()
    {
        QualitySettings.antiAliasing = gameSettings.Antialiasing =(int)Mathf.Pow(2, aADrop.value);
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = gameSettings.vSync = VsyncDrop.value;
    }

    public void OnMusicChange()
    {
        musicSource.volume = gameSettings.soundVolume= Soundvol.value;
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }
    public void SaveSettings()
    {
        string jsondata = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/Gamesettings;json", jsondata);
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText( Application.persistentDataPath + "/Gamesettings;json"));

        Soundvol.value = gameSettings.soundVolume;
        aADrop.value = gameSettings.Antialiasing;
        VsyncDrop.value = gameSettings.vSync;
        resolutionDrop.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        Screen.fullScreen = gameSettings.fullscreen;

        resolutionDrop.RefreshShownValue();
    }
}
