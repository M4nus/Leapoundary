using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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
            Instantiate(triangle, SpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(10f);
        }
    }

    public IEnumerator SpawnStanders()
    {
        while(true)
        {
            Instantiate(stander, SpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(14f);
        }
    }

    private Vector3 SpawnPosition()
    {
        return new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-3.5f, 3.5f), 0f);
    }
}
