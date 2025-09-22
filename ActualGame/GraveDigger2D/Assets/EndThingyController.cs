using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class EndThingyController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] public Image endImage;
    [SerializeField] public Image VERYBLACKImage;
    [SerializeField] public Image BLACKPanel;
    [SerializeField] public Image Letter;
    [SerializeField] public Image FinalMessage;
    [SerializeField] public Text Title;
    [SerializeField] public Text EndText1;
    [SerializeField] public Text EndText2;
    [SerializeField] public Text EndText3;
    [SerializeField] public GameObject NextDialogBTN;
    [SerializeField] public GameObject ReadLetterBTN;
    [SerializeField] public GameObject ContinueBTN;
    [SerializeField] public GameObject ExitBTN;
    public int NumDialog;
    private bool hasTriggered = false;

    void Start()
    {
        NumDialog = 1;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            UnityEngine.Debug.Log("Fim chegado, parabeinz");
            StartCoroutine(StartEnding());
            hasTriggered = true;
        }
    }

    public void NextDialog()
    {
        StartCoroutine(NextDialogue(NumDialog));
    }
    public void ReadLetterFunc()
    {
        StartCoroutine(ReadLetter());
    }
    public void ENDTHISMISERY()
    {
        StartCoroutine(ReadFinalMessage());
    }

    //IENUMERATORS
    IEnumerator StartEnding()
    {
        VERYBLACKImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);
        endImage.gameObject.SetActive(true);
        VERYBLACKImage.CrossFadeAlpha(0f, 2f, true);

        yield return new WaitForSeconds(3f);
        BLACKPanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        Title.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        EndText1.gameObject.SetActive(true);
        NextDialogBTN.SetActive(true);

    }
    IEnumerator NextDialogue(int NumDialogFunc)
    {
        if (NumDialogFunc == 1)
        {
            EndText1.CrossFadeAlpha(0f, 2f, true);
            yield return new WaitForSecondsRealtime(2.5f);
            EndText1.gameObject.SetActive(false);

            EndText2.gameObject.SetActive(true);
            NumDialog = 2;
        }
        else if (NumDialog == 2)
        {
            EndText2.CrossFadeAlpha(0f, 2f, true);
            yield return new WaitForSecondsRealtime(2.5f);
            EndText2.gameObject.SetActive(false);

            EndText3.gameObject.SetActive(true);
            EndText3.CrossFadeAlpha(1f, 2f, true);
            NumDialog = 3;
            NextDialogBTN.SetActive(false);
            ReadLetterBTN.SetActive(true);
        }

    }
    IEnumerator ReadLetter()
    {
        ReadLetterBTN.SetActive(false);
        VERYBLACKImage.gameObject.SetActive(true);
        EndText3.CrossFadeAlpha(0f, 2f, true);


        yield return new WaitForSecondsRealtime(2f);
        Letter.gameObject.SetActive(true);
        ContinueBTN.SetActive(true);
    }
    IEnumerator ReadFinalMessage()
    {
        VERYBLACKImage.gameObject.SetActive(true);
        VERYBLACKImage.CrossFadeAlpha(0f, 2f, true);
        Letter.CrossFadeAlpha(0f, 2f, true);
        BLACKPanel.CrossFadeAlpha(0f, 2f, true);
        yield return new WaitForSecondsRealtime(2f);

        FinalMessage.gameObject.SetActive(true);
        ContinueBTN.SetActive(false);
        ExitBTN.SetActive(true);
    }
}