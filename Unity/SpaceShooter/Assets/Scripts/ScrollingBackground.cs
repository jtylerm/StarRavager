﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	private Rigidbody2D rb2d;


	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity = new Vector2(0, GameManager.defaultGM.scrollSpeed);
	}
}