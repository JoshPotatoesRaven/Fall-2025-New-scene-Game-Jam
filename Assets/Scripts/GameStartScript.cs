using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas gameStartCanvas;
    void Start()
    {
        Button startButton = gameStartCanvas.transform.Find("PlayButton").GetComponent<Button>();
        startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level 1");
        });
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
