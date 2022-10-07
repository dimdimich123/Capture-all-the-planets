using UnityEngine;

[RequireComponent(typeof(MenuView))]
public sealed class MenuPresenter : MonoBehaviour
{
    [SerializeField] private SettingsContainer _settings;
    [SerializeField] private GameSettings _easySettings;
    [SerializeField] private GameSettings _normalSettings;
    [SerializeField] private GameSettings _hardSettings;

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
        _model.LoadLevel();
    }

    private void OnExit()
    {
        _model.ExitGame();
    }

    private void OnToggleDifficulty(GameDifficulty difficulty)
    {
        _model.ChangeDifficulty(difficulty);
    }

    private void OnDisable()
    {
        _view.OnPlay -= OnPlay;
        _view.OnExit -= OnExit;
        _view.OnToggleDifficulty -= OnToggleDifficulty;
    }
}
