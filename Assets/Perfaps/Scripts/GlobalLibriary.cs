using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GlobalLibriary
{
    public class Mathv
    {
        public static Vector2 Sign(Vector2 v)
        {
            return new Vector2(Mathf.Sign(v.x), Mathf.Sign(v.y));
        }

        public static Vector2 Clamp(Vector2 v,Vector2 min,Vector2 max)
        {
            return new Vector2(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y));
        }
        
    }
}