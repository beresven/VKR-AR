using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace App.Code
{
    public class ARRaycastInfo : IRaycastInfo
    {
        public Vector3 HitPoint { get; }
        public PlaneAlignment PlaneType { get; }
        public GameObject HitObject { get; }
        public Vector2 ScreenPos { get; }

        public ARRaycastInfo(ARRaycastHit raycastHit, Vector2 screenPos)
        {
            HitPoint = raycastHit.pose.position;
            PlaneType = raycastHit.trackable is ARPlane plane
                ? plane.alignment
                : PlaneAlignment.None;
                
            HitObject = raycastHit.trackable.gameObject;
            ScreenPos = screenPos;
        }
    }
}