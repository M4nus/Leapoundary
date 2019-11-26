using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartParticles : MonoBehaviour
{
    ParticleContainer pc;

    void Awake()
    {
        pc = GameObject.Find("GameManager").GetComponent<ParticleContainer>();
    }

    private void OnEnable()
    {
        Instantiate(pc.heartCharge, gameObject.transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        Instantiate(pc.heartMelt, gameObject.transform.position, Quaternion.identity);
    }
}
