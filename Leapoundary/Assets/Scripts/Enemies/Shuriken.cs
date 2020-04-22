using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private SpriteGlow.SpriteGlowEffect glow;
    private Rigidbody2D rb;
    private Color color;
    private float angle;

    public float speed = 100f;
    public float rotateSpeed = 100f;

    private void OnEnable()
    {
        GetComponentInChildren<SpriteGlow.SpriteGlowEffect>().GlowColor = Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.EulerAngles(new Vector3(0, 0, angle));
        angle += rotateSpeed * Time.fixedDeltaTime;
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
