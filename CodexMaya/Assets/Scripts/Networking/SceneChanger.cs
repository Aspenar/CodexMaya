using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : NetworkBehaviour
{
    [SerializeField] private string m_SceneName;
    private Scene m_LoadedScene;

    NetworkSceneManager m_NetworkSceneManager;

    /*private void Awake()
    {
        //m_NetworkSceneManager.OnSceneEvent += SceneManager_OnSceneEvent;
        Debug.Log(NetworkManager.Singleton);
        Debug.Log(NetworkManager.Singleton.SceneManager);
        //m_NetworkSceneManager = NetworkManager.Singleton.SceneManager;

        if (m_NetworkSceneManager != null)
        {
            m_NetworkSceneManager.OnSceneEvent += SceneManager_OnSceneEvent;
        }
    }*/

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        /*Debug.Log(NetworkManager.Singleton);
        Debug.Log(NetworkManager.Singleton.SceneManager);*/
        m_NetworkSceneManager = NetworkManager.Singleton.SceneManager;

        if (m_NetworkSceneManager != null)
        {
            m_NetworkSceneManager.OnSceneEvent += SceneManager_OnSceneEvent;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject.Find("Player 1").GetComponent<Camera>().enabled = false;
            NextSceneServerRpc();
            if (IsServer && !string.IsNullOrEmpty(m_SceneName))
            {
                UnloadScene();
            }
        }
    }

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

    [ServerRpc]
    private void NextSceneServerRpc()
    {
        LoadNextScene();

        //NextSceneClientRpc();
    }

    /*[ClientRpc]
    private void NextSceneClientRpc()
    {
        LoadNextScene();
    }*/

    private void LoadNextScene()
    {
        UnloadScene();
        var status = m_NetworkSceneManager.LoadScene(m_SceneName, LoadSceneMode.Single);
        if (status != SceneEventProgressStatus.Started)
        {
            Debug.LogWarning($"Failed to load {m_SceneName} " +
                  $"with a {nameof(SceneEventProgressStatus)}: {status}");
        }
    }

    public void UnloadScene()
    {
        GameObject.Find("Player 1").GetComponent<Camera>().enabled = true;
        // Assure only the server calls this when the NetworkObject is
        // spawned and the scene is loaded.
        if (!IsServer || !IsSpawned || !m_LoadedScene.IsValid() || !m_LoadedScene.isLoaded)
        {
            return;
        }

        // Unload the scene
        var status = m_NetworkSceneManager.UnloadScene(m_LoadedScene);
        CheckStatus(status, false);
        GameObject.Find("Player 1").GetComponent<Camera>().enabled = true;

    }

}
