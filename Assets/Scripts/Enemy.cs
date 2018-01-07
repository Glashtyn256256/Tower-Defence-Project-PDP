﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour {


    public float speed = 10f;

    private Transform target;

	public int currencyvalue = 10;
	public float startHealth = 100f;
    public int damageToWall = 1;
	private float health;
    private int pointIndex = 0;

	[Header("Unity Specific")]
	public Image healthBar;

    void Start()
    {
        target = Waypoint.points[0];
		health = startHealth;
    }

    void Update()
    {   
        if(PlayerStats.playerHealth <= 0)
        {
            Die();
        }
        // Target Position - the Enemy's position gives relative position.
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(target.position, transform.position) <= 0.2f)
        {
            FindNextWaypoint();
        }
    }

	public void TakeDamage (float amount)
	{
		health -= amount;

		healthBar.fillAmount = health / startHealth;

		if (health <= 0) 
		{
			PlayerStats.Currency += currencyvalue;
			Die();
		}
	}

   public void DamageWall()
    {
        PlayerStats.playerHealth -= damageToWall;
    }

	void Die()
	{
        Wave_Spawner.enemiesAlive--;
		Destroy (gameObject);
	}

    void FindNextWaypoint()
    {
        if (pointIndex >= (Waypoint.points.Length - 1))
        {
            //Destroy(gameObject);
            DamageWall();
            Die();
            return;
        }

        pointIndex++;
        target = Waypoint.points[pointIndex];
    }
}
