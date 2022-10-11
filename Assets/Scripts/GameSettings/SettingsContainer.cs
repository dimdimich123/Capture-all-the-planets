using UnityEngine;

[CreateAssetMenu(fileName = "NewSettingsContainer", menuName = "SettingsContainer", order = 2)]
public sealed class SettingsContainer : ScriptableObject
{
    [SerializeField] private GameSettings _settings;

    public GameSettings Settings
    {
        get
        {
            return _settings;
        }
        set
        {
            _settings = value;
        }
    }
}
