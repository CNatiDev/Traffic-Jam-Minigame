using System.Collections;
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

    public float dezactivateParentAfterTime;

    /// <summary>
    /// The particle system that will play when the money is collected
    /// </summary>
    public ParticleSystem collectEffect;

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
            collectEffect.Play();

            // Add the money value to the player's total money count.
            gameManager.playerMoneyCount += value;

            // Update the money count text in the UI.
            gameManager.moneyCountText.text = StringUtility.FormatMoney(gameManager.playerMoneyCount);

            StartCoroutine(DezactivateParent(dezactivateParentAfterTime, gameObject.transform.parent.gameObject));


            // Deactivate the money pickup object.
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator DezactivateParent(float dezactivateAfterTime, GameObject parent)
    {
        yield return new WaitForSeconds(dezactivateAfterTime);
        parent.SetActive(false);

    }
}
