using UnityEngine;
using UnityEngine.UI;

public class TopCameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    
    Slider slider1;
    public GameObject CameraToMove;
    public Quaternion Rotation;

    void Awake()
    {
        slider1 = GetComponentInParent<Slider>();
    }

    public void Move()
    {
        CameraToMove.GetComponent<Camera>().orthographicSize = slider1.value;
    }
}
