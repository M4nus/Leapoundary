using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public GameObject triangle;
    public GameObject stander;
    public GameObject shuriken;
    public GameObject kunai;
    public GameObject wall;
    private ParticleContainer pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<ParticleContainer>();
        StartCoroutine(SpawnTriangles());
        StartCoroutine(SpawnStanders());
        StartCoroutine(SpawnShurikens());
        StartCoroutine(SpawnKunais());
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

    public IEnumerator SpawnShurikens()
    {
        while(true)
        {
            if(PlayerSettings.instance.canSpawnShuriken)
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

                // Spawning shurikens

                shuriken = ObjectPooler.sharedInstance.GetPooledObject("Shuriken");
                if(shuriken != null)
                {
                    shuriken.transform.position = currentSpawnPosition;
                    shuriken.SetActive(true);
                }
            }
            yield return new WaitForSeconds(PlayerSettings.instance.shurikenSpawnTime);
        }
    }

    public IEnumerator SpawnKunais()
    {
        while(true)
        {
            if(PlayerSettings.instance.canSpawnKunai)
            {
                float radius = 12f;    // distance from center
                float angle = Random.Range(0, Mathf.PI * 2);    // Random angle in radians
                                                                // sin and cos need value in radians
                                                                // full turn aroud circle in radians equal 2*PI ~6.283185 rad
                Vector2 pos2d = new Vector2(Mathf.Sin(angle) * radius, Mathf.Cos(angle) * radius);
                Vector2 currentSpawnPosition = new Vector3(pos2d.x, pos2d.y, 0);
                
                // Spawning kunais

                kunai = ObjectPooler.sharedInstance.GetPooledObject("Kunai");
                if(kunai != null)
                {
                    kunai.transform.position = currentSpawnPosition;
                    kunai.SetActive(true);
                    if(PlayerSettings.instance.kunaiSpawnTime > 1f)
                        PlayerSettings.instance.kunaiSpawnTime -= 0.1f;
                }
            }
            yield return new WaitForSeconds(PlayerSettings.instance.kunaiSpawnTime);
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

    public void SpawnShuriken()
    {
        shuriken = ObjectPooler.sharedInstance.GetPooledObject("Shuriken");
        if(shuriken != null)
        {
            shuriken.transform.position = SpawnPosition();
            shuriken.SetActive(true);
        }
    }

    public IEnumerator SpawnWall()
    {
        Vector3 currentSpawnPosition = SpawnPosition();
        
        Instantiate(pc.innerWall, currentSpawnPosition, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        wall = ObjectPooler.sharedInstance.GetPooledObject("Wall");
        if(wall != null)
        {
            wall.transform.position = currentSpawnPosition;
            wall.SetActive(true);
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
