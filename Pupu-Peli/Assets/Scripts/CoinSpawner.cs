using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CoinSpawner : MonoBehaviour
{

    public Transform spawnArea;

    public GameObject coinPrefab;

    public float spawnDelay;
    public int maxSpawnForce;
    public int minSpawnForce;

    private void Start()
    {
       // StartCoroutine(SpawnCoinsTEST());
    }

    public void BeginSpawningCoins(int score)
    {
        StartCoroutine(SpawnCoins(score));
    }

    private IEnumerator SpawnCoins(int score)
    {
        for (int i = 0; i < score / 2; i++)
        {
                Vector3 tempSpawnPosition = spawnArea.position;
                tempSpawnPosition += new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0, 0.4f), Random.Range(-0.4f, 0.4f));

                GameObject coin = Instantiate(coinPrefab, tempSpawnPosition, spawnArea.transform.rotation);
                //coin.gameObject.transform.SetParent(spawnArea);

                

                coin.GetComponent<Rigidbody>().AddRelativeForce(Quaternion.Euler(0, 0, 45) * new Vector3(0, Random.Range(minSpawnForce, maxSpawnForce), 0), ForceMode.Impulse);

                yield return new WaitForSeconds(spawnDelay);
        }
    }


    public IEnumerator SpawnCoinsTEST()
    {
        for (int i = 0; i < 50; i++)
        {
            Vector3 tempSpawnPosition = spawnArea.localPosition;
            tempSpawnPosition += new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0, 0.4f), Random.Range(-0.4f, 0.4f));

            GameObject coin = Instantiate(coinPrefab, tempSpawnPosition, spawnArea.transform.rotation);
            coin.gameObject.transform.SetParent(spawnArea, false);

            coin.GetComponent<Rigidbody>().AddRelativeForce(Quaternion.Euler(0, 0, 45) * new Vector3(0, Random.Range(minSpawnForce, maxSpawnForce), 0), ForceMode.Impulse);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

}
