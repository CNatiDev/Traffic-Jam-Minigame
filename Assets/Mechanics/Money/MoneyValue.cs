using UnityEngine;

public class MoneyValue : MonoBehaviour
{
    // The value of the money pickup
    public int value;

    // Reference to the GameManager
    private GameManager gameManager;

    // Called when the script is started
    private void Start()
    {
        // Get a reference to the GameManager instance
        gameManager = GameManager.Instance;
    }

    // Called when another Collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the "Player" tag
        if (other.gameObject.CompareTag("Player"))
        {
            // Add the money value to the player's total money count
            gameManager.playerMoneyCount += value;

            // Update the money count text in the UI
            gameManager.moneyCountText.text = StringUtility.FormatMoney(gameManager.playerMoneyCount);

            // Deactivate the money pickup object
            gameObject.SetActive(false);
        }
    }
}
