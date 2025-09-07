using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Rigidbody2D shot;
    private Vector2 BulletDirection_ = Vector2.right;

    private Vector2 _BulletDirection = Vector2.left;
    // Start is called before the first frame update
    void Start()
    {
        shot = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        //transform.Translate(BulletSpeed * BulletDirection * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Destroi a bala quando colidir com QUALQUER coisa - o player

        if (other.CompareTag("Player")) ;

        else
        {
            Destroy(gameObject);
        }
    }
}
