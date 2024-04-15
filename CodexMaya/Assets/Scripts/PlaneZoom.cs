using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlaneZoom : NetworkBehaviour
{
    //public GameObject greyOut;
    private bool clicked = false;
    private bool isEnlarged = false;

    public GameObject ExploreUI;

    public CodexTezcatilpoca codexTezcatilpoca;

    public float lerpSpeed;
    [SerializeField] private Transform planeTransform;
    private Transform originalTransform;
    private Transform target;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

    }

    /*private void Awake()
    {
        GameObject greyOut = GameObject.FindGameObjectWithTag("greyOut");
    }*/

    private void Update()
    {
        //Enlarge plane
        if (clicked)
        {
            Debug.Log("1");

            /*if (!isEnlarged)
            {*/
            //Debug.Log("2");

            transform.position = Vector3.Lerp(transform.position, planeTransform.position, Time.deltaTime * lerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, planeTransform.rotation, Time.deltaTime * lerpSpeed);
            //Debug.Log(currentTransform.position);
            codexTezcatilpoca.pageTurner.SetActive(false);
            ExploreUI.SetActive(true);
            //isEnlarged = true;
            //greyOut.gameObject.SetActive(true);
            //}
        }
        //Minimize plane
        else if (!clicked)
        {
            Debug.Log("2");

            /*if (isEnlarged)
            {*/
            //Debug.Log("4");
            transform.position = Vector3.Lerp(transform.position, originalTransform.position, Time.deltaTime * lerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalTransform.rotation, Time.deltaTime * lerpSpeed);
            codexTezcatilpoca.pageTurner.SetActive(true);
            ExploreUI.SetActive(false);

            //isEnlarged = false;
            //greyOut.SetActive(false);
            //}
        }

    }

    public void Clicked()
    {
        if (clicked)
        {
            clicked = false;
        }
        else if (!clicked)
        {
            originalTransform = transform;
            //Debug.Log(originalTransform.position);
            clicked = true;
        }
        Debug.Log("Panel is: " + clicked);
    }

}
