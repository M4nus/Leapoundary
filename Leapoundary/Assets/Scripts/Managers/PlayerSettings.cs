using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallState
{
    Safe,
    Launched,
    Upgrade,
    Death
}

public class PlayerSettings : MonoBehaviour
{
    // Script with all the variables needed for ball, turret, enemies
    public static PlayerSettings instance;

    public BallState ballState = BallState.Safe;

    public GameObject ball;
    public GameObject currentTurret;
    public GameObject nextTurret;

    public int lives = 3;
    public int leaps = 0;
    public int triangleLimit = 5;
    public int standerLimit = 7;
    [Range(300, 1000)]
    public float ballSpeed;
    public float triangleSpeed = 1f;
    public float triangleRotate = 200f;
    public float triangleSpawnTime = 15f;
    public float standerSpawnTime = 10f;
    public bool upgradeTime = false;
    public bool canSpawnTriangle = true;
    public bool canSpawnStander = true;


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        // Checking whether ball was launched or not
        if(ballState != BallState.Death)
        {
            if(ball.transform.parent == currentTurret.transform && !upgradeTime)
                ballState = BallState.Safe;
            else if(ball.transform.parent != currentTurret.transform)
                ballState = BallState.Launched;
            else if(ball.transform.parent == currentTurret.transform && upgradeTime)
                ballState = BallState.Upgrade;
        }

        // Checking if we can spawn enemies
        canSpawnTriangle = (ballState == BallState.Upgrade || GameObject.FindGameObjectsWithTag("Triangle").Length >= triangleLimit) ? false : true;
        canSpawnStander = (ballState == BallState.Upgrade || GameObject.FindGameObjectsWithTag("Stander").Length >= standerLimit) ? false : true;

        // Cheats
        if(Input.GetKeyDown(KeyCode.R))
            ResetBall();
        
    }

    public void ResetBall()
    {
        // Setting ball in CurrentTurret
        ball.transform.parent = currentTurret.transform;
        ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ball.transform.position = currentTurret.transform.position;
    }

    public void HurtBall()
    {
        if(lives > 1)
        {
            lives--;
        }
        else
        {
            ballState = BallState.Death;
        }
    }

    public void MoveTurrets()
    {
        // Moving CurrentTurret to NextTurret position and then generating NextTurret new position
        currentTurret.transform.position = nextTurret.transform.position;
        do
        {
            nextTurret.transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-3.5f, 3.5f));
        } while(IsSpawnPointOverlap(nextTurret.transform.position));
        leaps++;
    }

    public bool IsSpawnPointOverlap(Vector3 position)
    {
        // Checking if we are spawning on some other collider
        return Physics2D.OverlapCircle(position, 0.8f);
    }

    public void GetCrosshairDirection(GameObject obj)
    {
        // Converting mouse position into world coordinates
        // Getting direction to point at
        // Setting vector of transform directly
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition - (Vector2)obj.transform.position).normalized;
        obj.transform.right = direction;
    }

}
