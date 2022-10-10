using UnityEngine;

[RequireComponent(typeof(WinPanelView))]

public sealed class WinPanelPresenter : MonoBehaviour
{
    private LosePanelModel _model;
    private LosePanelView _view;

    private void Awake()
    {
        _model = new LosePanelModel();
        _view = GetComponent<LosePanelView>();
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
