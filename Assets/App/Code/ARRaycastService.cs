using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace App.Code
{
    public class ARRaycastService : IRaycastService
    {
        private readonly ARRaycastManager _raycastManager;
        private readonly List<ARRaycastHit> _hits;

        public ARRaycastService(ARRaycastManager raycastManager)
        {
            _raycastManager = raycastManager;
            _hits = new List<ARRaycastHit>();
        }

        public IRaycastInfo Raycast(Vector2 screenPos)
        {
            var result = _raycastManager.Raycast(screenPos, _hits, TrackableType.PlaneWithinBounds);
            return result
                ? new ARRaycastInfo(_hits[0], screenPos)
                : null;
        }
    }
}