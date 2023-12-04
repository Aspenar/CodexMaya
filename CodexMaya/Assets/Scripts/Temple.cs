using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class Temple : MonoBehaviour
{
    private float speed =.5f;
    private TapGesture tap;
    private bool isTapped = false;

    [SerializeField] private Camera activeCamera;
    [SerializeField] private Transform templePosition;

    private void OnEnable()
    {
        activeCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        tap = GetComponent<TapGesture>();
        tap.Tapped += tappedHandler;
    }

    private void Update()
    {
        if (isTapped)
        {
            activeCamera.transform.position = Vector3.Lerp(activeCamera.transform.position, templePosition.position, (speed * Time.deltaTime));
        }
    }

    private void tappedHandler(object sender, System.EventArgs e)
    {
        isTapped = true;
    }
}
