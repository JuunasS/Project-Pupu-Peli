using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;

    public AudioSource audioSource;
    public AudioClip coinHitAudio;
    public AudioClip coinCollectAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.clip = coinHitAudio;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Add player the money, sfx and delete this object
        Debug.Log("Coin collision!");
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Player collided with coin!");
            audioSource.clip = coinCollectAudio;
        }
        else
        {

            audioSource.clip = coinHitAudio;
        }
        //audioSource.Play();
        SoundManager.Instance.PlaySound(coinHitAudio);

    }
}
