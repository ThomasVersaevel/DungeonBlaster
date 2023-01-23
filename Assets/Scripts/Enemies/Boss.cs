using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Boss : MonoBehaviour
{
    public GameObject BossUI; // Entire ui prefab, child 0 is sprite, 1 is hpbar, 2 is appeartext, 3 is bossname
    public Sprite BossHPSprite;
    private AbstractEnemy enemyScript;
    private float curVelocity = 0;
    private TMP_Text BossText;
    public string BossName;
    private Slider BossHPBar;

    // game object name for instantiated UI
    private GameObject UI;

    // setup boss UI
    void Start()
    { 
        UI = Instantiate(BossUI, GameObject.FindGameObjectWithTag("GameControllerCanvas").transform.position, transform.rotation, 
            GameObject.FindGameObjectWithTag("GameControllerCanvas").transform);
        enemyScript = gameObject.GetComponent<AbstractEnemy>();
        BossText = UI.GetComponentInChildren<TMP_Text>();
        BossHPBar = UI.GetComponentInChildren<Slider>();
        BossHPBar.value = 0;
        UI.GetComponentInChildren<SpriteRenderer>().sprite = BossHPSprite;
        GameObject.FindGameObjectWithTag("BossNameText").GetComponent<TMP_Text>().text = BossName;
        BossText.text = "Boss Appears";
        Invoke("DeactivateBossText", 1.3f);

    }

    void Update()
    {
        float curSliderValue = Mathf.SmoothDamp(BossHPBar.value,
            enemyScript.GetHitPoints() / enemyScript.GetMaxHitPoints(), ref curVelocity, 300 * Time.deltaTime);
        BossHPBar.value = curSliderValue;
    }
    private void DeactivateBossText()
    {
        BossText.gameObject.GetComponent<Animator>().SetTrigger("Fadeout");
    }

    private void OnDestroy()
    {
        Destroy(UI);
    }
}
