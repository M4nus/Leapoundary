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

                // Spawning particles

                GameObject spawnParticles = ObjectPooler.sharedInstance.GetPooledObject("SpawnPlace");
                if(spawnParticles != null)
                {
                    spawnParticles.transform.position = currentSpawnPosition;
                    spawnParticles.SetActive(true);
                }

                yield return new WaitForSeconds(2f);

                // Spawning standers

                triangle = ObjectPooler.sharedInstance.GetPooledObject("Triangle");
                if(triangle != null)
                {
                    triangle.transform.position = currentSpawnPosition;
                    triangle.SetActive(true);
                }
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

                // Spawning particles

                GameObject spawnParticles = ObjectPooler.sharedInstance.GetPooledObject("SpawnPlace");
                if(spawnParticles != null)
                {
                    spawnParticles.transform.position = currentSpawnPosition;
                    spawnParticles.SetActive(true);
                }

                yield return new WaitForSeconds(2f);
                
                // Spawning standers

                stander = ObjectPooler.sharedInstance.GetPooledObject("Stander"); 
                if(stander != null)
                {
                    stander.transform.position = currentSpawnPosition;
                    stander.SetActive(true);
                }
            }
            yield return new WaitForSeconds(PlayerSettings.instance.standerSpawnTime);
        }
    }

    public void SpawnTriangle()
    {
        triangle = ObjectPooler.sharedInstance.GetPooledObject("Triangle");
        if(triangle != null)
        {
            triangle.transform.position = SpawnPosition();
            triangle.SetActive(true);
        }
    }

    public void SpawnStander()
    {
        stander = ObjectPooler.sharedInstance.GetPooledObject("Triangle");
        if(stander != null)
        {
            stander.transform.position = SpawnPosition();
            stander.SetActive(true);
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
