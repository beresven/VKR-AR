using App.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    public GameObject obj;
    public GameObject PlaceManager;

    public void OnClick()
    {
        PlaceManager = GameObject.Find("AR Place Object");
        PlaceManager.GetComponent<ARPlaceObject>().ObjectToPlace = obj;
    }
}
