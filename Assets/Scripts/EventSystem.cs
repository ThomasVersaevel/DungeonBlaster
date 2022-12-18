using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    private GameObject[] portals;
    private List<GameObject> activeUnits;
    public GameObject DeathParticles;

    public GameObject Slime;
    public GameObject Skeleton;
    public GameObject Golem;
    public GameObject Bat;

    private Vector3 playerPos;

    public float spawnDelay;
    private float spawnCountdown;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnCountdown = spawnDelay;
        portals = GameObject.FindGameObjectsWithTag("SpawnPortal");
        activeUnits = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // keep the time

        foreach (GameObject obj in activeUnits)
        {
            if (obj == null)
            {
                activeUnits.Remove(obj);
            }
        }
        if (spawnCountdown < 0 && activeUnits.Count <= 0)
        {
            SendWave();
            spawnCountdown = spawnDelay;
        } else
        {
            spawnCountdown -= Time.deltaTime;
        }
    }
    // Spawn enemy units around the players view (>8 units distance)
    private void SendWave()
    {
        playerPos = GameObject.Find("Player").transform.position;

        Vector3 xDir = new Vector3(8, 0, 0);
        Vector3 yDir = new Vector3(0, 8, 0);

        int unitCount = 4 + Mathf.FloorToInt(timer / 30.0f); // deviding by 60 gives minute

        SpawnUnits(unitCount, playerPos + xDir);
        SpawnUnits(unitCount, playerPos - xDir);
        SpawnUnits(unitCount, playerPos + yDir);
        SpawnUnits(unitCount, playerPos - yDir);
    }

    private void SpawnUnits(float unitCount, Vector3 direction)
    {
        
        for (int i = 0; i < unitCount; i++)
        {   // spawn units outside view based on playerpos (check for out of world bounds)
            if (!(direction.x > 75 || direction.y > 75 || direction.x < -75 || direction.y < -75))
            {
                GameObject unit = Instantiate(Bat, direction, transform.rotation);
                activeUnits.Add(unit);

            }
        }
    }

    public void MakeDeathParticles(Vector3 pos)
    {
        Instantiate(DeathParticles, pos, new Quaternion(0, 0 ,0, 0));
    }
}
