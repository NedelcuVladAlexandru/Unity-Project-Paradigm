using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;

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