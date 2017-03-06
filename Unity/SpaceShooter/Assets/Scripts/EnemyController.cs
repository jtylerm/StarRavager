using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject missileTemplate;
	public float missileSpeed;


	void Start() {
		float firstShot = Random.Range(0.5f, 1.5f);
		float sequencialShots = Random.Range(1f, 2.5f);
		InvokeRepeating("FireMissile", firstShot, sequencialShots);
	}

	public void FireMissile() {
		//use enemy's position plus some extra space in front of their ship to Instantiate missiles
		Vector3 startPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		Quaternion startRotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, -180, 0);
		GameObject missile = Instantiate(missileTemplate, startPosition, startRotation);

		missile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, missileSpeed));
	}

	void OnTriggerEnter2D(Collider2D other) {
		//if enemy hits home base, decrement player's score
		if(other.gameObject.CompareTag("HomeBase")) {

			Destroy(this.gameObject);
			GameManager.defaultGM.DecrementEnemyCount();

			//only decrement score while round is active
			if(GameManager.defaultGM.isGameOver == false) {
				GameManager.defaultGM.EnemyHitHomeBase();
			}
		}
	}
}