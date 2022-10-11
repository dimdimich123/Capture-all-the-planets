using UnityEngine.UI;
using UnityEngine;
using System;

[RequireComponent(typeof(CanvasGroup))]

public sealed class WinPanelView : MonoBehaviour
{
    [SerializeField] private Button _buttonGoToMenu;

    private CanvasGroup _canvas;

    public event Action OnExit;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _buttonGoToMenu.onClick.AddListener(OnButtonExitClick);
    }

    private void OnButtonExitClick()
    {
        OnExit?.Invoke();
    }

    public void Open()
    {
        _canvas.alpha = 1;
        _canvas.interactable = true;
        _canvas.blocksRaycasts = true;
    }

    public void Close()
    {
        _canvas.alpha = 0;
        _canvas.interactable = false;
        _canvas.blocksRaycasts = false;
    }

    private void OnDisable()
    {
        _buttonGoToMenu.onClick.RemoveAllListeners();
    }
}
