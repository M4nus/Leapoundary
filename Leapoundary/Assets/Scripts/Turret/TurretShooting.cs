using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [HideInInspector]
    public PlayerSettings ps;
    public GameObject gameManager;

    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private Transform pointer;

    void Start()
    {
        ps = gameManager.GetComponent<PlayerSettings>();
        line.GetComponent<LineRenderer>();
    }
    
    void Update()
    {
        if(CanShoot() && Input.GetKey(KeyCode.Mouse0))
        {
            Laser();
        }
        if(CanShoot() && Input.GetKeyUp(KeyCode.Mouse0))
        {
            line.enabled = false;
            ShootBall();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            PlayerSettings.instance.ResetBall();
        }
    }
    
    private void ShootBall()
    {
        if(!UIManager.IsPointerOverUIElement())
        {
            ps.GetCrosshairDirection(gameObject);
            ps.ball.GetComponent<Rigidbody2D>().AddForce(ps.ballSpeed * transform.right, ForceMode2D.Impulse);
            ps.ball.transform.parent = null;
            AudioManager.instance.PlayRandom("TurretShoot");
        }
    }

    private bool CanShoot()
    {
        if(ps.ballState == BallState.Safe)
            return true;
        else
            return false;
    }

    void Laser()
    {
        Vector2 dir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (dir - (Vector2)transform.position).normalized;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(pointer.position, direction, 40f);


        if(Physics2D.Raycast((Vector2)pointer.position, direction, 40f))
        {
            line.enabled = true;
            line.positionCount = 2;
            line.SetPosition(0, (Vector2)pointer.position);
            line.SetPosition(1, hit.point);
            if(ps.isReflected)
            {
                line.positionCount = 3;
                if(hit.collider.tag == "Mirrors")
                {
                    Vector2 pos = Vector2.Reflect((hit.point - (Vector2)pointer.position), hit.normal);
                    RaycastHit2D reflection = Physics2D.Raycast(hit.point + hit.normal, pos);
                    line.SetPosition(2, reflection.point);
                }
                else
                {
                    line.SetPosition(2, hit.point);
                }
            }
        }
    }
}
