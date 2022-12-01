using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeHitBox : MonoBehaviour
{
    private touchable Owner;

    public bool Active;
    public List<touchable> Targets;
    public BoxCollider2D[] HitBoxes;
    public Dictionary<BoxCollider2D, HitEffect> HitboxInfo = new Dictionary<BoxCollider2D, HitEffect>();
    public HitEffect HitEffect;
    public int MultiHit;

    // Start is called before the first frame update
    public void Start()
    {
        Owner = this.GetComponentInParent<touchable>();
        HitBoxes = this.GetComponents<BoxCollider2D>();
        Targets = new List<touchable>(Owner.transform.parent.GetComponentsInChildren<touchable>());
        Targets.Remove(this.Owner);
        ResetHitBoxes();
    }


    public bool CheckMelee()
    {
        if (!Active) return false;
        bool result = false;
        foreach (touchable target in Targets)
        {
            HitEffect attack = isColliding(target.TotalHurtbox);
            if (!attack.Equals(HitEffect.Miss))
            {
                if (MultiHit < attack.MultiHit)
                {
                    MultiHit++;
                    target.GetHit(attack);
                }
            }
        }
        return result;
    }

    public HitEffect isColliding(List<Collider2D> targets)
    {
        HitEffect result = HitEffect.Miss;
        foreach(KeyValuePair<BoxCollider2D,HitEffect> attack in this.HitboxInfo)
        {
            foreach (Collider2D target in targets)
            {
                if (attack.Key.IsTouching(target))
                {
                    result.extend(attack.Value);
                }
            }
        }
        return result;
    }


    public void SetHitBoxes(BoxCollider2D[] hitboxes, HitEffect effect)
    {
        this.ResetHitBoxes();
        foreach(BoxCollider2D hitbox in hitboxes)
        {
            this.HitboxInfo.Add(hitbox, effect);
        }
    }
    public void SetHitBoxes(HitEffect[] hitEffects)
    {
        this.ResetHitBoxes();
        for(int i = 0; i < hitEffects.Length; i++)
        {
            if(i<HitBoxes.Length)
            this.HitboxInfo.Add(HitBoxes[i], hitEffects[i]);
        }
    }
    public void AddHitBox(BoxCollider2D hitbox,HitEffect hitEffect)
    {
        this.HitboxInfo.Add(hitbox, hitEffect); 
    }
    public void ResetHitBoxes()
    {
        this.MultiHit = 0;
        HitboxInfo.Clear();
    }


}
public struct HitEffect
{
    
    public float Damage;
    public float HitStun;
    public Vector2 KnockBack;
    public int MultiHit;
    public int status;

    public HitEffect(float Damage = 0, float HitStun = 0, Vector2 KnockBack = new Vector2(), int MultiHit = 0, int status = 0)
    {

        this.Damage = Damage;
        this.HitStun = HitStun;
        this.KnockBack = KnockBack;
        this.MultiHit = MultiHit;
        this.status = status;
    }
    public void extend(HitEffect hitEffect)
    {
        if (this.Damage < hitEffect.Damage) this.Damage = hitEffect.Damage;
        if (this.HitStun < hitEffect.HitStun) this.Damage = hitEffect.HitStun;

        if (this.KnockBack.x < hitEffect.KnockBack.x) this.KnockBack.x = hitEffect.KnockBack.x;
        if (this.KnockBack.y < hitEffect.KnockBack.y) this.KnockBack.y = hitEffect.KnockBack.y;

        if (this.MultiHit < hitEffect.MultiHit) this.MultiHit = hitEffect.MultiHit;
        if (this.status ==0) this.status = hitEffect.status;
    }

    public static HitEffect Miss => new HitEffect();
}