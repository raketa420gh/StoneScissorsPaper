using System;
using UnityEngine;
using UnityEngine.UI;

public class UIStartPanel : MonoBehaviour
{
    public event Action OnStartButtonClicked;
    
    [SerializeField] private Button _startButton;

    public void Initialize()
    {
        gameObject.SetActive(true);
        
        _startButton.onClick.AddListener((() =>
        {
            HidePanel();
            OnStartButtonClicked?.Invoke();
        }));
    }

    private void HidePanel() => 
        gameObject.SetActive(false);
}
