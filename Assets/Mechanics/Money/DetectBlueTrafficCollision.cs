using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class responsible for detecting hits on NPC game objects.
/// </summary>
public class DetectBlueTrafficCollision : MonoBehaviour
{
    /// <summary>
    /// The amount of money subtracted from `gameManager.playerMoneyCount` when the player collides with an NPC.
    /// </summary>
    public int hitBill;

    /// <summary>
    /// ParticleSystem for hit npc car effect
    /// </summary>
    public ParticleSystem hitEffect;

    private GameManager gameManager;

    private void Start()
    {
        // Initialize the gameManager instance
        gameManager = GameManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "Npc" tag
        if (collision.gameObject.CompareTag("Npc"))
        {
            // Deduct money from the player's money count
            gameManager.playerMoneyCount -= hitBill;

            // Update the money count text using the formatted money value
            gameManager.moneyCountText.text = StringUtility.FormatMoney(gameManager.playerMoneyCount);

            // Play hit effect at the collision point
            PlayHitEffect(collision.contacts[0].point);

            //Remove the car from the board by deactivating it.
            collision.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Instantiate and play the hit effect at a given position.
    /// </summary>
    /// <param name="position">The position where the hit effect should be played.</param>
    private void PlayHitEffect(Vector3 position)
    {
        // Instantiate the hit effect at the specified position
        ParticleSystem effect = Instantiate(hitEffect, position, Quaternion.identity);

        // Play the particle system
        effect.Play();

        // Destroy the particle system after its duration
        Destroy(effect.gameObject, effect.main.duration);
    }
}
