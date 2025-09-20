using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRoyal : MonoBehaviour
{
    private Rigidbody2D shot;
    [SerializeField] private GameObject player;
    [SerializeField] public GameObject AudioController;
    // Start is called before the first frame update
    void Start()
    {
        shot = GetComponent<Rigidbody2D>();
        if (player == null) // se não foi atribuído no Inspector
        {
            PlayerController pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
        if (AudioController == null) // se não foi atribuído no Inspector
        {
            AudioController = GameObject.FindGameObjectWithTag("AudioController");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        // Destroi a bala quando colidir com QUALQUER coisa - o player

        if (other.CompareTag("Player"))
        {
            Debug.Log("hitted Player");
            PlayerController pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            pc.hp -= 50;
            AudioController.GetComponent<audioController>().PlayPlayerScream();
            Destroy(gameObject);
        }

        else if (other.CompareTag("Enemy"))
        {
            
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
