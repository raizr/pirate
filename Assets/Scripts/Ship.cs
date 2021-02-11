using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {

	public enum ShipState {
		Idle,
		Attack,
		Stun,
		Sinked
	}


	public Slider healthBar;

	public float health = 100.0f;
	private float currentHealth;
	public float damage = 1;
	public int accuracy = 75;
	public float time = 60.0f;
	private ShipState currentState = ShipState.Idle;
	private ShipState previousState = ShipState.Idle;

	void Awake() {
		healthBar.minValue = 0.0f;
		healthBar.maxValue = health;
		currentHealth = health;
	}

	public void StartGame() {
		currentState = ShipState.Attack;
		InvokeRepeating("DoDamage", 0, time);
	}

	public void StopGame() {
		currentState = ShipState.Sinked;
		CancelInvoke("DoDamage");
	}
	public void SetStun(float stunTime) {
		StartCoroutine(StunTime(stunTime));
	}

	IEnumerator StunTime(float stunTime) {
		previousState = currentState;
		currentState = ShipState.Stun;
		yield return new WaitForSeconds(stunTime);
		currentState = previousState;
	}

	public void DoDamage(float fixedDamage) {
		if (currentState == ShipState.Stun) {
			return;
		}
		currentHealth -= fixedDamage;
	}

	void DoDamage() {
		if (currentState == ShipState.Stun) {
			return;
		}
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
