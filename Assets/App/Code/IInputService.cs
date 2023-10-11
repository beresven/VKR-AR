using System;
using UnityEngine.EventSystems;

namespace App.Code
{
    public interface IInputService
    {
        public event Action<PointerEventData> OnClick;
    }
}