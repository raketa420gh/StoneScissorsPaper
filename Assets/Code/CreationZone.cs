using System;
using UnityEngine;

public class CreationZone : MonoBehaviour
{
    public event Action<Collider> OnPointerEnter;
    public event Action<Collider> OnExit;

    [SerializeField] private GameObject _zoneView;
    private PlayerData _playerData;

    public void Initialize(PlayerData playerData)
    {
        _playerData = playerData;
        Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pointer>())
        {
            Show();
            OnPointerEnter?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Hide();
        OnExit?.Invoke(other);
    }

    private void Show() => 
        _zoneView.SetActive(true);

    public void Hide() => 
        _zoneView.SetActive(false);
}
