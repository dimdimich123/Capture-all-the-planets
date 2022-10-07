using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MenuModel
{
    private SettingsContainer _settings;
    private GameSettings _easySettings;
    private GameSettings _normalSettings;
    private GameSettings _hardSettings;

    public MenuModel(SettingsContainer settings, GameSettings easySettings, GameSettings normalSettings, GameSettings hardSettings)
    {
        _settings = settings;
        _easySettings = easySettings;
        _normalSettings = normalSettings;
        _hardSettings = hardSettings;
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

    public void ExitGame()
    {
        Application.Quit();
    }

}
