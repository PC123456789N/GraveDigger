using System.Transactions;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks.Dataflow;
using System.Numerics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // codar sem ter o unity aberto pra testar vai ser terrivel
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private float visionRange = 200f;

    [SerializeField] private RigidBody2D playerRb; // to raycast against them

    private Rigidbody2D rb;
    private Animator anima;
    private float isPatrolling;
    private float lastSeenPlayer;

    public int facing; // -1 for left, 1 for right
    public bool walking;

    void Start()
    {
        facing = 1;
    }

    void Update()
    {
        // the walking variable is set in a different part
        if (walking)
        {
            rb.velocity = new Vector2(facing * enemySpeed, 0);

            transform.localScale = new Vector3(facing, 1, 1);
            anima.SetBool("Walking", true);
        }

        else
        {
            anima.SetBool("Walking", false);
        }

        // raycast to the player to see if the enemy can see them
        RaycastHit2D hit;
        Vector2 directionToPlayer = (playerRb.position - (Vector2)transform.position).normalized;

        if (Mathf.Sign(playerRb.position.x - transform.position.x) == facing) // is facing player
        {
            if (Physics2D.Raycast(transform.position, directionToPlayer, out hit, 200f)) // can see player
            {
                GameObject hitObject = hit.collider.gameObject;

                Debug.DrawRay(transform.position, directionToPlayer * 200f, Color.red);

                if (hitObject.CompareTag("Player")) // is player
                {
                    // could have put most of these in a single if, but eh
                    isPatrolling = true;
                }
            }
        }


        //TODO: esse troço
        // // raycast against wall and missing floor to turn around
        // // will reuse hit
        // if (Physics2D.Raycast(transform.position, transform.position + facing * 0.5f, out hit) ||
        //     Physics2D.Raycast(transform.position + facing * 16f, transform.position + facing * 0.5f, out hit) ||)
        // {

        // }
    }
}