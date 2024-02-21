using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TransportPlayerToPoint : NetworkBehaviour
{
    private List<GameObject> players;
    private OVRScreenFade fade;

    [SerializeField] private List<Transform> positions;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        players = new List<GameObject>();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("MainCamera").Length; i++)
        {
            GameObject player = GameObject.Find("Player " + (i + 1));
            Debug.Log(player);
            players.Add(player);
            players[i].GetComponent<PlayerScript>().FindSpawnPos();
            Debug.Log(players[i]);
        }

        MovePlayer();
    }

    public void MovePlayer()
    {
        StartCoroutine(StartFade());

    }

    IEnumerator StartFade()
    {
        for (int i = 0; i < players.Count; i++)
        {
            fade = players[i].GetComponent<OVRScreenFade>();
            fade.FadeOut();
            
            yield return new WaitForSeconds(1);

            for (int y = 0; y < positions.Count; y++)
            {
                //If we find an active position in the hierarcy, place player
                if (positions[y].gameObject.activeInHierarchy)
                {
                    Debug.Log(positions[y].name + " is available...");
                    players[i].transform.position = positions[y].transform.position;
                    players[i].transform.rotation = positions[y].transform.rotation;
                    positions[i].gameObject.SetActive(false);
                    break;
                }
            }
            //player.transform.position = gameObject.transform.position;
            //player.transform.rotation = gameObject.transform.rotation;
            fade.FadeIn();
        }

    }
}
