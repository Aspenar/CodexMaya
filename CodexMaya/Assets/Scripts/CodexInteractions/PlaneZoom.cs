using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.UI;

public class PlaneZoom : NetworkBehaviour
{
    //this script allows the player to enlarge the plane by tapping on it, runs extremely slowly but it does eventually enlarge the plane, scene change from this is a little difficult but being worked on
    //public GameObject greyOut;
    private bool clicked = false;
    //private bool isEnlarged = false;

    public GameObject ExploreUI;

    public CodexTezcatilpoca codexTezcatilpoca;

    public float lerpSpeed;
    [SerializeField] private Transform planeTransform;
    private Transform originalTransform;
    private Transform target;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        originalTransform = transform;

    }

    /*private void Awake()
    {
        GameObject greyOut = GameObject.FindGameObjectWithTag("greyOut");
    }*/

    private void Update()
    {
        if (!codexTezcatilpoca.isAnimating)
        {
            //Enlarge plane
            if (clicked)
            {
                
                transform.position = Vector3.Lerp(transform.position, planeTransform.position, Time.deltaTime * lerpSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, planeTransform.rotation, Time.deltaTime * lerpSpeed);

            }
            //Minimize plane
            else if (!clicked)
            {
                transform.position = Vector3.Lerp(transform.position, originalTransform.position, Time.deltaTime * lerpSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, originalTransform.rotation, Time.deltaTime * lerpSpeed);

            }
        }
    }

    public void Clicked()
    {
        if (clicked)
        {
            Debug.Log("2");
            codexTezcatilpoca.pageTurner.SetActive(true);
            ExploreUI.SetActive(false);
            GetComponent<NetworkTransform>().enabled = true;

            clicked = false;
        }
        else if (!clicked)
        {
            Debug.Log("1");
            codexTezcatilpoca.pageTurner.SetActive(false);
            ExploreUI.SetActive(true);
            GetComponent<NetworkTransform>().enabled = false;

            originalTransform.position = transform.position;
            originalTransform.rotation = transform.rotation;
            //Debug.Log(originalTransform.position);
            clicked = true;
        }
        Debug.Log("Panel is: " + clicked);
    }

}
