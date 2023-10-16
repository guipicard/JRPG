using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioPool
{
    public string m_name;
    public AudioClip m_clip;

    [Range(0f, 1f)]
    public float m_volume;
    [Range(0f, 3f)]
    public float m_pitch;

    public bool m_loop;

    [HideInInspector]
    public AudioSource m_source;
}
