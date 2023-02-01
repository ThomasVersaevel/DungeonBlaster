using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dummy : AbstractEnemy
{
    private float OneSecDelay = 1;
    private float timer = 0;
    public TMP_Text dpsText;
    public float dps = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        ms = 0f;
        hitpoints = 10000;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Dummy is to test weapons and damage
        // Needs hp bar? or at least dps text

        if (timer < 0)
        {
            UpdateDpsText();
            timer = OneSecDelay;
        }

        timer -= Time.deltaTime;

        UpdateAbstract();
    }

    private void UpdateDpsText()
    {
        dpsText.text = dps.ToString();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        dps += damage;
        UpdateDpsText();
    }

    public override void MoveToTarget()
    {
        
    }
}
