using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStock : HealthBar
{
    public HealthBarManager Owner;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Image[] images = this.GetComponentsInChildren<Image>();
        backHealthBar = images[1];
        frontHealthBar = images[2];
        Owner = this.GetComponentInParent<HealthBarManager>();
    }

    public bool isAlive()
    {
        return health > 0;
    }
}
