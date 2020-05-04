using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckBouncingTrigger : MonoBehaviour
{
    public PuckSound puckSound;

    public bool corutinePlaying = false;

    private void Update()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!corutinePlaying)
        {
            StartCoroutine(PlayBounceSound());
        }
    }

    IEnumerator PlayBounceSound()
    {
        corutinePlaying = true;
        puckSound.BouncingSound();
        yield return new WaitForSeconds(0.1f);
        corutinePlaying = false;
    }

}
