using UnityEngine;
using UnityEngine.UI;

public class UIInfoPanel : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Image _image;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponentInChildren<Image>();
    }

    public void TurnInfoPanelTo(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
            {
                var angle = new Vector3(0, 0, 0);
                _rectTransform.rotation = Quaternion.Euler(angle);
                break;
            }
            case 2:
            {
                var angle = new Vector3(0, 0, 180);
                _rectTransform.rotation = Quaternion.Euler(angle);
                break;
            }
        }
    }

    public void UpdateFiller(float normalized) =>
        _image.fillAmount = normalized;

    public void SetInfoColor(Color color) => 
        _image.color = color;
}
