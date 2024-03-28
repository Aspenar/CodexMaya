using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlaneZoom : MonoBehaviour
{
    private GameObject greyOut;
    private bool clicked = false;
    private bool isEnlarged = false;

    private Animator anim;
<<<<<<< Updated upstream
/*    private Renderer rend;
    public Material mat1;
    public Material mat2;
*/
=======

>>>>>>> Stashed changes
    private void Awake()
    {
        GameObject greyOut = GameObject.FindGameObjectWithTag("greyOut");
        anim = GetComponent<Animator>();
<<<<<<< Updated upstream
/*        rend = GetComponent<Renderer>();
        mat1 = rend.material;
*/    }
=======
    }
>>>>>>> Stashed changes

    private void Update()
    {
        if (clicked)
        {
            if (!isEnlarged)
            {
<<<<<<< Updated upstream
                anim.Play(enlargeAnim);
/*                rend.material = mat2;
*/                isEnlarged = true;
=======
                anim.Play("Enlarge2");
                isEnlarged = true;
                greyOut.SetActive(true);
                
>>>>>>> Stashed changes
            }
        }
        else if (!clicked)
        {
            if (isEnlarged)
            {
<<<<<<< Updated upstream
                anim.Play(minimizeAnim);
/*                rend.material = mat1;
*/                isEnlarged = false;
=======
                anim.Play("Minimize");
                isEnlarged = false;
                greyOut.SetActive(false);
>>>>>>> Stashed changes
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
