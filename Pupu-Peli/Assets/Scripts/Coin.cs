using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    public int maxSoundPlayed;

    private void OnCollisionEnter(Collision collision)
    {
        // Add player the money, sfx and delete this object

        //Debug.Log("Coin collision!");
        if (collision.transform.tag == "Player")
        {
            //Debug.Log("Collect Coin!");
            SoundManager.Instance.PlaySound(SoundManager.Clip.CoinCollect);
            collision.gameObject?.GetComponent<CoinPurse>().AddMoney(this.value);
            Destroy(this.gameObject, .1f);
        }
        else if (maxSoundPlayed < 2)
        {
            maxSoundPlayed++;
            SoundManager.Instance.PlaySound(SoundManager.Clip.CoinCollide);
        }

    }

    // Float to player after having made contact with ground?
}
