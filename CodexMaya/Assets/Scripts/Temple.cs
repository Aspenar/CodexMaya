using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using Cinemachine;

public class Temple : MonoBehaviour
{
    private float speed =.5f;
    private TapGesture tap;
    private bool isTapped = false;

    [SerializeField] private Camera activeCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera1;
    [SerializeField] private CinemachineVirtualCamera virtualCamera2;
    [SerializeField] private CinemachineVirtualCamera virtualCamera3;

    [SerializeField] private Transform templePosition;

    // This is the time it takes the animation to play then switch to the next camera
    public float vCam2Time = 6f;

    private void OnEnable()
    {
        activeCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        virtualCamera1 = GameObject.Find("Virtual Camera 1").GetComponent<CinemachineVirtualCamera>();
        virtualCamera2 = GameObject.Find("Virtual Camera 2").GetComponent<CinemachineVirtualCamera>();
        virtualCamera3 = GameObject.Find("Virtual Camera 3").GetComponent<CinemachineVirtualCamera>();

        virtualCamera2.gameObject.SetActive(false);
        virtualCamera3.gameObject.SetActive(false);
        
        tap = GetComponent<TapGesture>();
        tap.Tapped += tappedHandler;
    }

    private void Update()
    {
        if (isTapped)
        {
            // Old movement system that lerps the camera in a straight line
            // activeCamera.transform.position = Vector3.Lerp(activeCamera.transform.position, templePosition.position, (speed * Time.deltaTime));

            // New CineMachine camera movement that makes the camera follow a smooth path
            StartCoroutine(SwitchCamerasWithDelay());
            Debug.Log("CorotutineStarted");

            
        }
    }

    private void tappedHandler(object sender, System.EventArgs e)
    {
        isTapped = true;
    }

    IEnumerator SwitchCamerasWithDelay() {
        // Activate Virtual Camera 2
        virtualCamera2.gameObject.SetActive(true);
        virtualCamera1.gameObject.SetActive(false);
        Debug.Log("Virtual Camera 2 Active");

        // Wait for the specified delay
        yield return new WaitForSeconds(vCam2Time);

        // Activate Virtual Camera 3 after the delay
        virtualCamera3.gameObject.SetActive(true);
        virtualCamera2.gameObject.SetActive(false);
        Debug.Log("Virtual Camera 3 Active");
    }
}
