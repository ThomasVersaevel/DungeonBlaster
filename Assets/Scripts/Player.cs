using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Camera camera;
    public RectTransform canvasRect;

    private GameObject healthContainer;
    private float ms;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private bool invincible;
    private AudioSource auS;
    private BoxCollider2D box;

    public GameObject XpBar; // starts at y: -5.81 and goes up to 0
    private float defaultXpBarPos = -5.81f;
    private float speed = 5.81f; // xp bar move speed
    public TMP_Text levelText;
    private int level = 1;
    private int curXP;
    private int reqXP;

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
        box = gameObject.GetComponent<BoxCollider2D>();
        UpdateUI();
        healthContainer = GameObject.Find("HealthContainer");
        auS = gameObject.GetComponent<AudioSource>();
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

            auS.Play();
        }
        UpdateUI();
    }

    void ResetHitbox()
    {
        box.enabled = false;
        box.enabled = true;
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
        //print("hello");
        for (int i = 0; maxHealth - health > i; i++)
        {
            healthContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void GainXP(int xp)
    {
        curXP += xp;
        //print(defaultXpBarPos * ((float)curXP / (float)reqXP));
        // bar has to move in positive y direction from -5.81 to 0. 

        //XpBar.transform.position = new Vector3(0, defaultXpBarPos * ((float)curXP / (float)reqXP), 0);
        Vector3 canvasSpacePos;// = XpBar.transform.position;

        //RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, 
        //    Vector3.MoveTowards(XpBar.transform.position, new Vector3(0, defaultXpBarPos * ((float)curXP / (float)reqXP), 0), speed), camera, 
        //    out canvasSpacePos);

        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect,
            new Vector3(0, ((float)curXP / (float)reqXP), 0), camera,
            out canvasSpacePos);

        XpBar.transform.position += canvasSpacePos;
        if (curXP > reqXP)
        {
            curXP -= reqXP; // carry over xp
            XpBar.transform.position = new Vector3(0, defaultXpBarPos, 0) + transform.position;
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
        levelText.text = level.ToString();

    }

    private void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        { //x
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * ms, rb.velocity.y / 1.5f);
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {//y
            rb.velocity = new Vector2(rb.velocity.x / 1.5f, Input.GetAxisRaw("Vertical") * ms);
        }
        if (Input.GetAxisRaw("Horizontal") <= 0.5f && Input.GetAxisRaw("Horizontal") >= -0.5f)
        { //x standstill
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") <= 0.5f && Input.GetAxisRaw("Vertical") >= -0.5f)
        {//y standstill
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        if (Input.GetAxisRaw("Horizontal") > 0.5f)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        if (rb.velocity.Equals(Vector3.zero))
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }
    }


}
