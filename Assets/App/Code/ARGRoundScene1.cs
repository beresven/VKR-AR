using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARGRoundScene1 : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("ARGroundManual", LoadSceneMode.Single);
    }
}
