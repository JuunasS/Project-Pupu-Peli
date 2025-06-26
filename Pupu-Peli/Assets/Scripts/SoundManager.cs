using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System;

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
            SoundAudioClip audioClip = GetAudioClip(clip);

            tempSource.volume = UnityEngine.Random.Range(audioClip.minVolume, audioClip.maxVolume);
            tempSource.pitch = UnityEngine.Random.Range(audioClip.minPitch, audioClip.maxPitch);

            tempSource.PlayOneShot(audioClip.audioClip);

            StartCoroutine(WaitToDestroy(tempSource, audioClip.audioClip.length + 0.2f));
        }
    }

    private SoundAudioClip GetAudioClip(Clip clip)
    {
        for (int i = 0; i < audioClipArray.Length; i++)
        {
            if (audioClipArray[i].clip == clip)
            {
                return audioClipArray[i];
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
                    float coinCollideTimerMax = .05f;
                    if (lastTimePlayed + coinCollideTimerMax < Time.time)
                    {
                        //Debug.Log("Can play sound!");
                        clipTimerDictionary[clip] = Time.time;
                        return true;
                    }
                    else
                    {
                        //Debug.Log("Time interval");
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

    public float minVolume = 1;
    public float maxVolume = 1;
    public float minPitch = 1;
    public float maxPitch = 1;
}
