using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Sets;
    public Button button;
    void Start()
    {
        foreach (var _set in Sets)
        {
            button.GetComponent<Image>().sprite = _set.GetComponent<ObjectsSet>().SetIcon;
            button.GetComponent<ARGRoundScene>().Set = _set;
            Instantiate(button).transform.parent = transform;

        }
    }

}
