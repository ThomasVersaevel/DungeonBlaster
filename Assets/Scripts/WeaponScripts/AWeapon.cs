using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    public SpriteRenderer sr;
    private float angle;
    public GameObject projectile;
    public float attackSpeed;
    public float projectileSpeed;
    public float attackTimer;
    public float damage;
    protected AudioSource auS;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = 0;
    }

    // Update is called once per frame
    public void UpdateRotation()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
       
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (angle < 0)
        {
            sr.flipX = false;
        } else
        {
            sr.flipX = true;
        }

        if (Input.GetMouseButtonDown(0) && attackTimer < 0)
        {
            attackTimer = attackSpeed;
            Shoot(mousePos);
        } else
        {
            attackTimer -= Time.deltaTime;
        }
    }
    public void UpdateMeleeRotation(float offsetAngle)
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90+offsetAngle));
        if (angle < 90)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
        if (Input.GetMouseButtonDown(0) && attackTimer < 0)
        {
            attackTimer = attackSpeed;
            Shoot(mousePos);
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }
    public virtual void Shoot(Vector3 mousePos)
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, angle-90))) as GameObject;
        proj.GetComponent<Rigidbody2D>().velocity = mousePos.normalized * projectileSpeed;
        proj.GetComponent<AProjectile>().damage = damage;
        //auS.Play();
    }
}
