using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VkLinkToClipboard : MonoBehaviour
{
    public string VKLink;

    public void Copy()
    {
        GUIUtility.systemCopyBuffer = VKLink;
        Application.OpenURL(VKLink);
    }
}
