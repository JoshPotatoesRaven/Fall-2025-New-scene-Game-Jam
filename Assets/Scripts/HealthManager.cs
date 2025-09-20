using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    public float maxHealth;

    public float currentHealth;

    public HealthBar healthBar;

    public Transform lastCheckpoint;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercent = (currentHealth / maxHealth) * 100;
        healthBar.currentHealth = healthPercent;

        currentHealth -= Time.deltaTime;
        if (currentHealth < 5)
        {
            Debug.Log("You died");
        }
    }


    void RestartLevel()
    {
        if (lastCheckpoint == null) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else {
            //Move to last checkpoint
        }
        
    }
}
