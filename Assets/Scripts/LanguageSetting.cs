using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSettings : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;  // Reference to your TMP_Dropdown (or Dropdown for regular Unity)
    private List<string> availableLanguages = new List<string>();

    public void InitializeLanguageSettings()
    {
        // Populate the dropdown with available languages
        PopulateDropdown();

        // Load the saved language index or default to 0 (English)
        int savedLanguageIndex = PlayerPrefs.GetInt("SelectedLanguage", 0);

        // Set the dropdown value to the saved language index
        languageDropdown.value = savedLanguageIndex;
        languageDropdown.RefreshShownValue();

        // Listen for when the dropdown value changes
        languageDropdown.onValueChanged.AddListener(delegate {
            OnLanguageDropdownValueChanged(languageDropdown.value);
        });
    }

    void PopulateDropdown()
    {
        // Clear any existing options in the dropdown
        languageDropdown.ClearOptions();
        availableLanguages.Clear();

        // Get the list of available locales
        var locales = LocalizationSettings.AvailableLocales.Locales;

        // Populate the dropdown with available languages
        foreach (var locale in locales)
        {
            availableLanguages.Add(locale.Identifier.CultureInfo.DisplayName);  // Use the display name of the language
        }

        // Add the language names to the dropdown options
        languageDropdown.AddOptions(availableLanguages);

        // Set the default value in the dropdown to the current locale
        int currentLocaleIndex = locales.IndexOf(LocalizationSettings.SelectedLocale);
        languageDropdown.value = currentLocaleIndex;
        languageDropdown.RefreshShownValue();
    }

    private void OnLanguageDropdownValueChanged(int index)
    {
        // We just save the selected index, but don't apply it yet.
        PlayerPrefs.SetInt("SelectedLanguage", index);
    }

    // This method will apply the selected language.
    public void ApplySelectedLanguage()
    {
        // Retrieve the saved language index
        int savedLanguageIndex = PlayerPrefs.GetInt("SelectedLanguage", 0);

        // Switch the locale
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[savedLanguageIndex];
        Debug.Log("Language applied: " + LocalizationSettings.SelectedLocale.Identifier.Code);
    }
}
