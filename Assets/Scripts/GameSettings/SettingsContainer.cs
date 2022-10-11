using UnityEngine;

/// <summary>
/// Stores a reference to the settings object.
/// </summary>
/// <remarks>
/// Needed to transfer settings between scenes.
/// </remarks>

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
