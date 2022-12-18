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

    public float spawnDelay;
    private float spawnCountdown;

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

    private void SendWave()
    {
        
    }

    public void MakeDeathParticles(Vector3 pos)
    {
        Instantiate(DeathParticles, pos, new Quaternion(0, 0 ,0, 0));
    }
}
