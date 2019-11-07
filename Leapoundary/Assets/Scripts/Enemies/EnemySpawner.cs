using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public GameObject triangle;
    public GameObject stander;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTriangles());
        StartCoroutine(SpawnStanders());
    }

    public IEnumerator SpawnTriangles()
    {
        while(true)
        {
            if(PlayerSettings.instance.canSpawnTriangle)
                Instantiate(triangle, SpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(PlayerSettings.instance.triangleSpawnTime);
        }
    }

    public IEnumerator SpawnStanders()
    {
        while(true)
        {
            if(PlayerSettings.instance.canSpawnStander)
                Instantiate(stander, SpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(PlayerSettings.instance.standerSpawnTime);
        }
    }

    private Vector3 SpawnPosition()
    {
        Vector3 newPosition;
        do
        {
            newPosition = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-3.5f, 3.5f), 0f);
        } while(PlayerSettings.instance.IsSpawnPointOverlap(newPosition));
        return newPosition;
    }
}
