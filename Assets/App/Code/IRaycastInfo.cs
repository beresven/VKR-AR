using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace App.Code
{
    public interface IRaycastInfo
    {
        public Vector3 HitPoint { get; }
        public PlaneAlignment PlaneType { get; }
        public GameObject HitObject { get; }
        public Vector2 ScreenPos { get; }
    }
}