using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Animal
{
    // the life span as a static object.
    // this is the same for all wolves.
    private static int LIFE_SPAN = 100;

    // Start is called before the first frame update
    void Start()
    {
        species = "wolf";
    }

    // gets the life span of the sheep.
    public override int GetLifeSpan()
    {
        return LIFE_SPAN;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}
