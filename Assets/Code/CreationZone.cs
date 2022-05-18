using System;
using UnityEngine;

public class CreationZone : MonoBehaviour
{
    public event Action<Collider> OnPointerEnter;
    public event Action<Collider> OnExit;

    [SerializeField] private GameObject _meshGameObject;
    private PlayerData _playerData;

    public PlayerType PlayerType => _playerData.Type;

    public void Initialize(PlayerData playerData) => 
        _playerData = playerData;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pointer>())
            OnPointerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other) => 
        OnExit?.Invoke(other);

    private void Show() => 
        _meshGameObject.SetActive(true);

    private void Hide() => 
        _meshGameObject.SetActive(false);
}
