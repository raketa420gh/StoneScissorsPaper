using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IUIButtonPointer : IDragHandler, IBeginDragHandler, IEndDragHandler
{
    event Action<UnitData, Pointer, Vector3> OnPointerDragEnd;
}