using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class CodexTezcatilpoca : NetworkBehaviour
{
    //this script is for interacting with the codex, still running into performance issues but page turns and enlarging is working currently even if it is slow
    private NetworkVariable<bool> m_IsAnimationPlaying = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        anim = GetComponent<Animator>();
        Debug.Log("Codex loading...");
        m_IsAnimationPlaying.OnValueChanged += OnAnimationStateChanged;
    }

    public override void OnNetworkDespawn()
    {
        m_IsAnimationPlaying.OnValueChanged -= OnAnimationStateChanged;
    }
    // --

    // -- USE THIS WHEN TESTING JUST THE SCENE
    /*private void Awake()
    {
        anim = GetComponent<Animator>();
    }*/
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
        Debug.Log("Book Opening...");
        if (IsOwnedByServer)
        {
            Debug.Log("Sending OpenBookServerRpc...");
            OpenBookServerRpc();
        }
        else
        {
            Debug.LogWarning("OpenBook called but not the local player.");
        }

        /*anim.Play(enlargeAnim);

        StartCoroutine(NextAnim())*/
        ;
    }

    [ServerRpc(RequireOwnership = false)]
    private void OpenBookServerRpc()
    {
        Debug.Log("Received OpenBookServerRpc on the server.");
        m_IsAnimationPlaying.Value = true;
        Debug.Log("Network Value changed: " + m_IsAnimationPlaying.Value);
    }

    [ServerRpc(RequireOwnership = false)]
    public void RotateLeftServerRpc()
    {
        currentRotation -= rotationIncrement;

        targetRotation = Quaternion.Euler(0, currentRotation, 0);
        Debug.Log("Right");
    }

    [ServerRpc(RequireOwnership = false)]
    public void RotateRightServerRpc()
    {
        currentRotation += rotationIncrement;

        targetRotation = Quaternion.Euler(0, currentRotation, 0);
        Debug.Log("Left");
    }

    private void OnAnimationStateChanged(bool previousValue, bool newValue)
    {
        // Update the animation state based on the networked variable
        if (newValue)
        {
            StartCoroutine(PlayAnimation());
        }
    }

    public IEnumerator PlayAnimation()
    {
        Debug.Log("Playing anim...");
        anim.Play(enlargeAnim);
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
}
