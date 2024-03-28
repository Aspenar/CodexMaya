using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaneZoom : MonoBehaviour
{
    private bool clicked = false;
    private bool isEnlarged = false;

    public string enlargeAnim;
    public string minimizeAnim;

    private Animator anim;
/*    private Renderer rend;
    public Material mat1;
    public Material mat2;
*/
    private void Awake()
    {
        anim = GetComponent<Animator>();
/*        rend = GetComponent<Renderer>();
        mat1 = rend.material;
*/    }

    private void Update()
    {
        if (clicked)
        {
            if (!isEnlarged)
            {
                anim.Play(enlargeAnim);
/*                rend.material = mat2;
*/                isEnlarged = true;
            }
        }
        else if (!clicked)
        {
            if (isEnlarged)
            {
                anim.Play(minimizeAnim);
/*                rend.material = mat1;
*/                isEnlarged = false;
            }
        }
    }

    public void Clicked()
    {
        Debug.Log("Clicked");
        if (clicked)
        {
            clicked = false;
        }
        else if (!clicked)
        {
            clicked = true;
        }
    }
}
