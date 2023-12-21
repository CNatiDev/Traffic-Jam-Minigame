using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DezactivateNpc : MonoBehaviour
{
    //Destroy npc after destroy time 
    public float destroyTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Npc"))
            other.gameObject.SetActive(false);
    }
}
