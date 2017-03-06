using UnityEngine;
using System.Collections;

public class RepeatingBackground : MonoBehaviour {

	private BoxCollider2D backgroundCollider;      
	private float backgroundVerticalLength;       


	private void Awake () {
		backgroundCollider = GetComponent<BoxCollider2D> ();

		//Store the size of the collider along the y axis (its length in units).
		backgroundVerticalLength = backgroundCollider.size.y;
	}
		
	private void Update() {
		//Check if the difference along the y axis between the main Camera and the position of the object this is attached to is greater than backgroundVerticalLength.
		if (transform.position.y < -backgroundVerticalLength) {
			//If true, this means this object is no longer visible and we can safely move it forward to be re-used.
			RepositionBackground ();
		}
	}

	//Moves the object this script is attached to right in order to create our looping background effect.
	private void RepositionBackground() {
		//This is how far down we will move our background object, in this case, twice its length. This will position it directly to the top of the currently visible background object.
		Vector2 backgroundOffSet = new Vector2(0, backgroundVerticalLength * 2f);

		//Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
		transform.position = (Vector2) transform.position + backgroundOffSet;
	}
}