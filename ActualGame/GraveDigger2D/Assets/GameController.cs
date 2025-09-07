using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] public GameObject MainMenu;
    [SerializeField] public Image Title1;
    [SerializeField] public Image Title2;
    [SerializeField] public GameObject PlayButton;
    [SerializeField] public GameObject MedicMenu;
    [SerializeField] public GameObject GunReloadMenu;

    public bool MedicMenuOpen;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; //ALTER TO 0 AFTER TESTS
        MedicMenuOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

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

    public void MedicChart()
    {
        MedicMenu.SetActive(true);
        MedicMenuOpen = true;
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
        Time.timeScale = 1;
    }
}
