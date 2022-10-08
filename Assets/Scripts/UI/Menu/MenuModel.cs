using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public sealed class MenuModel
{
    private const string SoundVolume = "SoundVolume";
    private const string MusicVolume = "MusicVolume";

    private SettingsContainer _settings;
    private GameSettings _easySettings;
    private GameSettings _normalSettings;
    private GameSettings _hardSettings;

    private AudioMixer _mixer;

    public MenuModel(SettingsContainer settings, GameSettings easySettings, GameSettings normalSettings, GameSettings hardSettings, AudioMixer mixer)
    {
        _settings = settings;
        _easySettings = easySettings;
        _normalSettings = normalSettings;
        _hardSettings = hardSettings;
        _mixer = mixer;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneName.Level.ToString());
    }

    public void ChangeDifficulty(GameDifficulty difficulty)
    {
        switch(difficulty)
        {
            case GameDifficulty.Easy:
                _settings.Settings = _easySettings;
                break;
            case GameDifficulty.Normal:
                _settings.Settings = _normalSettings;
                break;
            case GameDifficulty.Hard:
                _settings.Settings = _hardSettings;
                break;
            default: 
                throw new System.Exception("Unknown difficulty (" + difficulty.ToString() + ")");
        }
    }

    public void ChangeSound(bool state)
    {
        float volume = 0;
        if(!state)
        {
            volume = -80f;
        }
        _mixer.SetFloat(SoundVolume, volume);
    }

    public void ChangeMusic(bool state)
    {
        float volume = 0;
        if (!state)
        {
            volume = -80f;
        }
        _mixer.SetFloat(MusicVolume, volume);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
