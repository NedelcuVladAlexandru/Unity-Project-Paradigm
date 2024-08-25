using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsMenuController : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public GameObject mainMenu;
    private Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        // Populate dropdown with available resolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Check if this is the current resolution
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options); 
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            SetResolution(resolutionDropdown.value);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        AdjustCameraViewport(); 
    }

    private void AdjustCameraViewport()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float targetAspect = 16.0f / 9.0f; // Define target aspect ratio
            float windowAspect = (float)Screen.width / (float)Screen.height; // Calculate current aspect ratio
            float scaleHeight = windowAspect / targetAspect; // Determine scale height

            if (scaleHeight < 1.0f)
            {
                Rect rect = mainCamera.rect;

                rect.width = 1.0f;
                rect.height = scaleHeight;
                rect.x = 0;
                rect.y = (1.0f - scaleHeight) / 2.0f;

                mainCamera.rect = rect; 
            }
            else
            {
                float scaleWidth = 1.0f / scaleHeight;

                Rect rect = mainCamera.rect;

                rect.width = scaleWidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scaleWidth) / 2.0f;
                rect.y = 0;

                mainCamera.rect = rect;
            }
        }
    }

    public void ApplySettings()
    {
        SetResolution(resolutionDropdown.value);
    }

    public void BackToMainMenu()
    {
        // Logic to hide settings and show main menu
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
