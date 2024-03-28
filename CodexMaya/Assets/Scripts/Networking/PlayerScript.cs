using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Mirror;

public class PlayerScript : NetworkBehaviour
{
    private GameObject[] positions;

    private Camera _camera;
    private ScreenPixelCorrector pixelCorrector;

    private static int playerCount = 0;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        _camera = GetComponent<Camera>();
        FindSpawnPos();

        SpawnPlayer();

        if (!IsOwner) return;
        _camera.enabled = true;
        GetComponent<OVRScreenFade>().FadeIn();

        //gameObject.name = "Host";
        //GetComponent<OVRScreenFade>().FadeIn();
        //_ownerUI.SetActive(true);
    }

    public void FindSpawnPos()
    {
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

    private void SpawnPlayer()
    {
        playerCount++;

        /*switch (playerCount)
        {
            case 1:
                pixelCorrector.RearangeScreenSize(7266);
                break;
            case 2:
                pixelCorrector.RearangeScreenSize(6065);
                break;
            case 3:
                pixelCorrector.RearangeScreenSize(8480);
                break;
        }*/


        //When the player spawns, assign them to a position in the array
        for (int i = 0; i < positions.Length; i++)
        {
            //If we find an active position in the hierarcy, place player
            if (positions[i].gameObject.activeInHierarchy)
            {

                Debug.Log(positions[i].name + " is available...");
                gameObject.transform.position = positions[i].transform.position;
                gameObject.transform.rotation = positions[i].transform.rotation;



                gameObject.name = "Player " + playerCount.ToString(); 
                positions[i].gameObject.SetActive(false);
                //playerInPlace = true;
                break;
            }
        }

        /*if (!IsOwner)
        {
            gameObject.transform.parent = GameObject.Find("Host").transform;
        }*/
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        playerCount--;
    }

}
