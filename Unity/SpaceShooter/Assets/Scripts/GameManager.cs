using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager defaultGM;

	public float scrollSpeed = -2f;

	public GameObject enemyTemplate;
	private int numEnemies = 0;
	public float enemySpeed;

	private Text scoreText;
	private int scoreNum = 0;

	private float ROUND_TIME = 30;
	private float time = 0;
	private Text timerText;
	private float seconds = 0;

	private GameObject pauseMenu;

	public bool isGameOver = false;

	void Awake () {
		defaultGM = this;
		scoreText = GameObject.Find("Canvas").transform.Find("Score").GetComponent<Text>();
		timerText = GameObject.Find("Canvas").transform.Find("Timer").GetComponent<Text>();
		pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
		pauseMenu.gameObject.SetActive(false);
	}

	void Update () {
		if(time < ROUND_TIME && isGameOver == false) {
			time += Time.deltaTime;
			seconds = (int)(time % 60f);
			timerText.text = "Time: " + seconds.ToString("00");

			if(numEnemies < 4) {
				Invoke("InstantiateEnemy", 2);

				numEnemies++;
			}
		}
		else {
			GameOver(true);
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

	public void PlayerMissedEnemy() {
		scoreNum -= 1;
		scoreText.text = "Score: " + scoreNum;
	}

	public void DidHitEnemy() {
		scoreNum += 20;
		scoreText.text = "Score: " + scoreNum;
	}

	public void EnemyHitHomeBase() {
		scoreNum -= 10;
		scoreText.text = "Score: " + scoreNum;
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
}
