using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

    private int kills = 0, levelsComplete = 0, pickupsObtained = 0;//, score = 0;

    private Text scoreValueText;

    public delegate void ScoreUpdate();

    private void Awake()
    {
        scoreValueText = GetComponent<Text>();
    }
    void addKillScore()
    {
        kills++;
        UpdateScoreText();
    }
    void addLevelScore()
    {
        levelsComplete++;
        kills++; // because some boss enemy died
        UpdateScoreText();
    }
    void addPickupScore()
    {
        pickupsObtained++;
        UpdateScoreText();
    }
    private void OnEnable()
    {
        NPCHealth.onNPCKilled += addKillScore;
        BossHealth.onBossKilled += addLevelScore;
        PickupController.onPickupObtained += addPickupScore;
    }
    private void OnDisable()
    {
        NPCHealth.onNPCKilled -= addKillScore;
        BossHealth.onBossKilled -= addLevelScore;
        PickupController.onPickupObtained -= addPickupScore;
    }
    public void UpdateScoreText()
    { 
        scoreValueText.text = GetTotal().ToString();
    }
    public int GetTotal()
    {
        return 5*(kills+levelsComplete+pickupsObtained);
    }
    public void ResetScore()
    {
        kills = 0;
        levelsComplete = 0;
        pickupsObtained = 0;
    }
}
