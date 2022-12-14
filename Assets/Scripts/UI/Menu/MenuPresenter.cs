using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Implements the interaction between the MenuModel and the MenuView.
/// </summary>
[RequireComponent(typeof(MenuView))]
public sealed class MenuPresenter : MonoBehaviour
{
    [SerializeField] private SettingsContainer _settings;
    [SerializeField] private GameSettings _easySettings;
    [SerializeField] private GameSettings _normalSettings;
    [SerializeField] private GameSettings _hardSettings;
    [SerializeField] private AudioSource _sounds;
    [SerializeField] private AudioMixer _mixer;

    private MenuModel _model;
    private MenuView _view;

    private void Awake()
    {
        _model = new MenuModel(_settings, _easySettings, _normalSettings, _hardSettings, _mixer);
        _view = GetComponent<MenuView>();
    }

    private void OnEnable()
    {
        _view.OnPlay += OnPlay;
        _view.OnExit += OnExit;
        _view.OnToggleDifficulty += OnToggleDifficulty;
        _view.OnToggleSound += OnToggleSound;
        _view.OnToggleMusic += OnToggleMusic;
    }

    private void OnPlay()
    {
        _sounds.PlayOneShot(_sounds.clip);
        _model.LoadLevel();
    }

    private void OnExit()
    {
        _sounds.PlayOneShot(_sounds.clip);
        _model.ExitGame();
    }

    private void OnToggleDifficulty(GameDifficulty difficulty)
    {
        _sounds.PlayOneShot(_sounds.clip);
        _model.ChangeDifficulty(difficulty);
    }

    private void OnToggleSound(bool state)
    {
        _model.ChangeSound(state);
        _sounds.PlayOneShot(_sounds.clip);
    }

    private void OnToggleMusic(bool state)
    {
        _model.ChangeMusic(state);
        _sounds.PlayOneShot(_sounds.clip);
    }

    private void OnDisable()
    {
        _view.OnPlay -= OnPlay;
        _view.OnExit -= OnExit;
        _view.OnToggleDifficulty -= OnToggleDifficulty;
        _view.OnToggleSound -= OnToggleSound;
        _view.OnToggleMusic -= OnToggleMusic;
    }
}
