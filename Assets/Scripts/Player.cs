using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Camera camera;
    public RectTransform canvasRect;
    public GameObject SpotlightObj;
    public GameObject WeaponBox;
    private Light2D Spotlight;

    private GameObject healthContainer;
    private float ms;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private bool invincible;
    private AudioSource auS;
    private BoxCollider2D boxColl;

    public GameObject XpBar; // slider
    public TMP_Text levelText;
    private int level = 1;
    private int curXP;
    private int reqXP;
    private float curVelocity = 0;

    private int health;
    private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = 6;
        maxHealth = 6;
        ms = 4;
        reqXP = 5; // (first lvl);
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        boxColl = gameObject.GetComponent<BoxCollider2D>();
        UpdateUI();
        healthContainer = GameObject.Find("HealthContainer");
        auS = gameObject.GetComponent<AudioSource>();
        Spotlight = SpotlightObj.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            Movement();
        }

        anim.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("moveY", Input.GetAxisRaw("Vertical"));

        // this just reducing it to 10.5 aprox
        //Spotlight.pointLightOuterRadius = Mathf.SmoothDamp(Spotlight.pointLightOuterRadius, Random.Range(10f, 11), ref curVelocity, 100 * Time.deltaTime);

        // smooth xp bar
        float curSliderValue = Mathf.SmoothDamp(XpBar.GetComponent<Slider>().value, (float)curXP / (float)reqXP, ref curVelocity, 100 * Time.deltaTime);
        XpBar.GetComponent<Slider>().value = curSliderValue;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "EnemyProjectile")
        {
            //color change for clarity
        }
    }

    public void TakeDamage()
    {
        if (!invincible)
        {
            health--;
            if (health <= 0)
            { //play death animation
                anim.SetBool("death", true);
                Destroy(rb);
                WeaponBox.SetActive(false);
            }
            Invoke("EndInvincibilityFrames", 0.5f);
            invincible = true;

            Invoke("FlashColor", 0.0f);
            Invoke("ResetColor", 0.1f);
            Invoke("FlashColor", 0.2f);
            Invoke("ResetColor", 0.3f);
            Invoke("FlashColor", 0.4f);
            Invoke("ResetColor", 0.5f);

            Invoke("ResetHitbox", 0.5f); //re enable hitbox to take damage again if you stay in a collider

            //auS.Play();

        }
        if (health >= 0)
        {
            UpdateUI();
        }
    }

    void ResetHitbox()
    {
        boxColl.enabled = false;
        boxColl.enabled = true;
    }

    void FlashColor()
    {
        sr.color = new Color32(220, 220, 220, 180);

    }
    void ResetColor()
    {
        sr.color = Color.white;
    }


    private void EndInvincibilityFrames()
    {
        invincible = false; //ends frames of invincibility after being hit.
    }

    public void UpdateUI() //call this when taking damage or healing
    {
        int lostHealth = maxHealth - health;
        for (int i = 0; lostHealth > i; i++)
        {
            healthContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    // returns level to weapons
    public int getLevel()
    {
        return level;
    }

    public void GainXP(int xp)
    {
        curXP += xp;
        
        if (curXP >= reqXP)
        {
            curXP -= reqXP; // carry over xp
            LevelUp();
        }
    }
    private void LevelUp() // vampsurvivors uses 10, 13, 16 reqXP increments
    {
        if (level < 20)
        {
            reqXP = reqXP + 10;
        }
        else if (level < 40)
        {
            reqXP = reqXP + 13;
        }
        else if (level < 100)
        {
            reqXP = reqXP + 16;
        }
        level++;
        // UI changes:
        //levelUpScreen

        levelText.text = level.ToString();
    }

    private void Movement()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        
        // Move character independendant of frame rate and without physics. Also ignore deadzone
        if (Mathf.Abs( Input.GetAxisRaw("Horizontal") ) > 0.5f || Mathf.Abs(Input.GetAxisRaw("Vertical") ) > 0.5f )
        {
            gameObject.transform.position += move * Time.deltaTime * ms;

            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        sr.flipX = Input.GetAxisRaw("Horizontal") > 0.5f;
    }


}
