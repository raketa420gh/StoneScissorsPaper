using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class UIButtonPointer: MonoBehaviour, IUIButtonPointer
{
    public event Action<string, Pointer, Vector3> OnPointerDragEnd;
    [SerializeField] private UnitData _unitData;
    [SerializeField] private Image _icon;
    private GameFactory _factory;
    private Pointer _pointer;
    private Vector3 _pointerStartPosition = new Vector3(0, 100, 0);

    [Inject]
    public void Construct(GameFactory factory) => 
        _factory = factory;

    public void Initialize(PlayerData playerData)
    {
        _pointer = _factory.CreatePointer(_pointerStartPosition);
        _pointer.gameObject.SetActive(false);
        _icon.sprite = _unitData.Icon;
    }

    public void OnBeginDrag(PointerEventData eventData) => 
        _pointer.gameObject.SetActive(true);

    public void OnDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform hitTransform = hit.transform;
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        _pointer.gameObject.transform.position = hit.point;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _pointer.gameObject.SetActive(false);
        OnPointerDragEnd?.Invoke(_unitData.AssetPath, _pointer, _pointer.transform.position);
    }
}