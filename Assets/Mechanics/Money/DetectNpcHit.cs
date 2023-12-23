using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectNpcHit : MonoBehaviour
{
    public int hitBill;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Npc"))
        {
            gameManager.playerMoneyCount -= hitBill;
            gameManager.moneyCountText.text = StringUtility.FormatMoney(gameManager.playerMoneyCount);
            
        }
    }
}
