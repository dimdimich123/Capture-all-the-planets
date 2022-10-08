using UnityEngine.UI;
using UnityEngine;
using System;

public class LosePanelView : MonoBehaviour
{
    [SerializeField] private Button _buttonGoToMenu;
    [SerializeField] private Button _buttonExit;

    public event Action OnExit;

    private void OnEnable()
    {
        _buttonGoToMenu.onClick.AddListener(OnButtonExitClick);
        _buttonExit.onClick.AddListener(OnButtonExitClick);
    }


    private void OnButtonExitClick()
    {
        OnExit?.Invoke();
    }

    private void OnDisable()
    {
        _buttonGoToMenu.onClick.RemoveAllListeners();
        _buttonExit.onClick.RemoveAllListeners();
    }
}
