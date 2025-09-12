using System.Diagnostics;
using System.Timers;
using System.Threading;
using System.Collections;
using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // codar sem ter o unity aberto pra testar vai ser terrivel
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private float visionRange = 8f;

    [SerializeField] private Rigidbody2D playerRb; // to raycast against them

    [Header("Raycast settings")]
    [SerializeField] private float wallRange;
    [SerializeField] private GameObject groundRayOrigin;
    [SerializeField] private float groundDown; // goddamn, just ask my if you have any questions
    [SerializeField] private float targetEngagementDistance;
    [SerializeField] private float acceptableRange;


    [Header("Others")]
    private Rigidbody2D rb;
    private Animator animaRoyal;
    [SerializeField] private bool inCombat;
    [SerializeField] private float shankingRange; // esfaquear
    private float lastRaycast;
    private float lastShot;
    private float lastSeenPlayer;

    [SerializeField] private float shootCooldown;

    public int facing; // -1 for left, 1 for right
    public bool walking;

    private Vector2 directionToPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animaRoyal = GetComponent<Animator>();
        facing = 1;
        walking = true;
    }

    void Update()
    {
        // the walking variable is set in a different part
        if (walking)
        {
            rb.velocity = new Vector2(facing * enemySpeed, rb.velocity.y);

            transform.localScale = new Vector3(facing, 1, 1);
            animaRoyal.SetBool("Walking", true);
        }

        else
        {
            animaRoyal.SetBool("Walking", false);
        }

        // raycast to the player to see if the enemy can see them
        Vector2 directionToPlayer = (playerRb.position - (Vector2)transform.position).normalized;

        if (Mathf.Sign(playerRb.position.x - transform.position.x) == facing && (Time.time > lastRaycast + 0.2f)) // is facing player
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, visionRange);
            if (hit.collider != null) // can see player
            {
                GameObject hitObject = hit.collider.gameObject;

                UnityEngine.Debug.Log("Inimigo viu Coisa!" + hitObject.name);
                UnityEngine.Debug.DrawRay(transform.position, directionToPlayer * visionRange, Color.red);

                if (hitObject.CompareTag("Player")) // is player
                {
                    // could have put most of these in a single if, but eh
                    inCombat = true;
                    UnityEngine.Debug.Log("Inimigo viu Player!");
                }
            }
            else
            {
                UnityEngine.Debug.Log("he saw nothing");
                
            }
            lastRaycast = Time.time;
        }

        // turn around if going to hit a wall or fall
        // these have side effects btw :wink:
        bool wallResult = checkWall();
        bool groundResult;
        if (!wallResult)
        { // only turn once, never know these damn edge cases
            groundResult = CheckGround();
        }

        if (groundResult || wallResult)
        {
            // stop for 4 seconds then go back to normal patrol
            StartCoroutine(TurnWaitTime());
        }

        // will overrride the facing from check wall and ground
        if (inCombat)
        {
            if (!CanSeePlayer())
            {
                if (Time.time > lastSeenPlayer + 30f)
                {
                    UnityEngine.Debug.Log("Inimigo parou de perseguir o player!");
                    //TODO: should start search idk
                    inCombat = false;
                }
            }
            else
            {
                lastSeenPlayer = Time.time;
            }

            // move backwards or forwards to get within a set amount from target
            float direction = -Mathf.Sign(playerRb.position.x - transform.position.x);

            float targetX = direction * targetEngagementDistance + playerRb.position.x;

            if (Mathf.Abs(targetX - transform.position.x) > acceptableRange)
            {
                // move towards targetX
                // face towards player
                facing = (int)-direction; // this just works, i can assure you
                // override previous velocity
                rb.velocity = new Vector2(Mathf.Sign(targetX - transform.position.x) * enemySpeed, rb.velocity.y);
            }


            if (Time.time >= lastShot + shootCooldown)
            {
                // try to shoot at player
                UnityEngine.Debug.Log("pewpewpew o inimigo tentou atirar!");
                lastShot = Time.time;
            }

            // shank if too close
            if (Mathf.Abs(playerRb.position.x - transform.position.x) < shankingRange)
            {
                UnityEngine.Debug.Log("O inimigo tentou esfaquear o player!");
            }
        }
    }

    bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, visionRange);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Player")) // is player
            {
                UnityEngine.Debug.DrawRay(transform.position, directiontoPlayer * visionRange, Color.red);
                return true;
            }
        }

        UnityEngine.Debug.DrawRay(transform.position, directiontoPlayer * visionRange, Color.blue);
        return false;
    }

    bool CheckWall()
    {
        // setting facing will automatically turn around the enemy on next frame btw
        Vector2 direction = facing == 1 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, wallRange);

        if (hit.collider != null)
        {
            if (!hit.collider.gameObject.compareTag("Player"))
            {
                UnityEngine.Debug.DrawRay(transform.position, direction * wallRange, Color.red);
                UnityEngine.Debug.Log("Virando por causa da parede na frente!");
                //TODO: add check for terrain
                facing *= -1; // set to inverse
                return true;
            }
        }
        UnityEngine.Debug.DrawRay(transform.position, direction * wallRange, Color.blue);

        return false;
    }

    bool CheckGround()
    {
        // same comment from before
        // direction is down
        RaycastHit2D hit = Physics2D.Raycast(groundRayOrigin.position, Vector2.down, groundDown);

        if (hit.collider == null)
        {
            UnityEngine.Debug.DrawRay(groundRayOrigin.position, Vector2.Down * groundDown, Color.red);
            UnityEngine.Debug.log("Invertendo direção por causa de chão faltando!");
            facing *= -1; // set to inverse
            return true;
        }
        UnityEngine.Debug.DrawRay(groundRayOrigin.position, Vector2.Down * groundDown, Color.blue);

        return false;
    }

    IEnumerator TurnWaitTime()
    {
        yield return new WaitForSeconds(4f);
        walking = true;
    }
}