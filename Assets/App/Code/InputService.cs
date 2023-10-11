using System;
using UnityEngine;
using UnityEngine.EventSystems;
//using static UnityEditor.Experimental.RestService.PlayerDataFileLocator;

namespace App.Code
{
    public class InputService : MonoBehaviour,
        IPointerClickHandler, IInputService
    {
        public event Action<PointerEventData> OnClick;

        // private void Awake() =>
        //     Locator.Set<IInputService>(this);

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(eventData);
        }
    }
}