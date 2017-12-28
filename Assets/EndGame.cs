using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public GameObject gameOverPanel;
    private Animator anim;
    private Text scoreValue;
    private Text scoreText;
    // Use this for initialization

    public delegate void GameEnded();
    public static GameEnded onGameOver;

	void Start () {
        anim = GetComponent<Animator>();
        gameOverPanel = GameObject.Find("MainCanvas").transform.Find("Gameover2").gameObject;
        scoreText = gameOverPanel.transform.Find("GameOverPanel/ScoreText").GetComponent<Text>();
        scoreValue = GameObject.Find("ScoreValueText").GetComponent<Text>();
	}

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        if (onGameOver != null)
            onGameOver();
        Time.timeScale = 0;
        scoreText.text = scoreText.text + " " + scoreValue.text;
    }

}
