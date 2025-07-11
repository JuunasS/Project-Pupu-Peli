using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    public int maxSoundPlayed;
    public bool collected = false;
    public bool moveToTargetActive = false;
    public float moveDuration = 1f;
    public Vector3 playerPositionOffset;

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
        else
        {
            if (!moveToTargetActive) {
                FloatToPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //Debug.Log("Collect Coin!");
            SoundManager.Instance.PlaySound(SoundManager.Clip.CoinCollect);
            other.gameObject?.GetComponent<CoinPurse>().AddMoney(this.value);
            Destroy(this.gameObject, .1f);
        }
    }

    // Float to player after having made contact with ground?
    public void FloatToPlayer()
    {
        moveToTargetActive = true;
        GameObject player = FindFirstObjectByType<CoinPurse>().gameObject;
        if (player != null)
        {
            this.GetComponent<MeshCollider>().isTrigger = true;
            StartCoroutine(MoveToTarget(player.transform.position + playerPositionOffset));
        }

    }

    IEnumerator MoveToTarget(Vector3 pos)
    {
        float timeElapsed = 0;

        while (timeElapsed < moveDuration)
        {
            if (collected) { break; }

            //Debug.Log("Lerp 1: " + timeElapsed);
            float t = timeElapsed / moveDuration;
            transform.position = Vector3.Lerp(transform.position, pos, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
}
