using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControllerr : MonoBehaviour
{
    private static MusicControllerr music;
    void Awake()
    {
        if (music == null)
        {
            music = this;
            DontDestroyOnLoad(music);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
