using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoint;
    public SpawnData[] spawndata;
    float timer;
    int level;
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();    
    }
    void Update()
    {

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gametime / 10f),spawndata.Length-1);
        if(timer > spawndata[level].spawnTime)
        {
            Spawn();
            timer = 0f;
        }
        
    }
    

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //�ڽĿ�����Ʈ������ ���õǵ��� 1���� ����
        enemy.GetComponent<Enemy>().Init(spawndata[level]);
        
    }
}



[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
