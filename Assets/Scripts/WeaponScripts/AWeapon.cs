using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    protected SpriteRenderer sr;
    private float angle;
    protected Vector3 mousePos;
    public GameObject projectile;
    public float attackSpeed;
    public float projectileSpeed;
    public float attackTimer;
    public float damage;
    protected AudioSource auS;
    public int itemLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = 0;
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void UpdateRotation()
    {
        mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos -= objectPos;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
       
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        sr.flipX = angle > 0;
    }
    protected virtual void UpdateRotation(float offsetAngle)
    {
        mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos -= objectPos;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - offsetAngle));
        sr.flipX = angle > 0 + offsetAngle;
    }
    public virtual void Shoot(Vector3 mPos)
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
        proj.GetComponent<Rigidbody2D>().velocity = mPos.normalized * projectileSpeed;
        proj.GetComponent<AProjectile>().damage = damage;
        //auS.Play();
    }
}
