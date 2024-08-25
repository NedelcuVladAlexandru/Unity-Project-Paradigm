using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public GameObject playerPrefab;

    private GameObject playerInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure this object persists across scenes
            InstantiatePlayer(); // Instantiate the player when the game starts
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InstantiatePlayer()
    {
        if (playerInstance == null && playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab);
            DontDestroyOnLoad(playerInstance); // Ensure the player persists across scenes
        }
    }

    public GameObject GetPlayer()
    {
        return playerInstance;
    }
}
