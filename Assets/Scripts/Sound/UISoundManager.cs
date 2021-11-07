using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class UISoundManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip selectSound;
    [SerializeField] AudioClip connectSound;
    [SerializeField] AudioClip disconnectErrorSound;
    [SerializeField] AudioClip dropdownSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHoverSound(){
        audioSource.PlayOneShot(hoverSound);
    }

    public void PlaySelectSound(){
        audioSource.PlayOneShot(selectSound);
    }

    public void PlayConnectedSound(){
        audioSource.PlayOneShot(connectSound);
    }

    public void PlayDisconnectedSound(){
        audioSource.PlayOneShot(disconnectErrorSound);
    }

    public void PlayDropdownSound(){
        audioSource.PlayOneShot(dropdownSound);
    }
    
}
