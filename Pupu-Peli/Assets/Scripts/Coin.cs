using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;

    private void OnCollisionEnter(Collision collision)
    {
        // Add player the money, sfx and delete this object
        Debug.Log("Coin collision!");
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Collect Coin!");
            SoundManager.Instance.PlaySound(SoundManager.Clip.CoinCollect);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.Clip.CoinCollide);
        }

    }
}
