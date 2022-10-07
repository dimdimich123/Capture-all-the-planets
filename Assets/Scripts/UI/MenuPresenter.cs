using UnityEngine;

[RequireComponent(typeof(MenuView))]
public sealed class MenuPresenter : MonoBehaviour
{
    [SerializeField] private SettingsContainer _settings;
    [SerializeField] private GameSettings _easySettings;
    [SerializeField] private GameSettings _normalSettings;
    [SerializeField] private GameSettings _hardSettings;

    [SerializeField] private AudioSource _sounds;

    private MenuModel _model;
    private MenuView _view;

    private void Awake()
    {
        _model = new MenuModel(_settings, _easySettings, _normalSettings, _hardSettings);
        _view = GetComponent<MenuView>();
    }

    private void OnEnable()
    {
        _view.OnPlay += OnPlay;
        _view.OnExit += OnExit;
        _view.OnToggleDifficulty += OnToggleDifficulty;
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

    private void OnDisable()
    {
        _view.OnPlay -= OnPlay;
        _view.OnExit -= OnExit;
        _view.OnToggleDifficulty -= OnToggleDifficulty;
    }
}
