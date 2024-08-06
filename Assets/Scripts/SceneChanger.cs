using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    public string targetSceneName; // TODO: Change this so it can dynamically go between 2-3 scenes.
    public GameObject player; // Reference to the Player GameObject

    private Vector3 savedPosition;
    private Quaternion savedRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchScene(targetSceneName);
        }
    }

    public void SwitchScene(string sceneName)
    {
        StartCoroutine(TransitionScene(sceneName));
    }

    private IEnumerator TransitionScene(string sceneName)
    {
        // Save player transform data
        SavePlayerTransform();

        // Load new scene additively
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Unload previous scene
        string currentSceneName = SceneManager.GetActiveScene().name;
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
