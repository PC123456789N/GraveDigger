using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Rigidbody2D shot;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject EnemyCorpse;

    [SerializeField] public float CorpseHeight;
    [SerializeField] public GameObject AudioController;

    private Vector2 CorpsePos;
    // Start is called before the first frame update
    void Start()
    {
        shot = GetComponent<Rigidbody2D>();
        if (player == null) // se não foi atribuído no Inspector
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (AudioController == null) // se não foi atribuído no Inspector
        {
            AudioController = GameObject.FindGameObjectWithTag("AudioController");
        }
        
        

    }

    // Update is called once per frame
    void Update()
    {

        //transform.Translate(BulletSpeed * BulletDirection * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Destroi a bala quando colidir com QUALQUER coisa - o player

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hitted enemy");
            Destroy(other.gameObject);
            Vector2 CorpsePos = new Vector2(transform.position.x, transform.position.y - CorpseHeight);
            Instantiate
            (
                EnemyCorpse,
                CorpsePos,
                Quaternion.Euler(0, 0, 90)
            );
            AudioController.GetComponent<audioController>().PlayEnemyScream();    
            Destroy(gameObject);
        }

        else if (other.CompareTag("Player"))
        {
            
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
