using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public bool spawning; // debug
    public int levelSelector;

    private GameObject[] portals;
    private List<GameObject> activeUnits;
    public GameObject DeathParticles;

    public GameObject[] enemies1;
    public GameObject[] bosses1;
    public GameObject[] enemies2;
    public GameObject[] bosses2;

    public GameObject[] cEnemies;
    public GameObject[] cBosses;

    private Vector3 playerPos;

    public float spawnDelay;
    public float bossDelay;
    private float bossCountdown;
    private float spawnCountdown;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnCountdown = spawnDelay;
        bossCountdown = bossDelay;
        portals = GameObject.FindGameObjectsWithTag("SpawnPortal");
        activeUnits = new List<GameObject>();

        if (levelSelector == 1)
        {
            cEnemies = enemies1;
            cBosses = bosses1;
        } else if (levelSelector == 2) {
            cEnemies = enemies2;
            cBosses = bosses2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // keep the time in ?seconds?

        //foreach (GameObject obj in activeUnits)
        //{
        //    if (obj == null)
        //    {
        //        activeUnits.Remove(obj);
        //    }
        //}
        if (bossCountdown < 0 )
        {
            SpawnBoss();
            bossCountdown = bossDelay;
        }
        else
        {
            bossCountdown -= Time.deltaTime;
        }
        if ((spawnCountdown < 0 || activeUnits.Count <= 0) && spawning)
        {
            SendWave();
            spawnCountdown = spawnDelay;
        } else
        {
            spawnCountdown -= Time.deltaTime;
        }
    }

    public int getLevelSelector()
    {
        return levelSelector;
    }

    // Spawn enemy units around the players view (>8 units distance)
    private void SendWave()
    {
        playerPos = GameObject.Find("Player").transform.position;
        int spawnDist = 12; // distance based on view

        // TODO make the spawning circular around player
        Vector3 xDir = new Vector3(spawnDist, 0, 0);
        Vector3 yDir = new Vector3(0, spawnDist, 0);

        if (levelSelector == 1)
        {
            int unitCount = 4 + Mathf.FloorToInt(timer / 14.0f); // deviding by 60 gives minute (smaller number more unitCount)
                                                                 // Spawn units increasing nr by time, check out of bounds and move the units to other spawn direction
            SpawnUnits(unitCount, playerPos + xDir);
            SpawnUnits(unitCount, playerPos - xDir);
            SpawnUnits(unitCount, playerPos + yDir);
            SpawnUnits(unitCount, playerPos - yDir);
        } else if (levelSelector == 2)
        {
            int unitCount = 2 + Mathf.FloorToInt(timer / 20.0f); // deviding by 60 gives minute (smaller number more unitCount)
                                                                 // Spawn units increasing nr by time, check out of bounds and move the units to other spawn direction
            SpawnUnits(unitCount, playerPos + xDir);
            SpawnUnits(unitCount, playerPos - xDir);
            SpawnUnits(unitCount, playerPos + yDir);
            SpawnUnits(unitCount, playerPos - yDir);
        }
    }

    private void SpawnUnits(float unitCount, Vector3 direction)
    {
        
        for (int i = 0; i < unitCount; i++)
        {   // spawn units outside view based on playerpos (check for out of world bounds)
            if (!(direction.x > 75 || direction.y > 75 || direction.x < -75 || direction.y < -75))
            {
                Vector3 addedRandom = new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f), 0);
                GameObject unit = Instantiate(cEnemies[0], direction+addedRandom, transform.rotation);
                activeUnits.Add(unit);

            }
        }
    }

    private void SpawnBoss()
    {
        //Vector3 addedRandom = new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f), 0);
        GameObject unit = Instantiate(cBosses[0], Vector3.up*3, transform.rotation);
        activeUnits.Add(unit);
    }

    public void MakeDeathParticles(Vector3 pos)
    {
        Instantiate(DeathParticles, pos, new Quaternion(0, 0 ,0, 0));
    }
}
