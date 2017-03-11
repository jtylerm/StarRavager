using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager defaultGM;

	public PlayerController player;

	public float scrollSpeed = -2f;

	public GameObject enemyTemplate;
	private int numEnemies = 0;
	public float enemySpeed;

	public float ammoPickupFlyingSpeed;
	public GameObject ammoPickupTemplate;

	private Text scoreText;
	private int scoreNum = 0;

	private float ROUND_TIME = 30;
	private float time = 0;
	private Text timerText;
	private float seconds = 0;

	private GameObject pauseMenu;

	private Text ammoCountText;

	public bool isGameOver = false;

	public List<GameObject> healthBars = new List<GameObject>();


	void Awake () {
		defaultGM = this;

		ammoCountText = GameObject.Find("Canvas").transform.Find("AmmoCount").GetComponent<Text>();
		scoreText = GameObject.Find("Canvas").transform.Find("Score").GetComponent<Text>();
		timerText = GameObject.Find("Canvas").transform.Find("Timer").GetComponent<Text>();

		pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
		pauseMenu.gameObject.SetActive(false);
	}

	void Start() {
		float spawnTime = Random.Range(5f, 10f);
		float sequencialSpawnTime = Random.Range(5f, 10f);
		InvokeRepeating("SpawnAmmoPickup", spawnTime, sequencialSpawnTime);
	}

	void Update () {
		if(time < ROUND_TIME && isGameOver == false) {
			//update the timer
			time += Time.deltaTime;
			seconds = (int)(time % 60f);
			timerText.text = "Time: " + seconds.ToString("00");

			//update the ammo count UI
			ammoCountText.text = ("x " + player.ammoCount);

			//update the score UI
			scoreText.text = "Score: " + scoreNum;

			//instantiate enemy when # of enemies is below 4
			if(numEnemies < 4) {
				Invoke("InstantiateEnemy", 2);

				numEnemies++;
			}
		}
		else {
			GameOver(true);
		}

		if(isGameOver == true) {
			pauseMenu.gameObject.SetActive(true);
		}
	}

	void InstantiateEnemy() {
		//spawn enemy
		Vector3 startPosition = new Vector3(Random.Range(-2.3f,2.31f), 4 , 0);
		GameObject enemy = Instantiate(enemyTemplate, startPosition, Quaternion.identity);

		//send enemy down the screen as long as current round is not over
		if(isGameOver == false) {
			enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, enemySpeed));
		}
	}

	void SpawnAmmoPickup () {
		//use random horizontal position to spawn ammo pickup
		Vector3 startPosition = new Vector3(Random.Range(-2.5f, 2.6f), 4.5f, 0);
		GameObject ammoPickup = Instantiate(ammoPickupTemplate.gameObject, startPosition, Quaternion.identity);

		//spawn ammo pickup
		if(isGameOver == false) {
			ammoPickup.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, ammoPickupFlyingSpeed));
		}
	}

	public void PlayerMissedEnemy() {
		player.ammoCount -= 1;
		scoreNum -= 1;
	}

	public void DidHitEnemy() {
		player.ammoCount -= 1;
		scoreNum += 20;
	}

	public void EnemyHitHomeBase() {
		scoreNum -= 10;
	}

	public void DecrementEnemyCount() {
		numEnemies--;
	}

	public void RestartGame() {
		SceneManager.LoadScene("Main");
	}

	public void GameOver(bool gameOver) {
		isGameOver = gameOver;
	}

	public void UpdateHealthUI() {
		for(int i = 0; i < healthBars.Count; i++) {
			healthBars[i].SetActive(false);
		}

		for(int i = 0; i < player.playerHealth - 1; i++) {
			healthBars[i].SetActive(true);
		}
	}
}
