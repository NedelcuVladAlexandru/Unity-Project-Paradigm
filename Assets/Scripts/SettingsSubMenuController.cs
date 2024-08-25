using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsMenuController : MonoBehaviour
{
    public ResolutionSettings resolutionSettings;
    public ScreenTypeSettings screenTypeSettings;
    public GameObject mainMenu;

    private void Start()
    {
        resolutionSettings.InitializeResolutionOptions();
        screenTypeSettings.InitializeScreenModeOptions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu(); // Allowing the user to return from Settings with escape.
        }
    }

    public void ApplySettings()
    {
        screenTypeSettings.SetScreenMode(screenTypeSettings.screenModeDropdown.value);
        resolutionSettings.SetResolution(resolutionSettings.resolutionDropdown.value);
    }

    public void BackToMainMenu()
    {
        // Logic to hide settings and show main menu
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
