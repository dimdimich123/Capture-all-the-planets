using UnityEngine.UI;
using System;
using UnityEngine;

/// <summary>
/// Implements data display from model.
/// </summary>
public sealed class MenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _buttonExit;

    [SerializeField] private Toggle _toggleEasyDifficulty;
    [SerializeField] private Toggle _toggleNormalDifficulty;
    [SerializeField] private Toggle _toggleHardDifficulty;

    [Header("Audio")]
    [SerializeField] private Toggle _toggleSound;
    [SerializeField] private Toggle _toggleMusic;

    public event Action OnPlay;
    public event Action OnExit;
    public event Action<GameDifficulty> OnToggleDifficulty;
    public event Action<bool> OnToggleSound;
    public event Action<bool> OnToggleMusic;

    private void OnEnable()
    {
        _buttonPlay.onClick.AddListener(OnButtonPlay);
        _buttonExit.onClick.AddListener(OnButtonExit);
        _toggleEasyDifficulty.onValueChanged.AddListener(OnToggleEasyDifficulty);
        _toggleNormalDifficulty.onValueChanged.AddListener(OnToggleNormalDifficulty);
        _toggleHardDifficulty.onValueChanged.AddListener(OnToggleHardDifficulty);
        _toggleSound.onValueChanged.AddListener(OnToggleSoundClick);
        _toggleMusic.onValueChanged.AddListener(OnToggleMusicClick);
    }

    private void OnButtonPlay()
    {
        OnPlay?.Invoke();
    }

    private void OnButtonExit()
    {
        OnExit?.Invoke();
    }

    private void OnToggleEasyDifficulty(bool state)
    {
        if(state)
        {
            OnToggleDifficulty?.Invoke(GameDifficulty.Easy);
        }
    }

    private void OnToggleNormalDifficulty(bool state)
    {
        if (state)
        {
            OnToggleDifficulty?.Invoke(GameDifficulty.Normal);
        }
    }

    private void OnToggleHardDifficulty(bool state)
    {
        if (state)
        {
            OnToggleDifficulty?.Invoke(GameDifficulty.Hard);
        }
    }

    private void OnToggleSoundClick(bool state)
    {
        OnToggleSound?.Invoke(state);
    }

    private void OnToggleMusicClick(bool state)
    {
        OnToggleMusic?.Invoke(state);
    }

    private void OnDisable()
    {
        _buttonPlay.onClick.RemoveAllListeners();
        _buttonExit.onClick.RemoveAllListeners();
        _toggleEasyDifficulty.onValueChanged.RemoveAllListeners();
        _toggleNormalDifficulty.onValueChanged.RemoveAllListeners();
        _toggleHardDifficulty.onValueChanged.RemoveAllListeners();
        _toggleSound.onValueChanged.RemoveAllListeners();
        _toggleMusic.onValueChanged.RemoveAllListeners();
    }
}
