using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class SwipeTest : MonoBehaviour
{
    private FlickGesture _flick;
    public Image panelImage;

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
                panelImage.color = Color.red;
            }
            else if (dotProduct < -0.1f)
            {
                panelImage.color = Color.green;
            }

        }
    }
}
