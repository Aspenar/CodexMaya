using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CubeTap : MonoBehaviour
{
    public float Force = 5f;

    private TapGesture tap;
    private Rigidbody rb;
    private Camera activeCamera;

    private void OnEnable()
    {
        activeCamera = GameObject.Find("Host").GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        tap = GetComponent<TapGesture>();
        tap.Tapped += tappedHandler;
    }

    private void tappedHandler(object sender, System.EventArgs e)
    {
        var ray = activeCamera.ScreenPointToRay(tap.ScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            rb.AddForceAtPosition(ray.direction * Force, hit.point, ForceMode.Impulse);
        }
    }
}
