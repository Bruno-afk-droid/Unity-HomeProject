using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ActionInfo_{

    public enum DamageType
        {
            Impact=0,
            fire=1,
        }

    public class ActionInfo 
    {
        public CollisionBox[][] CollisionBoxes;
        public Rect CollisionArea;
        public ActionInfo(Dictionary<int,CollisionBox[]> KeyFrames,int length)
        {
            int previousFrame = 0;
            this.CollisionBoxes = new CollisionBox[length][];
            foreach(int frame in KeyFrames.Keys)
            {
                CollisionBoxes[frame] = KeyFrames[frame];
                if (previousFrame != frame)
                {
                    autoFillMotionArk(previousFrame, frame, AnimationCurve.Linear(0, 0, 1, 1));
                }
                previousFrame = frame;
            }
        }

        private void autoFillMotionArk(int start,int end,AnimationCurve ark)
        {
            CollisionBox[] Base = CollisionBoxes[start];
            CollisionBox[] Final = CollisionBoxes[end];
            //boxes
            for (int i = 0; i < Base.Length; i++)
            {
                CollisionBox Vel = CollisionBoxes[end][i] - CollisionBoxes[start][i];
                //frames
                for (int frame = start; frame < end; frame++)
                {
                    CollisionBoxes[frame][i] = Base[i]+(Vel*ark.Evaluate((start-frame)/(start-end)));
                }
            }
        }

        public List<Effect> IsCollidingWith(int F1,int F2,Vector2 Distance,ActionInfo AC)
        {
            List<Effect> result = new List<Effect>();
            foreach(CollisionBox B1 in CollisionBoxes[F1])
                foreach(CollisionBox B2 in AC.CollisionBoxes[F2])
                {
                    if (((Rect)B1).Overlaps(B2+Distance)){
                        result.Add(B1.conflict(B2));
                    }
                }
            return result;
        }
    
        public CollisionBox[] this[int frame] { get { return CollisionBoxes[frame]; } set { this.CollisionBoxes[frame] = value; } }
    }


    public struct CollisionBox 
    {
        public Rect Box;
        public Effect Effect;

        public CollisionBox(Rect box,Effect effect)
        {
            this.Box = box;
            this.Effect = effect;
        }

        public Effect conflict(CollisionBox target)
        {
            Effect result = this.Effect;
            result.ImpactModifier = (this.Effect.ImpactModifier / target.Effect.ImpactModifier) / 2;
            return result;
        }

        public static CollisionBox operator +(CollisionBox c,Rect r)
        {
            c.Box.x += r.x;
            c.Box.y += r.y;
            c.Box.width += r.width;
            c.Box.height += r.height;
            return c;
        }
        public static CollisionBox operator -(CollisionBox c, Rect r)
        {
            c.Box.x -= r.x;
            c.Box.y -= r.y;
            c.Box.width -= r.width;
            c.Box.height -= r.height;
            return c;
        }

        public static CollisionBox operator *(CollisionBox c, float m)
        {
            c.Box.x *= m;
            c.Box.y *= m;
            c.Box.width *= m;
            c.Box.height *= m;
            return c;
        }

        public static Rect operator +(CollisionBox c, Vector2 p)
        {
            Rect result = c;
            result.position += p;
            return result;
        }

        public static implicit operator Rect(CollisionBox v)
        {
        return v.Box;
        }
    }

    public struct Effect
    {
        public DamageType DamageType;
        public float ImpactModifier;
        public Vector2 KnockbackAngle;
        public int Multihit;
        public int Stun;

        public Effect(int S = 1, int MH = 1, float IM = 1.0f,DamageType DT=0, Vector2 KA = new Vector2())
        {
            this.Stun = S;
            this.Multihit = MH;
            this.ImpactModifier = IM;
            this.DamageType = DT;
            this.KnockbackAngle = KA;
        }

        //public static Effect operator *(Effect a, Effect b) => new Effect(a.Stun*b.Stun, a.Multihit * b.Multihit, a.ImpactModifier * b.ImpactModifier, a.DamageType, a.KnockbackAngle * b.KnockbackAngle);
    }
}