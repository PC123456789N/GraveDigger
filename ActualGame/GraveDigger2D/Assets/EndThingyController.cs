using UnityEngine;

public class EndThingyController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Image endImage;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider2D other)
    {
        if (other.compareTag("Player") && !hasTriggered)
        {
            UnityEngine.Debug.Log("Fim chegado, parabeinz");
            endImage.gameObject.SetActive(true);
            hasTriggered = true;
        }
    }
}