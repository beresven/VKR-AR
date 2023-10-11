using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace App.Code
{
    public class EditorRaycastInfo : IRaycastInfo
    {
        public Vector3 HitPoint { get; }
        public PlaneAlignment PlaneType { get; }
        public GameObject HitObject { get; }
        public Vector2 ScreenPos { get; }

        public EditorRaycastInfo(RaycastHit raycastHit,Vector2 screenPos)
        {
            HitPoint = raycastHit.point;
            HitObject = raycastHit.collider.gameObject;
            ScreenPos = screenPos;
            PlaneType = PlaneAlignment.HorizontalUp;
        }
    }
}