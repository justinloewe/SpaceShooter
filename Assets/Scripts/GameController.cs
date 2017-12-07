using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] spawns;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float waveBreak;
	public float waveNumber;

	public GameObject playerExplosion;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText GameOverText;
	public GUIText timeText;
	public GUIText shotsText;

	public bool gameOver;
	public bool restart;
	private int score;
	private int time;
	public int shots;
	public int timeValue;
	public int shotValue;

	public void function()
	{
		Debug.Log ("Funktion aufgerufen!" + shots);
	}

	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		GameOverText.text = "";
		score = 0;
		time = 20;
		shots = 10;
		UpdateScore ();
		UpdateTime ();
		UpdateShots ();
		StartCoroutine (SpawnWaves ());
		StartCoroutine (TimeRun ());
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

	//Konstantes Zeitvergehen in Realsekunden
	IEnumerator TimeRun ()
	{
		while (time > 0)
		{
			yield return new WaitForSecondsRealtime (1);
		    AddTime (timeValue);
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{	
			for (int i = 0; i < hazardCount; i++)
		    {
                int toSpawn = Mathf.RoundToInt(UnityEngine.Random.Range(0, spawns.Length));
                Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(spawns[toSpawn], spawnPosition, spawnRotation);

                if (time == 0)
				{
					GameOver ();
					restartText.text = "Press 'R' for Restart";
					restart = true;
					break;
				}
			    yield return new WaitForSeconds (spawnWait);
		    }
			hazardCount++;
			waveNumber++;
			if (waveBreak < 5) {
				waveBreak++;
			}
			yield return new WaitForSeconds (waveWait - waveBreak);
			AddShots (shotValue);   //Hinzufügen von Shots nach jeder Welle

			if (gameOver) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
		
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void AddTime (int newTimeValue)
	{
		time += newTimeValue;
		UpdateTime ();
	}

	void UpdateTime ()
	{
		timeText.text = "Time: " + time;
	}

	public void AddShots (int value)
	{
		shots += value;
		UpdateShots ();
	}

	//Methode für das Abziehen der Shots im PlayerController
    public void RemoveShots(int value) {
        shots -= value;
        UpdateShots();
    }

    void UpdateShots ()
	{
		shotsText.text = "Shots: " + shots;
	}

	public void GameOver ()
	{
		GameOverText.text = "Game Over!";
		gameOver = true;
	}
}