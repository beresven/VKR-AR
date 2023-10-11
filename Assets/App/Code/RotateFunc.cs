using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFunc : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject instance;
    public void OnClick()
    {
        Instantiate(instance);
    }
}
