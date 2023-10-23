using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMan : MonoBehaviour
{
    [SerializeField] public AudioPool[] m_sounds;
    private static AudioMan m_instance;
    public static AudioMan _instance => m_instance;
    private bool menuPlaying = false;
    private bool combatPlaying = false;
    private bool openMapPlaying = false;

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

    private void Update()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu") && !menuPlaying)
        {
            m_instance.Stop("OpenMap");
            m_instance.Stop("CombatMap");
            m_instance.Play("Menu");
            menuPlaying = true;
            openMapPlaying = false;
            combatPlaying = false;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("WorldMap") && !openMapPlaying)
        {
            m_instance.Stop("Menu");
            m_instance.Stop("CombatMap");
            m_instance.Play("OpenMap");
            openMapPlaying = true;
            menuPlaying = false;
            combatPlaying = false;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("CombatScene") && !combatPlaying)
        {
            m_instance.Stop("OpenMap");
            m_instance.Stop("Menu");
            m_instance.Play("CombatMap");
            combatPlaying = true;
            openMapPlaying = false;
            menuPlaying = false;
        }
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

    public IEnumerator StopMusic(string name)
    {
        AudioPool audio = Array.Find(m_sounds, sound => sound.m_name == name);
        if (audio == null) { yield return null; }

        while (true)
        {
            audio.m_volume -= 0.1f;
            if(audio.m_volume <= 0) 
            {
                audio.m_source.Stop();
                audio.m_volume = 1;
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
