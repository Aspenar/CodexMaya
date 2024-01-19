using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : NetworkBehaviour
{
    [SerializeField] private string m_SceneName;
    private Scene m_LoadedScene;

    private GameObject[] positions;

    [SerializeField] private Camera _camera;
    //[SerializeField] private GameObject _ownerUI;

    public bool SceneIsLoaded
    {
        get
        {
            if (m_LoadedScene.IsValid() && m_LoadedScene.isLoaded)
            {
                return true;
            }
            return false;
        }
    }

    private void CheckStatus(SceneEventProgressStatus status, bool isLoading = true)
    {
        var sceneEventAction = isLoading ? "load" : "unload";
        if (status != SceneEventProgressStatus.Started)
        {
            Debug.LogWarning($"Failed to {sceneEventAction} {m_SceneName} with" +
                $" a {nameof(SceneEventProgressStatus)}: {status}");
        }
    }

    private void SceneManager_OnSceneEvent(SceneEvent sceneEvent)
    {
        var clientOrServer = sceneEvent.ClientId == NetworkManager.ServerClientId ? "server" : "client";
        switch (sceneEvent.SceneEventType)
        {
            case SceneEventType.LoadComplete:
                {
                    // We want to handle this for only the server-side
                    if (sceneEvent.ClientId == NetworkManager.ServerClientId)
                    {
                        // *** IMPORTANT ***
                        // Keep track of the loaded scene, you need this to unload it
                        m_LoadedScene = sceneEvent.Scene;
                    }
                    Debug.Log($"Loaded the {sceneEvent.SceneName} scene on " +
                        $"{clientOrServer}-({sceneEvent.ClientId}).");
                    break;
                }
            case SceneEventType.UnloadComplete:
                {
                    Debug.Log($"Unloaded the {sceneEvent.SceneName} scene on " +
                        $"{clientOrServer}-({sceneEvent.ClientId}).");
                    break;
                }
            case SceneEventType.LoadEventCompleted:
            case SceneEventType.UnloadEventCompleted:
                {
                    var loadUnload = sceneEvent.SceneEventType == SceneEventType.LoadEventCompleted ? "Load" : "Unload";
                    Debug.Log($"{loadUnload} event completed for the following client " +
                        $"identifiers:({sceneEvent.ClientsThatCompleted})");
                    if (sceneEvent.ClientsThatTimedOut.Count > 0)
                    {
                        Debug.LogWarning($"{loadUnload} event timed out for the following client " +
                            $"identifiers:({sceneEvent.ClientsThatTimedOut})");
                    }
                    break;
                }
        }
    }
    private void Awake()
    {
        //InitializeSpawnPoints();
        if (GameObject.FindGameObjectsWithTag("Spawn").Length > 0)
        {
            //Find all game objects tagged 'Spawn' and put them in the array
            positions = GameObject.FindGameObjectsWithTag("Spawn");

            for (int i = 0; i < positions.Length; i++)//Sorts positions by name because why would they automatically put them in alphabetically?
            {
                Array.Sort(positions, (x, y) => string.Compare(x.name, y.name));
            }
            Debug.Log(positions[0]);

        }
        else
        {
            Debug.Log("All spawn points taken up...");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        SpawnPlayer();
        if (!IsOwner) return;
        _camera.enabled = true;
        //_ownerUI.SetActive(true);
    }

    private void SpawnPlayer()
    {
        //When the player spawns, assign them to a position in the array
        for (int i = 0; i < positions.Length; i++)
        {
            //If we find an active position in the hierarcy, place player
            if (positions[i].gameObject.activeInHierarchy)
            {
                Debug.Log(positions[i].name + " is available...");
                gameObject.transform.position = positions[i].transform.position;
                gameObject.transform.rotation = positions[i].transform.rotation;
                positions[i].gameObject.SetActive(false);
                //playerInPlace = true;
                break;
            }
        }
    }

    private void Update()
    {
        if(!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (IsServer && !string.IsNullOrEmpty(m_SceneName))
            {
                UnloadScene();

                var status = NetworkManager.Singleton.SceneManager.LoadScene(m_SceneName, LoadSceneMode.Single);
                if (status != SceneEventProgressStatus.Started)
                {
                    Debug.LogWarning($"Failed to load {m_SceneName} " +
                          $"with a {nameof(SceneEventProgressStatus)}: {status}");
                }

            }
        }
    }

    public void UnloadScene()
    {
        // Assure only the server calls this when the NetworkObject is
        // spawned and the scene is loaded.
        if (!IsServer || !IsSpawned || !m_LoadedScene.IsValid() || !m_LoadedScene.isLoaded)
        {
            return;
        }

        // Unload the scene
        var status = NetworkManager.SceneManager.UnloadScene(m_LoadedScene);
        CheckStatus(status, false);
    }
}
