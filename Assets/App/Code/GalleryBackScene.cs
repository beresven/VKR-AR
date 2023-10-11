using UnityEngine;
using UnityEngine.SceneManagement;

public class GalleryBackScene : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}