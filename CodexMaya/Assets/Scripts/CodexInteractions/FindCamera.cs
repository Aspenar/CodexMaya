using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FindCamera : NetworkBehaviour
{
    [SerializeField] private string camera;

    private Canvas canvas;

    //USE THIS WHEN USING BUILD
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = GameObject.Find(camera).GetComponent<Camera>();
        canvas.planeDistance = 1f;
        Debug.Log("Finding Camera2...");
    }

    // USE THIS WHEN TESTING JUST THE SCENE
    /*void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = GameObject.Find(camera).GetComponent<Camera>();
        canvas.planeDistance = 2;
    }*/
}
