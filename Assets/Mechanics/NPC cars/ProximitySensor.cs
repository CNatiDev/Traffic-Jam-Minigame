using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
    public float minDistance;
    public string npcTag; // Tag for the NPC
    public Transform frontSensorPosition;
    public Transform LSensorPosition;
    public Transform RSensorPosition;
    public float LRSensorRange;
    public bool isMain = true;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 maxPosition = frontSensorPosition.position + frontSensorPosition.forward * minDistance;

        // Check for NPC and change Gizmo color to red if detected
        if (ToClose())
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawLine(frontSensorPosition.position, maxPosition);
        Gizmos.DrawWireSphere(LSensorPosition.position, LRSensorRange);
        Gizmos.DrawWireSphere(RSensorPosition.position, LRSensorRange);

    }

    public bool ToClose()
    {
        Ray ray = new Ray(frontSensorPosition.position, frontSensorPosition.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, minDistance))
        {
            // Check if the hit object has the specified NPC tag
            if (hit.collider.CompareTag(npcTag)) //&& hit.collider.GetComponent<ProximitySensor>().isMain !=isMain)
            {
                Debug.Log("First");
                return true; // NPC detected
            }
        }

        // Perform sphere check and filter out the parent collider
        Collider[] colliders_L = new Collider[10]; // Adjust the array size as needed
        int numColliders = Physics.OverlapSphereNonAlloc(LSensorPosition.position, LRSensorRange, colliders_L);

        for (int i = 0; i < numColliders; i++)
        {
            // Check if the collider has the specified NPC tag and is not the parent collider
            if (colliders_L[i].CompareTag(npcTag) 
                && colliders_L[i].transform != transform 
                && colliders_L[i].GetComponent<CarNpc>().mainStreetCar !=isMain)
            {
                Debug.Log("Second");
                return true; // NPC detected
            }
        }
        // Perform sphere check and filter out the parent collider
        Collider[] colliders_R = new Collider[10]; // Adjust the array size as needed
        int numCollidersR = Physics.OverlapSphereNonAlloc(RSensorPosition.position, LRSensorRange, colliders_R);

        for (int i = 0; i < numCollidersR; i++)
        {
            // Check if the collider has the specified NPC tag and is not the parent collider
            if (colliders_R[i].CompareTag(npcTag) 
                && colliders_R[i].transform != transform
                && colliders_R[i].GetComponent<CarNpc>().mainStreetCar != isMain)
            {
                Debug.Log("Second");
                return true; // NPC detected
            }
        }

        return false; // No NPC detected
    }
}
