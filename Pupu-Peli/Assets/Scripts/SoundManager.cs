using System.Collections.Generic;
using Unity.VisualScripting;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using System;
using JetBrains.Annotations;

public class SoundManager : MonoBehaviour
{
    public enum Clip
    {
        CoinCollide,
        CoinCollect,
    }

    public static SoundManager Instance { get; private set; }

    public SoundAudioClip[] audioClipArray;

    public List<AudioSource> audioSources;
    public int maxActiveAudioClips;

    private static Dictionary<Clip, float> clipTimerDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        // Add interval times for repeating sounds
        clipTimerDictionary = new Dictionary<Clip, float>();
        clipTimerDictionary[Clip.CoinCollide] = 0f;
        clipTimerDictionary[Clip.CoinCollect] = 0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(Clip clip)
    {
        // Create audio source to play given sound clip
        // Destroy after audio has been played
        if (CanPlaySound(clip))
        {
            AudioSource tempSource = this.AddComponent<AudioSource>();

            audioSources.Add(tempSource);
            AudioClip audioClip = GetAudioClip(clip);
            tempSource.PlayOneShot(audioClip);

            StartCoroutine(WaitToDestroy(tempSource, audioClip.length + 0.2f));
        }
    }

    private AudioClip GetAudioClip(Clip clip)
    {
        for (int i = 0; i < audioClipArray.Length; i++)
        {
            if (audioClipArray[i].clip == clip)
            {
                return audioClipArray[i].audioClip;
            }
        }
        Debug.LogError("Clip " + clip + " not found!");
        return null;
    }

    private static bool CanPlaySound(Clip clip)
    {
        switch (clip)
        {
            case Clip.CoinCollide:
                if (clipTimerDictionary.ContainsKey(clip))
                {
                    float lastTimePlayed = clipTimerDictionary[clip];
                    float coinCollideTimerMax = .1f;
                    if (lastTimePlayed + coinCollideTimerMax < Time.time)
                    {
                        Debug.Log("Can play sound!");
                        clipTimerDictionary[clip] = Time.time;
                        return true;
                    }
                    else
                    {
                        Debug.Log("Time interval");
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            // Add more switch cases for sound that may repeat many times in succesion
            default:
                return true;
        }
    }

    private IEnumerator WaitToDestroy(AudioSource source, float time)
    {
        float timeElapsed = 0;


        while (timeElapsed < time)
        {
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        audioSources.Remove(source);
        Destroy(source);
    }
}

[Serializable]
public class SoundAudioClip
{
    public SoundManager.Clip clip;
    public AudioClip audioClip;
}
