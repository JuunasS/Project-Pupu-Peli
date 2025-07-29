using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    public int maxSoundPlayed;
    public bool collected = false;
    public bool moveToTargetActive = false;
    public float moveDuration = 1f;
    public Vector3 targetPositionOffset;

    private void OnCollisionEnter(Collision collision)
    {
        // Add player the money, sfx and delete this object

        //Debug.Log("Coin collision!");
        if (collision.transform.tag == "CoinPurse") //(collision.transform.tag == "Player")
        {
            Debug.Log("CoinPurse collision");
            //Debug.Log("Collect Coin!");
            SoundManager.Instance.PlaySound(SoundManager.Clip.CoinCollect);
            collision.gameObject?.GetComponent<CoinPurse>().AddMoney(this.value);
            Destroy(this.gameObject, .1f);
        }
        else
        {
            if (maxSoundPlayed < 2)
            {
                maxSoundPlayed++;
                SoundManager.Instance.PlaySound(SoundManager.Clip.CoinCollide);
            }

            if (!moveToTargetActive)
            {
                FloatToTarget();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "CoinPurse")
        {
            //Debug.Log("Collect Coin!");
            SoundManager.Instance.PlaySound(SoundManager.Clip.CoinCollect);
            other.gameObject?.GetComponent<CoinPurse>().AddMoney(this.value);
            Destroy(this.gameObject, .1f);
        }
    }

    // Float to target after having made contact with ground?
    public void FloatToTarget()
    {
        moveToTargetActive = true;
        GameObject target = FindFirstObjectByType<CoinPurse>().gameObject;
        if (target != null)
        {
            this.GetComponent<MeshCollider>().isTrigger = true;
            this.GetComponent<Rigidbody>().useGravity = false;
            StartCoroutine(MoveToTarget(target));
        }
    }

    IEnumerator MoveToTarget(GameObject obj)
    {
        float timeElapsed = 0;

        while (timeElapsed < moveDuration)
        {
            if (collected) { break; }

            //Debug.Log("Lerp 1: " + timeElapsed);
            float t = timeElapsed / moveDuration;
            transform.position = Vector3.Lerp(transform.position, obj.transform.position + targetPositionOffset, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
}
