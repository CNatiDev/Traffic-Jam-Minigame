using System.Collections;
using UnityEngine;

/// <summary>
/// Deactivates NPCs upon entering the trigger zone.
/// </summary>
public class BlueTrafficDezactivator : MonoBehaviour
{
    /// <summary>
    /// Time delay before destroying the NPC after entering the trigger zone.
    /// </summary>
    public float dezactivateTime;

    /// <summary>
    /// Called when another collider enters the trigger zone.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is an NPC
        if (other.CompareTag("Npc"))
        {
            // Deactivate the NPC after a delay
            StartCoroutine(DeactivateNpcDelayed(other.gameObject));
        }
    }

    /// <summary>
    /// Deactivates the NPC after a delay.
    /// </summary>
    /// <param name="npcGameObject">The NPC GameObject to deactivate.</param>
    private IEnumerator DeactivateNpcDelayed(GameObject npcGameObject)
    {
        yield return new WaitForSeconds(dezactivateTime);

        // Check if the NPC still exists (may have been destroyed by other means)
        if (npcGameObject != null)
        {
            npcGameObject.SetActive(false);
        }
    }
}
