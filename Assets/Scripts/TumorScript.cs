using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumorScript : MonoBehaviour
{

    BreakableBox breakableBox;

    public TumorScript otherTumor;
    // Start is called before the first frame update
    void Start()
    {
        breakableBox = GetComponent<BreakableBox>();
        breakableBox.BreakEvent += () => MarkBox();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MarkBox()
    {
        
    }
}
