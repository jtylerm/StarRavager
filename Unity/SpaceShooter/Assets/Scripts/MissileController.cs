using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour {

	public GameObject explosionTemplate;

	public PlayerController player;


	void Awake() {
		player = GameObject.Find("Player").gameObject.GetComponent<PlayerController>();
	}

	//destroy player missiles that go off screen
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("KillZone")) {
			GameManager.defaultGM.PlayerMissedEnemy();
			Destroy(this.gameObject);
		}
	}

	//detect enemy or player collision with missiles
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			//create explosion
			GameObject explosion = Instantiate(explosionTemplate, coll.transform.position, Quaternion.identity);

			//destroy enemy and explosion
			Destroy(this.gameObject);
			Destroy(coll.gameObject);
			Destroy(explosion, .33f);

			//count enemy hit for score, then decrement number of enemies
			GameManager.defaultGM.DidHitEnemy();
			GameManager.defaultGM.DecrementEnemyCount();
		}

		if (coll.gameObject.tag == "Player") {
			Destroy(this.gameObject);

			player.PlayerWasHit();

			if(player.playerHealth < 1) {
				//create explosion
				GameObject explosion = Instantiate(explosionTemplate, coll.transform.position, Quaternion.identity);

				//destroy player and explosion
				Destroy(coll.gameObject);
				Destroy(explosion, .33f);
			}
		}
	}
}
