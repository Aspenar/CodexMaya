using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CodexTezcatilpoca : NetworkBehaviour
{
    [Header("Animations")]
    // Start is called before the first frame update
    [SerializeField] private string enlargeAnim;
    [SerializeField] private string nextAnim;

    //private bool isRotatingLeft = false;
    [Header("Rotation")]
    public float rotationSpeed = 1.0f;
    public float rotationIncrement = 5.0f;
    private Quaternion targetRotation;
    private float currentRotation = 90f;

    public Animator anim;
    private bool isAnimating;

    [Header("UI")]
    public GameObject pageTurner;
    // -- USE THIS WHEN USING BUILD
    /*public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        anim = GetComponent<Animator>();
    }*/
    // --

    // -- USE THIS WHEN TESTING JUST THE SCENE
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // --

    private void Update()
    {
        if (!isAnimating)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void OpenBook()
    {
        anim.Play(enlargeAnim);

        StartCoroutine(NextAnim());
    }

    public IEnumerator NextAnim()
    {
        isAnimating = true;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        /* while (anim.GetCurrentAnimatorStateInfo(0).IsName(enlargeAnim))
         {
             yield return null;
         }*/

        anim.Play(nextAnim);

        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(12);

        pageTurner.SetActive(true);
        targetRotation = Quaternion.Euler(0, currentRotation, 0);
        isAnimating = false;
        anim.enabled = false;
    }

    public void RotateLeft()
    {
        currentRotation -= rotationIncrement;

        targetRotation = Quaternion.Euler(0, currentRotation, 0);
        Debug.Log("Right");
    }

    public void RotateRight()
    {
        currentRotation += rotationIncrement;

        targetRotation = Quaternion.Euler(0, currentRotation, 0);
        Debug.Log("Left");
    }

}
