using System.Collections.Generic;
using Unity.VisualScripting;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public List<AudioSource> audioSources;
    public int maxActiveAudioClips;

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(AudioClip clip)
    {
        // Create audio source to play given sound clip
        // Destroy after audio has been played

        if (audioSources.Count < maxActiveAudioClips)
        {
            AudioSource tempSource = this.AddComponent<AudioSource>();

            audioSources.Add(tempSource);
            tempSource.clip = clip;
            tempSource.Play();

            StartCoroutine(WaitToDestroy(tempSource, clip.length + 0.2f));
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
