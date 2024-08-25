using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenTypeSettings : MonoBehaviour
{
    public TMP_Dropdown screenModeDropdown;

    public void InitializeScreenModeOptions()
    {
        // Clear existing options and add new ones
        screenModeDropdown.ClearOptions();
        List<string> options = new List<string>
        {
            "Fullscreen",
            "Borderless Windowed",
            "Windowed"
        };

        screenModeDropdown.AddOptions(options);

        // Set the current screen mode in the dropdown
        int currentMode = Screen.fullScreen ? 0 : (Screen.fullScreenMode == FullScreenMode.FullScreenWindow ? 1 : 2);
        screenModeDropdown.value = currentMode;
        screenModeDropdown.RefreshShownValue();
    }

    public void SetScreenMode(int index)
    {
        switch (index)
        {
            case 0: // Fullscreen
                Screen.fullScreen = true;
                break;
            case 1: // Borderless Windowed
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
                break;
            case 2: // Windowed
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.Windowed);
                break;
        }
    }
}
