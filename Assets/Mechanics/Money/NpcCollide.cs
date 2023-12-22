using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCollide : MonoBehaviour
{

    public int hitBill;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Npc"))
        {
            GameManager.Instance.playerMoneyCount -= hitBill;
        }
    }
}
