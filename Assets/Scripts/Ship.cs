using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

	public Slider healthBar;

	public float health = 100.0f;
	private float currentHealth;
	public float damage = 1;
	public int accuracy = 75;
	public float time = 60.0f;

	void Awake() {
		healthBar.minValue = 0.0f;
		healthBar.maxValue = health;
		currentHealth = health;
	}

	public void StartGame() {
		InvokeRepeating("DoDamage", 0, time);
	}

	public void StopGame() {
		CancelInvoke("DoDamage");
	}

	void Update()
    {
        
    }

	public void DoDamage(SpinResult effectDamage) {
		currentHealth -= effectDamage.damage;
		if (currentHealth <= 0.0f) {
			StopGame();
		}
	}

	void DoDamage() {
		int value = Random.Range(0, 100);
		if (value <= accuracy) {
			currentHealth -= damage;
			if (!IsAlive()) {
				StopGame();
			} 
		}
	}

	public bool IsAlive() {
		return currentHealth > 0.0f;
	}

	void OnGUI() {
		healthBar.value = currentHealth;
	}
}
