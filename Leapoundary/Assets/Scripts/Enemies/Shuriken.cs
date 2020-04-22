using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    private SpriteGlow.SpriteGlowEffect glow;
    private Color color;
    private float angle;

    public float speed = 100f;
    public float rotateSpeed = 100f;

    private void OnEnable()
    {
        GetComponentInChildren<SpriteGlow.SpriteGlowEffect>().GlowColor = Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
        GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.EulerAngles(new Vector3(0, 0, angle));
        angle += rotateSpeed * Time.fixedDeltaTime;
            //transform.RotateAround(transform.position, new Vector3(0, 0, 1), angle);
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
