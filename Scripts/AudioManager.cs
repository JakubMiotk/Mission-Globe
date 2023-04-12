using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource clickSound;
    void Start()
    {
        clickSound.volume = PlayerPrefs.GetFloat("volume");
    }

}
