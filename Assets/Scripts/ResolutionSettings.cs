using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionSettings : MonoBehaviour
{
    public TMP_Dropdown screenModeDropdown;
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<string> options = new List<string>();
    private List<int> resolutionIndices = new List<int>();

    public void InitializeResolutionOptions()
    {
        // Clear the dropdown options and ensure no duplicates
        resolutionDropdown.ClearOptions();
        options.Clear();
        resolutionIndices.Clear();

        // Get all available screen resolutions
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        // Dictionary to ensure we only add unique resolutions
        Dictionary<string, int> uniqueResolutions = new Dictionary<string, int>();

        // Populate the dropdown with available resolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            // Add only unique resolutions and ensure they are for the current display
            if (!uniqueResolutions.ContainsKey(option))
            {
                uniqueResolutions[option] = i;
                options.Add(option);
                resolutionIndices.Add(i);

                // Check if this resolution is the current screen resolution
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = options.Count - 1;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int dropdownIndex)
    {
        int resolutionIndex = resolutionIndices[dropdownIndex];
        Resolution resolution = resolutions[resolutionIndex];

        switch (screenModeDropdown.value)
        {
            case 0: // Fullscreen
                Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);
                break;
            case 1: // Borderless Windowed
                Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);
                break;
            case 2: // Windowed
                Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.Windowed);
                break;
        }

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
}
