using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    public void OnClickFX()
    {
        myFX.PlayOneShot(clickFX);
    }


    public void OnHoverFX()
    {
        myFX.PlayOneShot(hoverFX);
    }
}
