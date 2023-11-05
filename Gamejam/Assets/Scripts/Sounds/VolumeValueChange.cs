using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoulmeValueChange : MonoBehaviour
{
    private AudioSource audioSrc;

    private float _soundVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = _soundVolume;
    }

    public void SetVolume(float vol)
    {
        _soundVolume = vol;
    }
}
