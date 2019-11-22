using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public GameObject triangle;
    public GameObject stander;
    private ParticleContainer pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<ParticleContainer>();
        StartCoroutine(SpawnTriangles());
        StartCoroutine(SpawnStanders());
    }

    public IEnumerator SpawnTriangles()
    {
        while(true)
        {
            if(PlayerSettings.instance.canSpawnTriangle)
            {
                Vector3 currentSpawnPosition = SpawnPosition();
                Instantiate(pc.spawnPlace, currentSpawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(2f);
                Instantiate(triangle, currentSpawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(PlayerSettings.instance.triangleSpawnTime);
        }
    }

    public IEnumerator SpawnStanders()
    {
        while(true)
        {
            if(PlayerSettings.instance.canSpawnStander)
            {
                Vector3 currentSpawnPosition = SpawnPosition();
                Instantiate(pc.spawnPlace, currentSpawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(2f);
                Instantiate(stander, currentSpawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(PlayerSettings.instance.standerSpawnTime);
        }
    }

    public void SpawnTriangle()
    {
        Instantiate(triangle, SpawnPosition(), Quaternion.identity);
    }

    public void SpawnStander()
    {
        Instantiate(stander, SpawnPosition(), Quaternion.identity);
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
