using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    private Coroutine byeRoutine;

    void Start()
    {
        //InvokeRepeating(nameof(RandomSpawn), 0, 3);
        //byeRoutine = StartCoroutine(Bye());
        StartCoroutine(SpawnRoutine());
    }
    private void Update()
    {
        //StartCoroutine(Hello());
        //if(Time.time > 5)
        //{
        //    StopCoroutine(byeRoutine);
        //}
    }

    void RandomSpawn()
    {
        var index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[index];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator Hello()
    {
        Debug.Log("Hello" + Time.frameCount);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator Bye()
    {
        while (true)
        {
            Debug.Log("Hello" + Time.frameCount + " " + Time.time);
            yield return new WaitForSeconds(1f);

            yield return Hello();
            yield return new WaitForSeconds(1f);
            //if(Time.time > 5)
            //{
            //    yield break;
            //}
        }
    }
}
