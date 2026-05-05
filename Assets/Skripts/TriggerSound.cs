using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerSound : MonoBehaviour
{
    [Header("")]
    [Tooltip("AudioClip)")]
    public AudioClip soundClip;

    [Tooltip("Volume")]
    [Range(0f, 1f)] public float volume = 1.0f;

    [Header("")]
    [Tooltip("Teg")]
    public string targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            PlaySound();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (soundClip != null)
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position, volume);
        }
    }
}
