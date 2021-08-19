using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Animal
{
    // the life span as a static object.
    // this is the same for all sheep.
    private static float LIFE_SPAN = 100.0F;

    // Start is called before the first frame update
    void Start()
    {
        species = "sheep";
        lifeSpan = LIFE_SPAN;
    }
    
    // reproduces the sheep.
    private void Reproduce()
    {

    }

    // sheep has killed something.
    public override void Kills(GameObject victim)
    {
        // throw new System.NotImplementedException();
    }

    // sheep has been killed.
    public override void OnKilled(GameObject killer)
    {
        // throw new System.NotImplementedException();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}
