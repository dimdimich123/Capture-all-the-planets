using UnityEngine;

[RequireComponent(typeof(WinPanelView))]

public sealed class WinPanelPresenter : MonoBehaviour
{
    private WinPanelModel _model;
    private WinPanelView _view;

    private void Awake()
    {
        _model = new WinPanelModel();
        _view = GetComponent<WinPanelView>();
    }

    private void OnEnable()
    {
        _view.OnExit += OnExit;
    }

    private void OnExit()
    {
        _model.GoToMenu();
    }

    private void OnDisable()
    {
        _view.OnExit -= OnExit;
    }
}
