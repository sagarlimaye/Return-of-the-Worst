using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BossHealth : MonoBehaviour
{
    public float StartingHealth = 150;

    public float CurrentHealth;
    public Slider healthSlider;
    public Text healthSliderText;

    public Image victory;
    public Text victoryText;

    //public Animator anim;
    public int killValue;

    public static event ScoreTracker.ScoreUpdate onBossKilled;

    public bool isEnd = false;
    public string nextStage = "Stage_";

    public AudioSource newMusic;


    private Animator anim;
    private ScreenFade screenFader;

    // Use this for initialization
    void Start()
    {
        CurrentHealth = StartingHealth;
        healthSlider.maxValue = StartingHealth;
        healthSlider.value = Mathf.Clamp(CurrentHealth, 0, StartingHealth);
        healthSliderText.text = Mathf.Clamp(CurrentHealth, 0, StartingHealth).ToString();
        anim = GetComponentInChildren<Animator>();
        anim.SetInteger("Health", (int)CurrentHealth);
        screenFader = GameObject.Find("ScreenFading").GetComponent<ScreenFade>();
    }

    // Update is called once per frame
    void Update()
    {
        //changeHealth(-0.3f);
        
    }

    public void changeHealth(float diff) //negative is damage, positive is healing
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth += diff;
            healthSlider.value = Mathf.Clamp(CurrentHealth, 0, StartingHealth);
            healthSliderText.text = Mathf.Clamp(CurrentHealth, 0, StartingHealth).ToString();
            if (diff < 0)
                anim.SetTrigger("Pain");
            anim.SetInteger("Health", (int)CurrentHealth);
            //Debug.Log("health is now " + CurrentHealth);
        }

        if (CurrentHealth <= 0)
        {
            //Player Died, do something
            Debug.Log("Boss Has Died");

            if (onBossKilled != null)
                onBossKilled();

            //Time.timeScale = 0;
        }

        //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        /*if (diff < 0 && CurrentHealth > 0)
        {
            anim.SetTrigger("pain");
        }
        else if (diff < 0 && CurrentHealth <= 0 && !stateInfo.IsName("Death"))
        {
            anim.SetTrigger("die");
        }*/
    }
    public void advanceStage()
    {
        if (isEnd)
        {
            victory.enabled = true;
            victoryText.enabled = true;
            AudioSource[] allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
            if (newMusic != null) newMusic.Play();
        }
        else
        {
            var bosses = GameObject.FindGameObjectsWithTag("Boss");
            if (bosses.Length == 1)
                StartCoroutine("ChangeLevel");
        }
        Destroy(transform.parent.gameObject, 3);
    }
    IEnumerator ChangeLevel()
    {
        float fadeTime = screenFader.BeginFade(1);
        yield return  new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(nextStage);
    }
}
