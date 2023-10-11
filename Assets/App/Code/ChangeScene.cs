using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("GalleryMenu", LoadSceneMode.Single);
    }
}
