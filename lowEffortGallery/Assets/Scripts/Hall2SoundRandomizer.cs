using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class Hall2SoundRandomizer : MonoBehaviour
{
    public AudioClip[] _audioclips = new AudioClip[2];
    public bool isRandom = true;
    public SoundManager _soundManager;

    private void Awake()
    {
        if (isRandom)
        {
            _soundManager.musicClip = RandomExtensions.GetRandomElement(_audioclips);
        }
    }
}
