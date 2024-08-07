using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject player; // Reference to the Player GameObject

    private Vector3 savedPosition;
    private Quaternion savedRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentSceneInt = currentScene.buildIndex;

            // Protection for going over existing number of scenes
            int nextSceneBuildIndex = (currentSceneInt + 1) % SceneManager.sceneCountInBuildSettings;

            SwitchScene(nextSceneBuildIndex);
        }
    }

    public void SwitchScene(int SceneBuildIndex)
    {
        StartCoroutine(TransitionScene(SceneBuildIndex));
    }

    private IEnumerator TransitionScene(int SceneBuildIndex)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Save player transform data
        SavePlayerTransform();

        // Load new scene additively
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneBuildIndex, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Unload previous scene
        SceneManager.UnloadSceneAsync(currentSceneName);

        // Restore player transform data
        RestorePlayerTransform();
    }

    private void SavePlayerTransform()
    {
        savedPosition = player.transform.position;
        savedRotation = player.transform.rotation;
    }

    private void RestorePlayerTransform()
    {
        player.transform.position = savedPosition;
        player.transform.rotation = savedRotation;
    }
}
