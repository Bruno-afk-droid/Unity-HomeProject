using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : touchable
{
    public InputController Controller;
    public HealthBarManager HealthBar;

    public override void GetHit(HitEffect hitEffect)
    {
        base.GetHit(hitEffect);
        HealthBar.TakeDamage(hitEffect.Damage);
    }
}
