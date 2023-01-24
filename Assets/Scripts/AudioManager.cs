using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume = 0.5f;
    public float pitch = 1.0f;

    private AudioSource audioSource;

    public void SetSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        this.audioSource.clip = clip;
    }

    public void Play()
    {
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    #region Singleton

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            go.transform.SetParent(transform);
            sounds[i].SetSource(go.AddComponent<AudioSource>());
        }
    }

    #endregion Singleton

    public void PlaySound(string name)
    {
        FindAndDoAction(name, true);
    }

    public void StopSound(string name)
    {
        FindAndDoAction(name, false);
    }

    private void FindAndDoAction(string name, bool play)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                if (play) { sounds[i].Play(); }
                else { sounds[i].Stop(); }
                return;
            }
        }
    }
}

