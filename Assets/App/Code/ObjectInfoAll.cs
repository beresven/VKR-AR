using App.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfoAll : MonoBehaviour
{
    // public GameObject obj;
    public List<GameObject> objs;
    public GameObject PlaceManager;

    public void OnClick()
    {
        PlaceManager = GameObject.Find("AR Place Object");
        PlaceManager.GetComponent<ARPlaceObjectAll>().ObjectToPlace = objs[0];
    }

    public void NextObject(int indexObjectToSpawn){
        PlaceManager = GameObject.Find("AR Place Object");
        PlaceManager.GetComponent<ARPlaceObjectAll>().ObjectToPlace = objs[indexObjectToSpawn];
    }
}
