using UnityEngine;

/// <summary>
/// The MoneyValue class represents a money pickup in the game.
/// </summary>
public class MoneyValue : MonoBehaviour
{
    /// <summary>
    /// The value of the money pickup.
    /// </summary>
    public int value;

    /// <summary>
    /// The particle system that will play when the money is collected
    /// </summary>
    public ParticleSystem particleSystem;

    /// <summary>
    /// Reference to the GameManager.
    /// </summary>
    private GameManager gameManager;

    private void Start()
    {
        // Get a reference to the GameManager instance.
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the "Player" tag.
        if (other.gameObject.CompareTag("Player"))
        {
            //Play the collect effect
            particleSystem.Play();

            // Add the money value to the player's total money count.
            gameManager.playerMoneyCount += value;

            // Update the money count text in the UI.
            gameManager.moneyCountText.text = StringUtility.FormatMoney(gameManager.playerMoneyCount);

            // Deactivate the money pickup object.
            gameObject.SetActive(false);
        }
    }
}
