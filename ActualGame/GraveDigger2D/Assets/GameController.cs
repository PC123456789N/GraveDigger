using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject MainMenu;
    [SerializeField] public Image Title1;
    [SerializeField] public Image Title2;
    [SerializeField] public Image Title3;
    [SerializeField] public GameObject PlayButton;
    [SerializeField] public GameObject PlayButton2;
    [SerializeField] public GameObject HUD;
    [SerializeField] public GameObject MedicMenu;
    [SerializeField] public Image LowHealthPrompt;
    [SerializeField] public PlayerController player;
    [SerializeField] public GameObject TheActualPlayerObjectAndNotTheScript;


    [Header("Menu Medico")]
    [SerializeField] private TextMeshProUGUI NumTrico;
    [SerializeField] private TextMeshProUGUI NumSinap;
    [SerializeField] private TextMeshProUGUI NumHalop;
    [SerializeField] private TextMeshProUGUI NumBicar;
    [SerializeField] private TextMeshProUGUI NumAmoto;
    public int QntTrico;
    public int QntSinap;
    public int QntHalop;
    public int QntBicar;
    public int QntAmoto;
    public bool MedicMenuOpen;
    public bool drugged;

    [Header("Respawnar")]
    [SerializeField] private Image DeathScreen
    [SerializeField] private GameObject SpawnPoint
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; //ALTER TO 0 AFTER TESTS
        MedicMenuOpen = false;

        drugged = false;
        QntTrico = 3;
        QntSinap = 3;
        QntHalop = 3;
        QntBicar = 3;
        QntAmoto = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.hp <= 0)
        {
            Debug.Log("Player morreu!");
            // ded
            DeathScreen.SetActive(true);

            Debug.Log("Esperando clicar no botão de respawnar");

            Debug.Log("AVISO: ------- Simulando como um clique aq, ligue a lógica dps");
            RespawnPlayer();
        } else if (player.hp <= 51)
        {
            LowHealthPrompt.gameObject.SetActive(true);
        } 
        else
        {
            LowHealthPrompt.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.H) && !MedicMenuOpen)
        {
            HUD.SetActive(false);
            MedicChart();
            Debug.Log("apertou H");
        }
        else if (Input.GetKeyUp(KeyCode.H) && MedicMenuOpen)
        {
            HUD.SetActive(true);
            MedicMenu.SetActive(false);
            MedicMenuOpen = false;
        }
    }

    //Menus
    public void StartGame()
    {
        StartCoroutine(PlayIntro());
    }

    public void StartGame2()
    {
        StartCoroutine(PlayIntroPt2());
    }

    //medic menu and DRUGS
    public void MedicChart()
    {
        MedicMenu.SetActive(true);
        NumTrico.text = $"{QntTrico}";
        NumSinap.text = $"{QntSinap}";
        NumHalop.text = $"{QntHalop}";
        NumBicar.text = $"{QntBicar}";
        NumAmoto.text = $"{QntAmoto}";
        MedicMenuOpen = true;
    }
    //functions
    public void UseTrico()
    {
        StartCoroutine(UseTricoIE());
    }
    public void UseSinap()
    {
        StartCoroutine(UseSinapIE());
    }
    public void UseHalop()
    {
        StartCoroutine(UseHalopIE());
    }
    public void UseBicar()
    {
        StartCoroutine(UseBicarIE());
    }
    public void UseAmoto()
    {
        StartCoroutine(UseAmotoIE());
    }

    //coroutines
    IEnumerator UseTricoIE()
    {
        if (!drugged && QntTrico > 0)
        {
            drugged = true;
            QntTrico -= 1;
            NumTrico.text = $"{QntTrico}";
            player.hp = 100;
            UnityEngine.Debug.Log("Used Trico");
            yield return new WaitForSecondsRealtime(10f);
            drugged = false;
            UnityEngine.Debug.Log("Effect passed");
        }
        else
        {
            UnityEngine.Debug.Log("Already Drugged");
        }

    }
    IEnumerator UseSinapIE()
    {
        if (!drugged && QntSinap > 0)
        {
            drugged = true;
            QntSinap -= 1;
            NumSinap.text = $"{QntSinap}";
            player.playerSpeed = 7f;
            UnityEngine.Debug.Log("Used Sinap");
            yield return new WaitForSecondsRealtime(30f);
            player.playerSpeed = 5f;
            drugged = false;
            UnityEngine.Debug.Log("Effect passed");
        }
        else
        {
            UnityEngine.Debug.Log("Already Drugged");
        }

    }
    IEnumerator UseHalopIE()
    {
        if (!drugged && QntHalop > 0)
        {
            drugged = true;
            QntHalop -= 1;
            NumHalop.text = $"{QntHalop}";
            player.reloadSpeed = 1f;
            UnityEngine.Debug.Log("Used Halop");
            yield return new WaitForSecondsRealtime(60f);
            player.reloadSpeed = 2f;
            drugged = false;
            UnityEngine.Debug.Log("Effect passed");
        }
        else
        {
            UnityEngine.Debug.Log("Already Drugged");
        }
    }
    IEnumerator UseBicarIE()
    {
        if (!drugged && QntBicar > 0)
        {
            drugged = true;
            QntSinap -= 1;
            NumSinap.text = $"{QntSinap}";
            player.hp += 30;
            UnityEngine.Debug.Log("Used Bicar");
            yield return new WaitForSecondsRealtime(10f);
            drugged = false;
            UnityEngine.Debug.Log("Effect passed");
        }
        else
        {
            UnityEngine.Debug.Log("Already Drugged");
        }
    }
    IEnumerator UseAmotoIE()
    {
        if (!drugged && QntAmoto > 0)
        {
            drugged = true;
            QntAmoto -= 1;
            NumAmoto.text = $"{QntAmoto}";
            player.jumpForce = 15f;
            UnityEngine.Debug.Log("Used Amoto");
            yield return new WaitForSecondsRealtime(60f);
            player.jumpForce = 10f;
            drugged = false;
            UnityEngine.Debug.Log("Effect passed");
        }
        else
        {
            UnityEngine.Debug.Log("Already Drugged");
        }
    }

    //Scenery
    IEnumerator PlayIntro() //Makes the Intro
    {
        PlayButton.SetActive(false);
        Title1.CrossFadeAlpha(0f, 1f, true); // 0 = invisível, duration, ignores the time thingie
        Debug.Log("com1");

        yield return new WaitForSecondsRealtime(2f);

        Title2.CrossFadeAlpha(0f, 1f, true); // 0 = invisível, duration, ignores the time thingie
        Debug.Log("com2");
        Title1.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(1.1f);
        Title2.gameObject.SetActive(false);
    }

    IEnumerator PlayIntroPt2()
    {
        PlayButton2.SetActive(false);
        Title3.CrossFadeAlpha(0f, 1f, true);
        yield return new WaitForSecondsRealtime(1.1f);
        MainMenu.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1;
    }

    IEnumerator RespawnPlayer()
    {
        Debug.Log("Player respawnou!");
        // dedn't
        DeathScreen.SetActive(false);
        player.hp = 100;
        TheActualPlayerObjectAndNotTheScript.transform.position = SpawnPoint.transform.position;
        Debug.Log("Player teleportado de volta");
    }
}
