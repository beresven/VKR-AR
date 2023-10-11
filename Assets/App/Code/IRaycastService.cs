using UnityEngine;

namespace App.Code
{
    public interface IRaycastService
    {
        public IRaycastInfo Raycast(Vector2 screenPos);
    }
}