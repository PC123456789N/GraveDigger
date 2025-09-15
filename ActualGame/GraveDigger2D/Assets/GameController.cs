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
    [SerializeField] public GameObject PlayButton;
    [SerializeField] public GameObject HUD;
    [SerializeField] public GameObject MedicMenu;
    [SerializeField] public Image LowHealthPrompt;
    [SerializeField] public PlayerController player;


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
        if (player.hp <= 50)
        {
            
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.H) && !MedicMenuOpen)
        {
            MedicChart();
            Debug.Log("apertou H");
        }
        else if (Input.GetKeyUp(KeyCode.H) && MedicMenuOpen)
        {
            MedicMenu.SetActive(false);
            MedicMenuOpen = false;
        }
    }

    //Menus
    public void StartGame()
    {
        StartCoroutine(PlayIntro());
    }

    //medic menu and DRUGS
    public void MedicChart()
    {
        MedicMenu.SetActive(true);
        NumTrico.text = $"{QntTrico}x";
        NumSinap.text = $"{QntSinap}x";
        NumHalop.text = $"{QntHalop}x";
        NumBicar.text = $"{QntBicar}x";
        NumAmoto.text = $"{QntAmoto}x";
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
        if (!drugged)
        {
            drugged = true;
            QntTrico -= 1;
            NumTrico.text = $"{QntTrico}x";
            player.hp = 100;
            UnityEngine.Debug.Log("Used Trico");
            yield return new WaitForSecondsRealtime(10f);
            drugged = false;
            UnityEngine.Debug.Log("Effect passed");
        }
        else{
            UnityEngine.Debug.Log("Already Drugged");
        }
        
    }
    IEnumerator UseSinapIE()
    {
        if (!drugged)
        {
            drugged = true;
            QntSinap -= 1;
            NumSinap.text = $"{QntSinap}x";
            player.playerSpeed = 8f;
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
        if (!drugged)
        {
            drugged = true;
            QntHalop -= 1;
            NumHalop.text = $"{QntHalop}x";
            player.reloadSpeed = 5f;
            UnityEngine.Debug.Log("Used Halop");
            yield return new WaitForSecondsRealtime(60f);
            player.reloadSpeed = 10f;
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
        if (!drugged)
        {
            drugged = true;
            QntSinap -= 1;
            NumSinap.text = $"{QntSinap}x";
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
        if (!drugged)
        {
            drugged = true;
            QntAmoto -= 1;
            NumAmoto.text = $"{QntAmoto}x";
            player.jumpForce = 15f;
            UnityEngine.Debug.Log("Used Amoto");
            yield return new WaitForSecondsRealtime(60f);
            player.jumpForce = 10f;
            drugged = false;
            UnityEngine.Debug.Log("Effect passed");
        }
        else{
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
        MainMenu.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1;
    }
}
