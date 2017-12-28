using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float StartingHealth = 100;

    public float CurrentHealth;
    public Slider healthSlider;
    public Text healthSliderText;
    public GameObject gameOverPanel;

    private int defence = 0;
    public Animator anim;

    public AudioSource deathMusic;
    private AudioSource deathSound;


    // Use this for initialization
    void Start ()
    {
        GameObject pa = GameObject.Find("PlayerAttributes");
        if (pa != null)
        {
            PlayerAttributes pas = pa.GetComponent<PlayerAttributes>();
            if (pas != null)
            {
                defence = pas.defence;
            }
        }

        CurrentHealth = StartingHealth;

        healthSlider.value = Mathf.Clamp(CurrentHealth, 0, 100);
        healthSliderText.text = Mathf.Clamp(CurrentHealth, 0, 100).ToString();

        Debug.Log("Looking");
        

        
    }

    // Update is called once per frame
    void Update ()
    {
        //changeHealth(-0.3f);
        
	}

    public void changeHealth(float diff) //negative is damage, positive is healing
    {
        Debug.Log("Called ChangeHealth");
        if (diff < 0)// if diff is negative, damage is taken
        {
            diff = Mathf.Clamp(diff + defence, -999999, 0);// since diff is negative, subtracting from a negative means to add. clamp makes sure defence doesnt heal
        }
        //Debug.Log("health diff = " + diff + "current health = " + CurrentHealth);
        if (CurrentHealth > 0 && !PlayerAttributes.instance.GodMode)
        {
            
            //CurrentHealth += diff;
            CurrentHealth = Mathf.Clamp(CurrentHealth + diff, 0f, 100f);
            healthSlider.value = Mathf.Clamp(CurrentHealth, 0f, 100f);
            healthSliderText.text = " " + Mathf.Clamp(CurrentHealth, 0f, 100f).ToString();
            Debug.Log(":" + healthSliderText.text + ":");

            //Debug.Log("health is now " + CurrentHealth);
            anim.SetInteger("Health", (int)CurrentHealth);
            
        }
        else
        {
            if (deathSound == null)  deathSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
            AudioSource[] allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
            if (deathSound != null) deathSound.Play();
            if (deathMusic != null) deathMusic.Play();
        }
        //Debug.Log("health diff = " + diff + "current hefalth = " + CurrentHealth);

        //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (diff < 0 && CurrentHealth > 0)
        {
            anim.SetTrigger("Pain");
        }
    //    else if (diff < 0 && CurrentHealth <= 0 && !stateInfo.IsName("Death"))
    //    {
    //        anim.SetTrigger("die");
    //    }
    }
}
