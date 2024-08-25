using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentSceneInt = currentScene.buildIndex;

            // Protection for going over existing number of scenes
            int nextSceneBuildIndex = (currentSceneInt + 1) % SceneManager.sceneCountInBuildSettings;

            // Protection from switching to the BootStrap scene
            if (nextSceneBuildIndex == 0)
            {
                nextSceneBuildIndex++;
            }

            SwitchScene(nextSceneBuildIndex);
        }
    }

    public void SwitchScene(int sceneBuildIndex)
    {
        StartCoroutine(TransitionScene(sceneBuildIndex));
    }

    private IEnumerator TransitionScene(int sceneBuildIndex)
    {
        // Load the new scene additively
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Unload the previous scene
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentScene.buildIndex);
        while (!asyncUnload.isDone)
        {
            yield return null;
        }

        // Ensure player is correctly instantiated in the new scene
        PlayerManager.Instance.GetPlayer().SetActive(true); // Ensure the player remains active
    }
}
