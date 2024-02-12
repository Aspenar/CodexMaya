using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : NetworkBehaviour
{
    private GameObject[] positions;

    private Camera _camera;
    //[SerializeField] private GameObject _ownerUI;

    private bool clientExist = false;

    private void Awake()
    {
        _camera = gameObject.GetComponent<Camera>();
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
        gameObject.name = "Host";
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

        if (!IsOwner)
        {
            gameObject.transform.parent = GameObject.Find("Host").transform;
        }
    }

    

}
