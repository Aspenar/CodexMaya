using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlaneZoom : MonoBehaviour
{
    public GameObject greyOut;
    private bool clicked = false;
    private bool isEnlarged = false;

    private Animator anim;
    private void Awake()
    {
        GameObject greyOut = GameObject.FindGameObjectWithTag("greyOut");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (clicked)
        {
            if (!isEnlarged)
            {
                anim.Play(enlargeAnim);
/*                rend.material = mat2;
*/                isEnlarged = true;
                greyOut.SetActive(true);
            }
        }
        else if (!clicked)
        {
            if (isEnlarged)
            {
                anim.Play(minimizeAnim);
/*                rend.material = mat1;
*/                isEnlarged = false;
                greyOut.SetActive(false);
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
