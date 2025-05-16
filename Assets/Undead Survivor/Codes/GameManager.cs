using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public static GameManager instance;
    [Header("#Game Control")]
    public float gametime;
    public float maxGameTime = 2 * 10f;
    [Header("#Game Object")]
    public PoolManager pool;
    public Player player;

    [Header("#Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

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
    public void GetExp()
    {
        exp++;
        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
