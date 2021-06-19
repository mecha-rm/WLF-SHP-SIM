using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Animal
{
    // the life span as a static object.
    // this is the same for all sheep.
    private static int LIFE_SPAN = 100;

    // Start is called before the first frame update
    void Start()
    {
        species = "sheep";
    }

    // gets the life span of the sheep.
    public override int GetLifeSpan()
    {
        return LIFE_SPAN;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
