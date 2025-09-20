using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    public float maxHealth;

    public float currentHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
