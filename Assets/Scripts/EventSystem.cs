using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour
{
    public bool spawning; // debug
    public int levelSelector;
    private static float badLuckMultiplier = 0;

    private List<GameObject> activeUnits;
    public GameObject DeathParticles;
    public GameObject PauseOverlay;
    public GameObject QuitPanel;
    private bool paused = false;

    [SerializeField] private GameObject Hotbar;


    // Enemy prefabs per level
    public List<GameObject> enemies1;
    public List<GameObject> bosses1;
    public List<GameObject> enemies2;
    public List<GameObject> bosses2;

    public List<GameObject> currentEnenemies;
    public List<GameObject> cBosses;

    private Vector3 playerPos;

    public float spawnDelay;
    public float bossDelay;
    private float bossCountdown;
    private float spawnCountdown;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
        spawnCountdown = spawnDelay;
        bossCountdown = bossDelay;
        activeUnits = new List<GameObject>();
        // Increment run nr for saving highscores
        PlayerPrefs.SetInt("RunNr", PlayerPrefs.GetInt("RunNr") + 1);

        if (levelSelector == 1)
        {
            currentEnenemies = enemies1;
            cBosses = bosses1;
        } else if (levelSelector == 2) {
            currentEnenemies = enemies2;
            cBosses = bosses2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // keep the time in ?seconds?
        playerPos = GameObject.Find("Player").transform.position;

        // key handler for pausing using keyboard
        if (Input.GetKeyDown("escape"))
        {
            if (paused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }

        if (bossCountdown < 0 )
        {
            SpawnBoss();
            Invoke("LightningStrike", 0f);
            Invoke("ResetLight", 0.1f);
            Invoke("LightningStrike", 0.15f);
            Invoke("ResetLight", 0.35f);
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
    public void GameOver()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60.0f);
        PlayerPrefs.SetString("Score", PlayerPrefs.GetString("Score") + "\n" + "Run: " + PlayerPrefs.GetInt("RunNr") + " time survived: " + string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    public int GetLevelSelector()
    {
        return levelSelector;
    }

    private Vector3 CirclePoint(float radius)
    {
        return Random.insideUnitCircle.normalized * radius;
    }

    private void LightningStrike()
    {
        gameObject.GetComponent<Light2D>().intensity = 1;
    }
    private void ResetLight()
    {
        gameObject.GetComponent<Light2D>().intensity = 0.6f;
    }

    // Pause game by setting time to 0, also open pause overlay and allow resume game from click
    public void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        // Activate pause overlay
        PauseOverlay.SetActive(true);
        Hotbar.transform.position += new Vector3(0, 1.8f, 0);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
        PauseOverlay.SetActive(false);
        Hotbar.transform.position -= new Vector3(0, 1.8f, 0);
    }
    public void QuitGame()
    {
        QuitPanel.SetActive(!QuitPanel.activeSelf);
    }

    // Spawn enemy units around the players view (>8 units distance)
    private void SendWave()
    {
        int spawnDist = 12; // distance based on view
        Vector3 spawnPoint = CirclePoint(spawnDist);
        // TODO make the spawning circular around player
        Vector3 xDir = new Vector3(spawnDist, 0, 0);
        Vector3 yDir = new Vector3(0, spawnDist, 0);

        if (levelSelector == 1)
        {
            int unitCount = 4 + Mathf.FloorToInt(timer / 14.0f); // deviding by 60 gives minute (smaller number more unitCount)
                                                                 // Spawn units increasing nr by time, check out of bounds and move the units to other spawn direction
            SpawnUnits(unitCount, playerPos + CirclePoint(spawnDist));
            SpawnUnits(unitCount, playerPos - CirclePoint(spawnDist));
            SpawnUnits(unitCount, playerPos + CirclePoint(spawnDist));
            SpawnUnits(unitCount, playerPos - CirclePoint(spawnDist));
        } else if (levelSelector == 2)
        {
            int unitCount = 2 + Mathf.FloorToInt(timer / 20.0f); // deviding by 60 gives minute (smaller number more unitCount)
                                                                 // Spawn units increasing nr by time, check out of bounds and move the units to other spawn direction
            SpawnUnits(unitCount, playerPos + CirclePoint(spawnDist));
            SpawnUnits(unitCount, playerPos + CirclePoint(spawnDist));
            SpawnUnits(unitCount, playerPos + CirclePoint(spawnDist));
            SpawnUnits(unitCount, playerPos + CirclePoint(spawnDist));
        }
    }

    private void SpawnUnits(float unitCount, Vector3 direction)
    {
        
        for (int i = 0; i < unitCount; i++)
        {   // spawn units outside view based on playerpos (check for out of world bounds)
            if (!(direction.x > 75 || direction.y > 75 || direction.x < -75 || direction.y < -75))
            {
                Vector3 addedRandom = new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f), 0);
                GameObject unit = Instantiate(currentEnenemies[0], direction+addedRandom, transform.rotation);
                activeUnits.Add(unit);

            }
        }
    }

    private void SpawnBoss()
    {
        //Vector3 addedRandom = new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f), 0);
        GameObject unit = Instantiate(cBosses[0], playerPos + Vector3.up*6, transform.rotation);
        activeUnits.Add(unit);
    }

    // deprecated
    public void MakeDeathParticles(Vector3 pos)
    {
        Instantiate(DeathParticles, pos, new Quaternion(0, 0, 0, 0));
    }

    public static float PseudoRandomGenerator(float chancePercent, float missPercent)
    {
        int roll = Random.Range(0, 100); // roll percentage
        if (roll < chancePercent) // hit (exclusive since Random.Range is 0-99)
        {

        }
        return roll;
    }
}
