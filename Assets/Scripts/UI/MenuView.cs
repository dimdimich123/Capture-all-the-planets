using UnityEngine.UI;
using System;
using UnityEngine;

public sealed class MenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _buttonExit;

    [SerializeField] private Toggle _toggleEasyDifficulty;
    [SerializeField] private Toggle _toggleNormalDifficulty;
    [SerializeField] private Toggle _toggleHardDifficulty;

    public event Action OnPlay;
    public event Action OnExit;
    public event Action<GameDifficulty> OnToggleDifficulty;

    private void OnEnable()
    {
        _buttonPlay.onClick.AddListener(OnButtonPlay);
        _buttonExit.onClick.AddListener(OnButtonExit);
        _toggleEasyDifficulty.onValueChanged.AddListener(OnToggleEasyDifficulty);
        _toggleNormalDifficulty.onValueChanged.AddListener(OnToggleNormalDifficulty);
        _toggleHardDifficulty.onValueChanged.AddListener(OnToggleHardDifficulty);
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

    private void OnDisable()
    {
        _buttonPlay.onClick.RemoveAllListeners();
        _buttonExit.onClick.RemoveAllListeners();
        _toggleEasyDifficulty.onValueChanged.RemoveAllListeners();
        _toggleNormalDifficulty.onValueChanged.RemoveAllListeners();
        _toggleHardDifficulty.onValueChanged.RemoveAllListeners();
    }
}
