using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    private Color currColor;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private Material defaultMaterial;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    public GameObject xpDrop;
    public GameObject DeathParticles;
    public Color spriteColor;
    private Coroutine flashRoutine;
    //AIPathing aiPath;

    public float ms;
    public float maxHP;
    public float hitpoints;
    public float attackRange;
    public float attackSpeed;
    protected float attackTimer;

    protected float playerDistance;
    protected Vector3 playerVector;
    protected bool attacking = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        maxHP = hitpoints;
        ResetColor();
    }
    public void UpdateAbstract()
    {
        playerVector = GameObject.Find("Player").transform.position - transform.position;
        MoveToTarget();
        
        if (hitpoints <= 0)
        {
            Death();
        }
    }

    public float CalculatePlayerDistance()
    {
        playerDistance = Mathf.Sqrt(Mathf.Pow(playerVector.x, 2) + Mathf.Pow(playerVector.y, 2));
        return playerDistance;
    }
    public float GetHitPoints()
    {
        return hitpoints;
    }
    public float GetMaxHitPoints()
    {
        return maxHP;
    }

    public virtual void MoveToTarget()
    {
        Vector2 moveDir = playerVector.normalized * ms * Time.deltaTime;
        transform.position += new Vector3(moveDir.x, moveDir.y, 0);

        sr.flipX = moveDir.x > 0;
    }

    private IEnumerator FlashRed()
    {
        sr.material = flashMaterial;
        yield return new WaitForSeconds(0.3f);
        sr.material = defaultMaterial;
        flashRoutine = null;
    }
    public void ResetColor()
    {
        // null reference exception
       // sr.material = defaultMaterial;
    }

    public virtual void TakeDamage(float damage)
    { //method invoked by bullets and sort
        hitpoints -= damage;
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRed());     
    }

    public virtual void Death()
    {
        // add drop chance
        Instantiate(xpDrop, transform.position, transform.rotation);
        GameObject dp = Instantiate(DeathParticles, transform.position, transform.rotation);
        ResetColor();
        var main = dp.GetComponent<ParticleSystem>().main;
        main.startColor = spriteColor;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //if (coll.gameObject.tag != "Enemy")print("Collided with: " + coll.gameObject.tag);
        if (coll.gameObject.tag == "Player")
        { //when hitting player, already handled in player
            coll.gameObject.GetComponent<Player>().TakeDamage();
        } else if (coll.gameObject.tag == "Tile")
        { //when hitting wall

        } else if (coll.gameObject.tag == "Projectile")
        {
            //already handled in the projectile itself.
        }
    }
}
