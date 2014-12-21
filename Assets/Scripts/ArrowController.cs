﻿using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	private int damage = 20;
	private int speed = 10;
	private float lifeSpan = 5f;

	private Vector3 _direction;
	public Vector3 Direction {
		set {
			_direction = value.normalized;
		}
	}

	// Use this for initialization
	void Start () {
		Object.Destroy(this.gameObject, lifeSpan);
	}

	void Update() {
		transform.Translate(_direction.x * speed * Time.deltaTime,
		                    _direction.y * speed * Time.deltaTime,
		                    _direction.z * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		//avoid any interaction with fireball and Adrian
		if (other.gameObject.tag != "Player")
		{
			//mark pillars as red and subsequently remove them
			if (other.gameObject.tag == "Enemy")
			{
				//other.gameObject.transform.renderer.material.color = Color.red;
				EnemyStatsHolder enemyStatsHolder = other.gameObject.GetComponent<EnemyStatsHolder>();
				enemyStatsHolder.modifyHealth(-damage);
				Destroy(gameObject);
			}
		}
	}

}