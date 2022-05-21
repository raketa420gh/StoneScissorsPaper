using System;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPanel : MonoBehaviour, IUIPanel
{
    public event Action OnStartButtonClicked;
    [SerializeField] private Button _startButton;

    public void Initialize()
    {
        Show();
        
        _startButton.onClick.AddListener((() =>
        {
            Hide();
            OnStartButtonClicked?.Invoke();
        }));
    }
    
    public void Show() => 
        gameObject.SetActive(true);

    public void Hide() => 
        gameObject.SetActive(false);
}