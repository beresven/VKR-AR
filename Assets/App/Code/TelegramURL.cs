using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelegramURL : MonoBehaviour
{
    public string TelegramLink;

    public void OnClick()
    {
        GUIUtility.systemCopyBuffer = TelegramLink;
        Application.OpenURL(TelegramLink);
    }
}
