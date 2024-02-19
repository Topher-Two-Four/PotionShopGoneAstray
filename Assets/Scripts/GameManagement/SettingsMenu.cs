using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void ToggleFullScreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    public void ToggleMaxWindowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }
}