using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogCheckpoint : MonoBehaviour
{
    public float seconds;
    public Text DialogText;
    private bool started;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator startDialog(Dialog[] lines)
    {
        string lineText = ""; started = true;
        foreach (Dialog dialog in lines)
        {
            if (dialog is PlayerDialog)
            {
                switch (PlayerAttributes.instance.characterName)
                {
                    case PlayerAttributes.Character.Jay:
                        lineText = "Jay: " + (dialog as PlayerDialog).jayText;
                        break;
                    case PlayerAttributes.Character.Mike:
                        lineText = "Mike: " + (dialog as PlayerDialog).mikeText;
                        break;
                    case PlayerAttributes.Character.Rich:
                        lineText = "Rich: " + (dialog as PlayerDialog).richText;
                        break;
                }
            }
            else if (dialog is BossDialog)
            {
                var line = dialog as BossDialog;
                lineText = line.name + ": " + line.text;
            }
            DialogText.text = lineText;
            yield return new WaitForSeconds(seconds);
        }
        DialogText.text = "";
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !started)
        {
            var lines = GetComponentsInChildren<Dialog>();
            StartCoroutine("startDialog", lines);
        }
    }
}
