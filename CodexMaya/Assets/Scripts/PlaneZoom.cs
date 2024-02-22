using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaneZoom : MonoBehaviour
{
    private bool clicked = false;
    private bool isEnlarged = false;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (clicked)
        {
            if (!isEnlarged)
            {
                anim.Play("Enlarge2");
                isEnlarged = true;
            }
        }
        else if (!clicked)
        {
            if (isEnlarged)
            {
                anim.Play("Minimize");
                isEnlarged = false;
            }
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
            clicked = true;
        }
    }
}
