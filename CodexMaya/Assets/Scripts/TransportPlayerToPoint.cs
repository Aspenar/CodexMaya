using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TransportPlayerToPoint : NetworkBehaviour
{
    private GameObject player;
    private OVRScreenFade fade;

    private void Awake()
    {
        player = GameObject.Find("Host");
        fade = player.GetComponent<OVRScreenFade>();
    }

    public void MovePlayer()
    {
        StartCoroutine(StartFade());
        
    }

    IEnumerator StartFade()
    {
        fade.FadeOut();
        yield return new WaitForSeconds(1);
        player.transform.position = gameObject.transform.position;
        player.transform.rotation = gameObject.transform.rotation;
        fade.FadeIn();
    }
}
