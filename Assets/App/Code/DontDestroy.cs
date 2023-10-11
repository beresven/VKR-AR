using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update

    public int NumOfSets = 4;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("set");
        if (objs.Length > NumOfSets)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
