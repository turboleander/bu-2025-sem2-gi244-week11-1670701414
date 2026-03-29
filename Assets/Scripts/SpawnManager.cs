using System.Collections;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int totalSpawnEnemies;
    public int numberOfRandomSpawnPoint;
    public float delayStart;
    public float spawnInterval;
    public int numberOfPowerUp;
}
public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    [Header("Spawn Settings")]
    public Wave[] waves;

    private Coroutine byeRoutine;
    private Coroutine spawnRoutine;
    private int[] randomSpawnPointIndex;

    void Start()
    {
        //InvokeRepeating(nameof(RandomSpawn), 0, 3);
        //byeRoutine = StartCoroutine(Bye());
        //StartCoroutine(SpawnRoutine());
        StartCoroutine(waveSpawner());
    }
    private void Update()
    {
        //StartCoroutine(Hello());
        //if(Time.time > 5)
        //{
        //    StopCoroutine(byeRoutine);
        //}
    }

    //void RandomSpawn()
    //{
    //    var index = Random.Range(0, spawnPoints.Length);
    //    var spawnPoint = spawnPoints[index];
    //    Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    //}

    //IEnumerator SpawnRoutine()
    //{
    //    while (true)
    //    {
    //        RandomSpawn();
    //        yield return new WaitForSeconds(5f);
    //    }
    //}

    void spawnPowerUp(int wave)
    {
        for (int i = 0; i < waves[wave].numberOfPowerUp; i++)
        {
            var index = Random.Range(0, spawnPoints.Length);
            var spawnPoint = spawnPoints[index];
            Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
    void randomSpawnPoint(int wave)
    {
        int amountOfSpawnPoints = waves[wave].numberOfRandomSpawnPoint;
        randomSpawnPointIndex = new int[amountOfSpawnPoints];

        for (int i = 0; i < amountOfSpawnPoints; i++)
        {
            randomSpawnPointIndex[i] = Random.Range(0, spawnPoints.Length);
        }
    }

    IEnumerator waveSpawner()
    {
        int wave = 0;
        while (wave < waves.Length)
        {
            Debug.Log("start Wave: " + (wave + 1));
            spawnPowerUp(wave);
            yield return new WaitForSeconds(waves[wave].delayStart);
            randomSpawnPoint(wave);

            yield return StartCoroutine(spawner(wave));
            Debug.Log("waves completed!");

            wave++;
        }
        Debug.Log("All waves completed!");
    }

    IEnumerator spawner(int wave)
    {
        int spawnCount = 0;
        while (spawnCount < waves[wave].totalSpawnEnemies)
        {
            int randomGeneratedPoint = Random.Range(0, randomSpawnPointIndex.Length);

            Instantiate(enemyPrefab, spawnPoints[randomSpawnPointIndex[randomGeneratedPoint]].position, Quaternion.identity);
            spawnCount++;
            yield return new WaitForSeconds(waves[wave].spawnInterval);
        }
    }

    //IEnumerator Hello()
    //{
    //    Debug.Log("Hello" + Time.frameCount);
    //    yield return new WaitForSeconds(1f);
    //}
    //IEnumerator Bye()
    //{
    //    while (true)
    //    {
    //        Debug.Log("Hello" + Time.frameCount + " " + Time.time);
    //        yield return new WaitForSeconds(1f);

    //        yield return Hello();
    //        yield return new WaitForSeconds(1f);
    //        //if(Time.time > 5)
    //        //{
    //        //    yield break;
    //        //}
    //    }
    //}
}
