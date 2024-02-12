using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPlayerToPoint : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Host");
    }

    public void MovePlayer()
    {
        player.transform.position = gameObject.transform.position;
        player.transform.rotation = gameObject.transform.rotation;
    }
}
