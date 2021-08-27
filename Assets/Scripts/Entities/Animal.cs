using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// animal behaviour
public abstract class Animal : Living
{
    // TODO: setup means for two animals to come together and reproduce.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected abstract void Reproduce();

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}
