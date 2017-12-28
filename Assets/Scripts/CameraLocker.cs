using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocker : MonoBehaviour
{
    public GameObject enemy_with_sword;
    public GameObject easyEnemy;
    public int numEasy = 0;
    public GameObject medEnemy;
    public int numMed = 0;
    public GameObject hardEnemy;
    public int numHard = 0;

    private GameObject[] enemies;
    private GameObject player;
    private bool triggered = false;
    private bool shouldCheck = false;
    public bool completed = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("col with" + col.gameObject.name);
        if (col.tag == "Player" && !triggered && !completed)
        {
            triggered = true;
            shouldCheck = true;
            player = col.gameObject;

            /*enemies = new GameObject[5];
            for (int i = 0; i < 5; i ++)
            {
                Vector2 spawnLocation = this.transform.position;// + Random.Range(-5f, 5f);
                float randx = Random.Range(-2f, 2f);
                float randy = Random.Range(-5f, -3f);
                Debug.Log(spawnLocation + " " + randx + " " + randy);

                if (randx < 0)
                {
                    randx -= 5f;
                }
                else if (randx >= 0)
                {
                    randx += 5f;
                }

                //if (Random.Range(1f, 10) >= 5) randx *= -1;
                //if (Random.Range(1f, 10) >= 5) randy *= -1;

                spawnLocation.x += randx;
                spawnLocation.y += randy;
                enemies[i] = Instantiate(enemy_with_sword, spawnLocation, Quaternion.identity);
            }*/

            enemies = new GameObject[numEasy + numMed + numHard+1];
            int current = 0;
            for (int i = 0; i < numEasy; i++)
            {
                Vector2 spawnLocation = this.transform.position;// + Random.Range(-5f, 5f);
                float randx = Random.Range(-2f, 2f);
                float randy = Random.Range(-5f, -3f);
                Debug.Log(spawnLocation + " " + randx + " " + randy);

                if (randx < 0)
                {
                    randx -= 5f;
                }
                else if (randx >= 0)
                {
                    randx += 5f;
                }

                //if (Random.Range(1f, 10) >= 5) randx *= -1;
                //if (Random.Range(1f, 10) >= 5) randy *= -1;

                spawnLocation.x += randx;
                spawnLocation.y += randy;
                current++;
                enemies[current] = Instantiate(easyEnemy, spawnLocation, Quaternion.identity);
            }

            for (int i = 0; i < numMed; i++)
            {
                Vector2 spawnLocation = this.transform.position;// + Random.Range(-5f, 5f);
                float randx = Random.Range(-2f, 2f);
                float randy = Random.Range(-5f, -3f);
                Debug.Log(spawnLocation + " " + randx + " " + randy);

                if (randx < 0)
                {
                    randx -= 5f;
                }
                else if (randx >= 0)
                {
                    randx += 5f;
                }

                //if (Random.Range(1f, 10) >= 5) randx *= -1;
                //if (Random.Range(1f, 10) >= 5) randy *= -1;

                spawnLocation.x += randx;
                spawnLocation.y += randy;
                current++;

                enemies[current] = Instantiate(medEnemy, spawnLocation, Quaternion.identity);
            }

            for (int i = 0; i < numHard; i++)
            {
                Vector2 spawnLocation = this.transform.position;// + Random.Range(-5f, 5f);
                float randx = Random.Range(-2f, 2f);
                float randy = Random.Range(-5f, -3f);
                Debug.Log(spawnLocation + " " + randx + " " + randy);

                if (randx < 0)
                {
                    randx -= 5f;
                }
                else if (randx >= 0)
                {
                    randx += 5f;
                }

                //if (Random.Range(1f, 10) >= 5) randx *= -1;
                //if (Random.Range(1f, 10) >= 5) randy *= -1;

                spawnLocation.x += randx;
                spawnLocation.y += randy;
                current++;

                enemies[current] = Instantiate(hardEnemy, spawnLocation, Quaternion.identity);
            }



            //Destroy(col.gameObject);
            //Debug.Log("Locking Camera");
        }
    }
    private void Update()
    {
        if (triggered && shouldCheck && !completed)
        {
            bool isDone = true;
            for (int i = 0; i < numEasy + numMed + numHard; i++)
            {
                if (enemies[i] != null)
                {
                    isDone = false;
                    //Debug.Log("not done");
                }
            }
            if (isDone)
            {
                player.GetComponent<CameraLockController>().cameraEventDone();
                completed = true;
                Destroy(this.gameObject);
                //Debug.Log("DONE");
                shouldCheck = false;
            }
        }
        
    }
}
