using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour {

    public void StartNewGame()
    {
        SceneManager.LoadScene("Stage_1");
    }
}
