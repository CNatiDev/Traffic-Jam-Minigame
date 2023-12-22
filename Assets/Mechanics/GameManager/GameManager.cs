using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        { if (instance == null)
                Debug.LogError("Game Manager is null");
            return instance; 
        }
    }
    [Header("PLAYER MONEY")]
    public int playerMoneyCount;
    public int maxActiveMoneyObjects;
    [Header("PLAYER CAR")]
    public GameObject playerCar;
    public float playerCarSpeed;
    [Header("NPC")]
    public int maxActiveNpcCar;
    public float npcSpeed;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //Assign player car speed on first frame
        if(playerCar.GetComponent<IMoveable>()!=null)
            playerCar.GetComponent<IMoveable>().carSpeed = playerCarSpeed;
    }
}
