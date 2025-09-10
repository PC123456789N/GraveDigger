using System.Diagnostics;
using System.Timers;
using System.Threading;
using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // codar sem ter o unity aberto pra testar vai ser terrivel
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private float visionRange = 200f;

    [SerializeField] private Rigidbody2D playerRb; // to raycast against them

    [Header("Raycast settings")]
    [SerializeField] private float wallRange;
    [SerializeField] private float groundForward;
    [SerializeField] private float groundDown; // goddamn, just ask my if you have any questions
    [SerializeField] private float targetEngagementDistance;
    [SerializeField] private float acceptableRange;


    [Header("Others")]
    private Rigidbody2D rb;
    private Animator animaRoyal;
    [SerializeField] private bool inCombat;
    [SerializeField] private bool shankingRange; // esfaquear
    private float lastSeenPlayer;
    private float lastRaycast;
    private float lastShot;
    private float lastSeenPlayer;

    [SerializeField] private float shootCooldown;

    public int facing; // -1 for left, 1 for right
    public bool walking;

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
            rb.velocity = new Vector2(facing * enemySpeed, 0);

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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, 200f);
            if (hit.collider != null) // can see player
            {
                GameObject hitObject = hit.collider.gameObject;

                Debug.log("Inimigo viu player!");
                //Debug.DrawRay(transform.position, directionToPlayer * 200f, Color.red);

                if (hitObject.CompareTag("Player")) // is player
                {
                    // could have put most of these in a single if, but eh
                    inCombat = true;
                }
            }
            lastRaycast = Time.time;
        }

        if (inCombat)
        {
            if (!CanSeePlayer())
            {
                if (Time.time > lastSeenPlayer + 30f)
                {
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

            if (Mathf.abs(targetX - transform.position.x) > acceptableRange)
            {
                // move towards targetX
                // face towards player
                facing = -direction; // this just works, i can assure you
                // override previous velocity
                rb.velocity = new Vector2(Mathf.Sign(targetX - transform.position.x) * enemySpeed, 0);
            }


            if (Time.time >= lastShot + shootCooldown)
            {
                // try to shoot at player
                Debug.Log("pewpewpew o inimigo tentou atirar!");
                lastShot = Timer.time;
            }

            // shank if too close
            if (Mathf.abs(playerRb.position.x - transform.position.x) < shankingRange)
            {
                Debug.Log("O inimigo tentou esfaquear o player!");
            }
        }


        //TODO: esse troço
        // // raycast against wall and missing floor to turn around
        // // will reuse hit
        // if (Physics2D.Raycast(transform.position, transform.position + facing * 0.5f, out hit) ||
        //     Physics2D.Raycast(transform.position + facing * 16f, transform.position + facing * 0.5f, out hit) ||)
        // {

        // }

        // these have side effects btw :wink:
        if (!CheckWall())
        { // only turn once, never know these damn edge cases
            CheckGround();
        }
    }

    bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, 200f);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Player")) // is player
            {
                return true;
            }
        }

        return false;
    }

    bool CheckWall()
    {
        // setting facing will automatically turn around the enemy on next frame btw
        Vector2 direction = facing == 1 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, wallRange);

        if (hit.collider != null)
        {
            //TODO: add check for terrain
            facing *= -1; // set to inverse
            return true;
        }

        return false;
    }

    bool CheckGround()
    {
        // same comment from before
        // direction is down
        Vector2 rayOrigin = transform.position + new Vector2(groundForward * facing);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundDown);

        if (hit.collider == null)
        {
            facing *= -1; // set to inverse
            return true;
        }

        return false;
    }
}