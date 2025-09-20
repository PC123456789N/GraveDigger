using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 5f;
    [SerializeField] private TextMeshProUGUI AmmoAmountText;
    [SerializeField] public GameObject Bullet;
    private Animator anima;
    private Vector2 playerMove;

    private Rigidbody2D Player;

    private int shootingAmount; // fodasse o nome :dedo_do_meio:

    private float lastShotTime;

    private int ammoAmount = 6; // max is 6, as like in a revolver :+1:

    private bool reloading;

    public float reloadSpeed;

    [SerializeField] public float shootCooldown;

    [SerializeField] public Camera mainCamera;
    [SerializeField] public audioController GunVFX;

    public bool jumping;

    public bool walking;
    public bool armed;
    public int facing;
    public int hp;

    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        jumping = false;
        walking = false;
        facing = 1;
        armed = false;
        hp = 100;
        jumpForce = 10; //10 é standart
        reloadSpeed = 2f; //2 é standart
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
            facing = 1;
        }

        else if (moveX < 0 && !jumping)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anima.SetBool("Walking", true);
            walking = true;
            facing = -1;
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

        //Tiro

        if (Input.GetMouseButtonUp(0) && armed)
        {
            if (ammoAmount == 0)
            {
                StartCoroutine(Reload());
            }
            else
            {
                StartCoroutine(Shoot(facing));
            }
        }

        //sacar arma

        if (Input.GetKeyUp(KeyCode.E) && !armed && !reloading)
        {
            armed = true;
            anima.SetBool("armed", true);
        }
        else if (Input.GetKeyUp(KeyCode.E) && armed && !reloading)
        {
            armed = false;
            anima.SetBool("armed", false);
        }
        else
        {
            
        }


        //Controle camera

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
            Player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anima.SetBool("Jumping", true);
        }
    }

    IEnumerator Shoot(float facing)
    {   
        // 1.5 seconds of cooldown
        if (Time.time >= lastShotTime + shootCooldown)
        {
            shootingAmount += 1;

            lastShotTime = Time.time;

            ammoAmount -= 1;

            anima.SetBool("Shot", true);

            Vector2 BulletPos = new Vector2(Player.position.x + facing, Player.position.y + 1.3f);
            GameObject BulletFired = Instantiate(Bullet, BulletPos, Quaternion.Euler(0, 0, 90));
            GunVFX.PlayAudioShot();

            Rigidbody2D bulletRB = BulletFired.GetComponent<Rigidbody2D>();

            AmmoAmountText.text = $"{ammoAmount}/6";

            if (facing > 0)
            {
                bulletRB.velocity = Vector2.right * +20;
            }

            else if (facing < 0)
            {
                bulletRB.velocity = Vector2.right * -20;
            }
            GunVFX.PlayCockShot();
            yield return new WaitForSeconds(0.2f);

            shootingAmount -= 1;

            if (shootingAmount == 0)
            {
                anima.SetBool("Shot", false);
            }
        }
    }

    IEnumerator Reload()
    {
        if (reloading == false)
        {
            Debug.Log("Reloading");
            AmmoAmountText.text = $"R";
            reloading = true;
            // começar animação de recarregar aq
            GunVFX.PlayReloadShot();
            yield return new WaitForSeconds(reloadSpeed);

            GunVFX.PlayReloadShot();
            yield return new WaitForSeconds(reloadSpeed);

            GunVFX.PlayReloadShot();
            yield return new WaitForSeconds(reloadSpeed);

            GunVFX.PlayReloadShot();
            yield return new WaitForSeconds(reloadSpeed);

            GunVFX.PlayReloadShot();
            yield return new WaitForSeconds(reloadSpeed);

            GunVFX.PlayReloadShot();

            ammoAmount = 6;
            AmmoAmountText.text = $"{ammoAmount}/6";
            reloading = false;
        }    
    }
    
}
