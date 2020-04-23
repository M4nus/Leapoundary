using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum BallState
{
    Safe,
    Launched,
    Upgrade,
    Death,
    Options
}

public enum GameType
{
    Classic,
    Ninja
}

public class PlayerSettings : MonoBehaviour
{
    // Script with all the variables needed for ball, turret, enemies
    public static PlayerSettings instance;

    public GameType gameType = GameType.Ninja;
    public BallState ballState = BallState.Upgrade;
    
    public List<object> positiveCards = new List<object>();
    public List<object> negativeCards = new List<object>();
    public List<object> neutralCards = new List<object>();

    public GameObject ball;
    public GameObject currentTurret;
    public GameObject nextTurret;
    public GameObject cardsTab;

    public int lives = 3;
    public int leaps = 0;
    public int triangleLimit = 5;
    public int standerLimit = 7;
    public int shurikenLimit = 7;
    public int kunaiLimit = 5;
    public int cardType;

    [Range(300, 1000)]

    public float ballSpeed;
    public float triangleSpeed = 1f;
    public float triangleRotate = 200f;
    public float triangleSpawnTime = 15f;
    public float standerSpawnTime = 10f;
    public float shurikenSpawnTime = 10f;
    public float kunaiSpawnTime = 6f;

    [HideInInspector]
    public bool upgradeTime = false;
    [HideInInspector]
    public bool canSpawnTriangle = false;
    [HideInInspector]
    public bool canSpawnStander = false;
    [HideInInspector]
    public bool canSpawnShuriken = false;
    [HideInInspector]
    public bool canSpawnKunai = false;
    [HideInInspector]
    public bool cardsDissolved = false;
    [HideInInspector]
    public bool isBounced = false;
    [HideInInspector]
    public bool isReflected = false;


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        //AudioManager.instance.Play("Void");

        if(gameType == GameType.Classic)
        {
            canSpawnTriangle = true;
            canSpawnStander = true;
            positiveCards.AddRange(Resources.LoadAll("Cards/ClassicPositive"));
            negativeCards.AddRange(Resources.LoadAll("Cards/ClassicNegative"));
            neutralCards.AddRange(Resources.LoadAll("Cards/ClassicNeutral"));
        }
        if(gameType == GameType.Ninja)
        {
            canSpawnShuriken = true;
            canSpawnKunai = true;
            positiveCards.AddRange(Resources.LoadAll("Cards/NinjaPositive"));
            negativeCards.AddRange(Resources.LoadAll("Cards/NinjaNegative"));
            neutralCards.AddRange(Resources.LoadAll("Cards/NinjaNeutral"));
        }
    }

    private void Update()
    {
        // Checking whether ball was launched or not
        if(ballState != BallState.Death && ballState != BallState.Options)
        {
            if(ball.transform.parent == currentTurret.transform && !upgradeTime)
                ballState = BallState.Safe;
            else if(ball.transform.parent != currentTurret.transform)
                ballState = BallState.Launched;
            else if(ball.transform.parent == currentTurret.transform && upgradeTime)
                ballState = BallState.Upgrade;
        }

        if(ballState == BallState.Upgrade)
            cardsTab.SetActive(true);
        else if(cardsDissolved)
        {
            cardsTab.SetActive(false);
            cardsDissolved = false;
        }

        // Checking if we can spawn enemies
        if(gameType == GameType.Classic)
        {
            ClassicMode();
        }
        else if(gameType == GameType.Ninja)
        {
            NinjaMode();
        }
    }

    public void ClassicMode()
    {
        canSpawnTriangle = (ballState == BallState.Upgrade || GameObject.FindGameObjectsWithTag("Triangle").Length >= triangleLimit) ? false : true;
        canSpawnStander = (ballState == BallState.Upgrade || GameObject.FindGameObjectsWithTag("Stander").Length >= standerLimit) ? false : true;
    }

    public void NinjaMode()
    {
        canSpawnShuriken = (ballState == BallState.Upgrade || GameObject.FindGameObjectsWithTag("Shuriken").Length >= shurikenLimit) ? false : true;
        canSpawnKunai = (ballState == BallState.Upgrade || GameObject.FindGameObjectsWithTag("Kunai").Length >= shurikenLimit) ? false : true;
    }

    public void CheckLeaps()
    {
        leaps++;
        if(leaps % 10 == 0)
        {
            //0 - positive, 1 - negative, 2 - neutral;
            float percentage = Random.value;
            if(percentage < 0.5f)
                cardType = 0;
            else if(percentage < 0.8f)
                cardType = 1;
            else if(percentage < 1f)
                cardType = 2;

            upgradeTime = true;
        }
        if(leaps <= 180)
        {
            gameObject.GetComponent<UpgradeSettings>().HueIncrement(1);
        }
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
        lives--;
        if(lives <= 0)
            ballState = BallState.Death;
    }

    public void MoveTurrets()
    {
        // Moving CurrentTurret to NextTurret position and then generating NextTurret new position
        currentTurret.transform.position = nextTurret.transform.position;
        do
        {
            nextTurret.transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-3.5f, 3.5f));
        } while(IsSpawnPointOverlap(nextTurret.transform.position));
        CheckLeaps();
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

    public void ResetCardList()
    {
        positiveCards.Clear();
        negativeCards.Clear();
        neutralCards.Clear();
        if(gameType == GameType.Classic)
        {
            positiveCards.AddRange(Resources.LoadAll("Cards/ClassicPositive"));
            negativeCards.AddRange(Resources.LoadAll("Cards/ClassicNegative"));
            neutralCards.AddRange(Resources.LoadAll("Cards/ClassicNeutral"));
        }
        if(gameType == GameType.Ninja)
        {
            positiveCards.AddRange(Resources.LoadAll("Cards/NinjaPositive"));
            negativeCards.AddRange(Resources.LoadAll("Cards/NinjaNegative"));
            neutralCards.AddRange(Resources.LoadAll("Cards/NinjaNeutral"));
        }
    }
}
