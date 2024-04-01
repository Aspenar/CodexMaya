using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCamera : MonoBehaviour
{
    [SerializeField] private string camera;

    private Canvas canvas;

    //USE THIS WHEN USING BUILD
    /*public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
canvas = GetComponent<Canvas>();
        canvas.worldCamera = GameObject.Find(camera).GetComponent<Camera>();
        canvas.planeDistance = 2;    }*/

    // USE THIS WHEN TESTING JUST THE SCENE
    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = GameObject.Find(camera).GetComponent<Camera>();
        canvas.planeDistance = 2;
    }
}
