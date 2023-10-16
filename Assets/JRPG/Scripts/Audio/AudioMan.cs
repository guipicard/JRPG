using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMan : MonoBehaviour
{
    [SerializeField] public AudioPool[] m_sounds;
    private static AudioMan m_instance;
    public static AudioMan _instance => m_instance;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (AudioPool audio in m_sounds)
        {
            audio.m_source = gameObject.AddComponent<AudioSource>();
            audio.m_source.clip = audio.m_clip;

            audio.m_source.volume = audio.m_volume;
            audio.m_source.pitch = audio.m_pitch;
            audio.m_source.loop = audio.m_loop;
        }
    }

    private void Start()
    {

    }

    public void Play(string name)
    {
        AudioPool audio = Array.Find(m_sounds, sound => sound.m_name == name);
        if (audio == null) { return; }
        audio.m_source.Play();
    }

    public void Stop(string name)
    {
        AudioPool audio = Array.Find(m_sounds, sound => sound.m_name == name);
        if (audio == null) { return; }
        audio.m_source.Stop();

    }
}
