using System;
using UnityEngine;
using UnityEngine.UI;

public class UIFinishPanel : MonoBehaviour, IUIPanel
{
    public event Action OnRestartButtonClicked;
    [SerializeField] private Button _restartButton;

    public void Initialize()
    {
        _restartButton.onClick.AddListener((() =>
        {
            Hide();
            OnRestartButtonClicked?.Invoke();
        }));
    }
    
    public void Show() => 
        gameObject.SetActive(false);

    public void Hide() => 
        gameObject.SetActive(false);
}