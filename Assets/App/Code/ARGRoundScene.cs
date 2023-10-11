using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARGRoundScene : MonoBehaviour
{
    public List<GameObject> Objects;
    public List<Sprite> ObjectIcons;
    public GameObject Set;
    // private void Start() {
    //     GetComponent<Image>().SetNativeSize();
    // }
    public void OnClick()
    {
        //Debug.Log();
        PlayerPrefs.SetString("set", Set.gameObject.name);
        SceneManager.LoadScene("ARGround", LoadSceneMode.Single);      
    }
}