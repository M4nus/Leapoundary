using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 100f;

    private void OnEnable()
    {
        Vector3 dir = Vector3.zero - this.transform.position;
        this.transform.up = -dir;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            PlayerSettings.instance.ResetBall();
            PlayerSettings.instance.HurtBall();
        }
    }
}
