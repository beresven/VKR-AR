using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
    Slider slider;
    public GameObject ObjectToRotate;
    public Quaternion Rotation;

    void Awake()
    {
        slider = GetComponentInParent<Slider>();
    }

    public void Rotate()
    {
        ObjectToRotate.transform.localEulerAngles = new Vector3(0f, slider.value, 0f);
    }


}
