using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFinishPanel : MonoBehaviour, IUIPanel
{
    public event Action OnRestartButtonClicked;
    
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _winText;

    public void Initialize()
    {
        _restartButton.onClick.AddListener((() =>
        {
            Hide();
            OnRestartButtonClicked?.Invoke();
        }));
    }

    public void SetWinTextColor(Color color) => 
        _winText.color = color;

    public void Show() => 
        gameObject.SetActive(true);

    public void Hide() => 
        gameObject.SetActive(false);
}