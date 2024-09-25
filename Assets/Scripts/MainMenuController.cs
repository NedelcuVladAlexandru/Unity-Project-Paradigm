using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;

    public void Start()
    {
        // Load the saved language index or default to 0 (English)
        int savedLanguageIndex = PlayerPrefs.GetInt("SelectedLanguage", 0);

        // Set the localization based on saved preference
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[savedLanguageIndex];
    }

    // Loads the main scene of the title
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene"); // TODO: Add some fade screen for transition
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR_WIN
        EditorApplication.ExitPlaymode();
#endif 
    }
}