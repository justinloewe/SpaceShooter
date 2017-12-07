using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestryByBoundary : MonoBehaviour
{
	public GameObject playerExplosion;
	private GameController gameController;

	public GUIText GameOverText;
	public GUIText restartText;

	public bool gameOver;
	public bool restart;

	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		GameOverText.text = "";
	}

	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		//Zerstörung des Players bei Berühren der Boundary
		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			GameOver ();
		}
		Destroy (other.gameObject);
		if (gameOver) {
			restartText.text = "Press 'R' for Restart";
			restart = true;
		}
	}

	public void GameOver ()
	{
		GameOverText.text = "Game Over!";
		gameOver = true;
	}
}
