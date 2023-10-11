using UnityEngine;
using UnityEngine.UI;

public class ScaleObject : MonoBehaviour
{
    // Start is called before the first frame update
    
    Slider slider1;
    public GameObject ObjectToScale;
    public Quaternion Rotation;

    void Awake()
    {
        slider1 = GetComponentInParent<Slider>();
    }

    public void Scale()
    {
        ObjectToScale.transform.localScale = new Vector3(slider1.value, slider1.value, slider1.value);
    }
}
