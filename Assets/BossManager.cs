using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject tumor1;
    BreakableBox breakableBox1;
    public GameObject tumor2;
    BreakableBox breakableBox2;
    


    public BreakableBox bossBreakableBox;
    public GameObject EndDoor;
    public GameObject shield;
    public GameObject damagedShield;
    int deadTumorCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        breakableBox1 = tumor1.GetComponent<BreakableBox>();
        breakableBox1.BreakEvent += () => MarkBoxes();

        breakableBox2 = tumor2.GetComponent<BreakableBox>();
        breakableBox2.BreakEvent += () => MarkBoxes();

        bossBreakableBox.BreakEvent += () => OpenWin();
    }

    void OpenWin()
    {
        
        EndDoor.SetActive(false);
    }

    void MarkBoxes()
    {
        deadTumorCount += 1;
        Debug.Log(String.Format("deadTumor count {0}", deadTumorCount));
        breakableBox1.breakTextIndex = 3;
        breakableBox2.breakTextIndex = 3;

        if (deadTumorCount == 1)
        {
            DamageShield();
        }
        if (deadTumorCount == 2)
        {
            BreakShield();
        }
    }
    void DamageShield()
    {
        shield.SetActive(false);
        damagedShield.SetActive(true);
    }
    void BreakShield()
    {
        damagedShield.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
