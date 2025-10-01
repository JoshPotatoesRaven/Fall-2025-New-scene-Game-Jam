using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions_Script : MonoBehaviour
{
    private BreakableBox Script;
    private BreakableBox Script2;

    // Start is called before the first frame update
    void Start()
    {
        GameObject BreakableTumor2 = GameObject.Find("BreakableTumor2");
        Script2 = BreakableTumor2.GetComponent<BreakableBox>();
        GameObject BreakableTumor1 = GameObject.Find("BreakableTumor");
        Script = BreakableTumor1.GetComponent<BreakableBox>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Script.health < 1 && Script2.health < 1)
        {
            gameObject.SetActive(true);
        }
    }
}
