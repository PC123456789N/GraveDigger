using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;

    private Animator anima;
    private Vector2 playerMove;

    private Rigidbody2D Player;

    [SerializeField] public Camera mainCamera;

    public bool jumping;

    public bool walking;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        jumping = false;
        walking = false;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //movimentação + animação

        Player.velocity = new Vector2(moveX * playerSpeed, Player.velocity.y);

        if (moveX > 0 && !jumping)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anima.SetBool("Walking", true);
            walking = true;
        }

        else if (moveX < 0 && !jumping)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anima.SetBool("Walking", true);
            walking = true;
        }

        else
        {
            anima.SetBool("Walking", false);
            walking = false;
        }

        //pulando ou nn

        if (Input.GetKey(KeyCode.Space) && Player.velocity.y == 0)
        {
            Jump(jumping);
        }

        else if (Player.velocity.y == 0)
        {
            anima.SetBool("Jumping", false);
        }

        else
        {
            jumping = false;

        }

        mainCamera.transform.position = new Vector3(
            Player.position.x,
            Player.position.y + 2f,
            mainCamera.transform.position.z  // mantém o Z da câmera 2D
        );
    }

    void FixedUpdate()
    {
        jumping = false;
    }

    private void Jump(bool AlredyJumping)
    {
        if (AlredyJumping) return;

        else
        {
            jumping = true;
            Player.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            anima.SetBool("Jumping", true);
        }
    }

    
}
