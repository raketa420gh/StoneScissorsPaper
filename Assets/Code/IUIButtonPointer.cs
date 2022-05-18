using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IUIButtonPointer : IDragHandler, IBeginDragHandler, IEndDragHandler
{
    event Action<string, Pointer, Vector3> OnPointerDragEnd;
}