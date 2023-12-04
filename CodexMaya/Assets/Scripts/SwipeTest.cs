using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SwipeTest : MonoBehaviour
{
   
    [SerializeField] private GameObject player;
    private FlickGesture _flick;
    //public Image panelImage; <-- althought this isn't needed the canvas has to have something in it in order for swipe to work
    
    
    private float rotateRight = 90.0f;
    private float rotateLeft = -90.0f;
    private float currentRotate;
    private float speed = .8f;
    private bool _turn = false;

    private void OnEnable()
    {
        _flick = GetComponent<FlickGesture>();
        _flick.Flicked += flickedHandler;
    }

    private void Update()
    {
        if(_turn)
        {
            // Interpolate the player's rotation towards the target rotation
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.Euler(0, currentRotate, 0), speed * Time.deltaTime);
            
            // Check if the rotation is close enough to the target rotation
            if (Quaternion.Angle(player.transform.rotation, Quaternion.Euler(0, currentRotate, 0)) < 1.0f)
            {
                _turn = false; // Set _turn back to false when rotation is close enough
            }
        }
    }

    private void flickedHandler(object sender, System.EventArgs e)
    {
        //Sets the minimum flick time to unlimted so a quick flick isn't required
        _flick.FlickTime = Time.time;
        Debug.Log("flicked");
        // Calculate the normalized flick vector.
        Vector2 normalizedFlickVector = _flick.ScreenFlickVector.normalized;
        if (normalizedFlickVector.magnitude >= 0.1f)
        {
            float dotProduct = Vector2.Dot(normalizedFlickVector, Vector2.right);

            if (dotProduct > 0.1f)
            {
                // Increment the desired rotation for a right swipe
                currentRotate += rotateRight;
                Debug.Log("Right");
                // Set _turn to true to initiate the rotation in the Update method
                _turn = true;
            }
            else if (dotProduct < -0.1f)
            {
                //Same as rotateRight
                currentRotate += rotateLeft;
                Debug.Log("Left");

                _turn = true;
            }

        }
    }
}
