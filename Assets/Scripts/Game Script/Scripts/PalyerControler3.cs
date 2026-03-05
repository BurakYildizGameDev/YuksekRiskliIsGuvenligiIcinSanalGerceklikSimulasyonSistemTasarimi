using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalyerControler3 : MonoBehaviour
{
    public bool faaltrue;
    public Animator animator;
    public AudioSource audioSource; // Ses çalmak için referans

    void Start()
    {
        faaltrue = false;
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // AudioSource'u bul
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fall")
        {
            faaltrue = true;
            animator.SetBool("fallplayer", true);

            // Müzik çal
            if (audioSource != null && !audioSource.isPlaying) // Eðer ses çalmýyorsa
            {
                audioSource.Play();
            }
        }
    }
}
