using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {
    public static PlayerAttributes instance = null;
    public enum Character { Mike, Jay, Rich };
    public Character characterName = Character.Rich;

    public int attackDamage = 1;
    public int defence = 0;
    public float speed = 1f;

    public GameObject mikeModel, jayModel, richModel;
    private Button mikeButton, jayButton, richButton;

    public bool GodMode = false, UnlimitedSuper = false;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a SettingsManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        SetCharacter(2);

        EndGame.onGameOver += DestroyThis;
        PauseMenu.onGameQuit += DestroyThis;

        var mainCanvas = GameObject.Find("MainCanvas");
        mikeButton = mainCanvas.transform.Find("CharSelectPanel/ButtonPanel/MikeButton").GetComponent<Button>();
        jayButton = mainCanvas.transform.Find("CharSelectPanel/ButtonPanel/JayButton").GetComponent<Button>();
        richButton = mainCanvas.transform.Find("CharSelectPanel/ButtonPanel/RichButton").GetComponent<Button>();
        mikeButton.onClick.AddListener(instance.SetCharacterMike);
        jayButton.onClick.AddListener(instance.SetCharacterJay);
        richButton.onClick.AddListener(instance.SetCharacterRich);
    }
    private void OnDestroy()
    {
        EndGame.onGameOver -= DestroyThis;
        PauseMenu.onGameQuit -= DestroyThis;

        mikeButton.onClick.RemoveListener(instance.SetCharacterMike);
        jayButton.onClick.RemoveListener(instance.SetCharacterJay);
        richButton.onClick.RemoveListener(instance.SetCharacterRich);
    }
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
    public void SetAttackDamage(int attack_damage)
    {
        attackDamage = attack_damage;
    }
    public void SetCharacterMike()
    {
        SetCharacter(0);
    }
    public void SetCharacterJay()
    {
        SetCharacter(1);
    }
    public void SetCharacterRich()
    {
        SetCharacter(2);
    }
    public void SetCharacter(int character)
    {
        switch(character)
        {
            case 0:
                characterName = Character.Mike;
                attackDamage = 15;
                speed = 1.8f;
                defence = 10;
                break;
            case 1:
                characterName = Character.Jay;
                attackDamage = 12;
                speed = 4.2f;
                defence = 8;
                break;
            case 2:
                characterName = Character.Rich;
                attackDamage = 9;
                speed = 3f;
                defence = 6;
                break;
        }
    }
}
