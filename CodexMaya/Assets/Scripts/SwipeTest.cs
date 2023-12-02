using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class SwipeTest : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private FlickGesture _flick;
    public Image panelImage;
    private float rotateRight = 90.0f;
    private float rotateLeft = -90.0f;
    private float currentRotate = 0.0f;
    private Vector3 Rotation;


    private void OnEnable()
    {
        _flick = GetComponent<FlickGesture>();
        _flick.Flicked += flickedHandler;
    }

    private void flickedHandler(object sender, System.EventArgs e)
    {
        //sets the minimum flick time to unlimted so a quick flick isn't required
        _flick.FlickTime = Time.time;
        Debug.Log(_flick.ScreenFlickVector.normalized);

        // Calculate the normalized flick vector.
        Vector2 normalizedFlickVector = _flick.ScreenFlickVector.normalized;
        if (normalizedFlickVector.magnitude >= 0.1f)
        {
            float dotProduct = Vector2.Dot(normalizedFlickVector, Vector2.right);

            if (dotProduct > 0.1f)
            {
                 currentRotate += rotateRight;
                player.transform.rotation = Quaternion.Euler(new Vector3 (0, currentRotate, 0));
               
            }
            else if (dotProduct < -0.1f)
            {
                 currentRotate += rotateLeft;
                player.transform.rotation = Quaternion.Euler(new Vector3 (0, currentRotate, 0));
                //panelImage.color = Color.green;
            }

        }
    }
}
