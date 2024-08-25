using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsMenuController : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public ResolutionSettings resolutionSettings;
    public GameObject mainMenu;

    void Start()
    {
        resolutionSettings.InitializeResolutionOptions();
    }

    public void ApplySettings()
    {
        resolutionSettings.SetResolution(resolutionDropdown.value);
    }

    public void BackToMainMenu()
    {
        // Logic to hide settings and show main menu
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
