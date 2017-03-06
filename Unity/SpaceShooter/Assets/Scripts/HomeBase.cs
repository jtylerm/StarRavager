using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("EnemyMissile")) {
			Destroy(other.gameObject);
		}
	}
}
