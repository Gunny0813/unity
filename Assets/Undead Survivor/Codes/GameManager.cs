using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public static GameManager instance;
    public PoolManager pool;
    public Player player;
    public float gametime;
    public float maxGameTime = 2 *10f;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        gametime += Time.deltaTime;
        if (gametime > maxGameTime)
        {
            gametime = maxGameTime;
        }

    }
}
