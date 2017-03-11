using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

	private PlayerController player;

	void Awake() {
		player = GameObject.Find("Player").gameObject.GetComponent<PlayerController>();
	}
		
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Player")) {
			player.PickedUpAmmo();
			Destroy(this.gameObject);
		}
	}
}
