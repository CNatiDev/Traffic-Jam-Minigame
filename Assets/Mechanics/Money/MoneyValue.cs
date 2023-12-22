using UnityEngine;

public class MoneyValue : MonoBehaviour
{
    public int value;

    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.playerMoneyCount += value;
            gameObject.SetActive(false);
        }
    }
}
