 using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchable : KinematicObject
{
    public Vector2 position { get { return this.transform.position; } }
    public MeleeHitBox MeleeHitBox;
    public Collider2D BodyHurtBox;

    public List<Collider2D> TotalHurtbox { get {List<Collider2D> r= new List<Collider2D>(MeleeHitBox.HitBoxes); r.Add(BodyHurtBox);return r; } }
           
    public virtual void Awake()
    {
        MeleeHitBox = this.GetComponentInChildren<MeleeHitBox>();
        BodyHurtBox = this.GetComponent<Collider2D>();
    }

    public virtual void GetHit(HitEffect hitEffect)
    {
        Debug.Log(this.name + " get hit");
    }
}
