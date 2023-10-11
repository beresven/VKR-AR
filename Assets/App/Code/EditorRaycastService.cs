using UnityEngine;

namespace App.Code
{
    public class EditorRaycastService : MonoBehaviour,
        IRaycastService
    {
        public LayerMask EditorPlaneLayer;

        public IRaycastInfo Raycast(Vector2 screenPos)
        {
            var result = Physics.Raycast(Camera.main.ScreenPointToRay(screenPos), out var raycastInfo, 50.0f);
            return result
                ? new EditorRaycastInfo(raycastInfo, screenPos)
                : null;
        }
    }
}