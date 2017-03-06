using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveX;
	public float moveSpeed;
	public float hInput = 0;

	public float missileSpeed;

	public GameObject missileTemplate;


	void Update () {
		//move player in appropriate direction, idle by default
		Move(hInput);

		//keyboard commands for when not playing mobile
		#if !UNITY_ANDROID && !UNITY_IOS 
		if(Input.GetKeyUp(KeyCode.Space)) {
			FireMissile();
		}
		#endif
	}

	void Move(float horizontalDirection) {
		//keyboard commands for when not playing mobile
		#if !UNITY_ANDROID && !UNITY_IOS 
		moveX = Input.GetAxis("Horizontal");
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * moveSpeed, 0);
		#endif

		//using touch controls
		GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalDirection * moveSpeed, 0);
	}

	public void StartMoving(float horizontalDirection) {
		//set direction for movement
		hInput = horizontalDirection;
	}

	public void FireMissile() {
		//use player's position plus some extra space in front of his ship to Instantiate missiles
		Vector3 startPosition = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
		GameObject missile = Instantiate(missileTemplate, startPosition, Quaternion.identity);

		missile.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, missileSpeed));
	}
}