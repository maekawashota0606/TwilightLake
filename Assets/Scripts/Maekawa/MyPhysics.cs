using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPhysics : MonoBehaviour
{
    public struct Object
    {
        public Vector3 center;
        public float height;
        public float width;

        public Object(Vector3 position, float height, float width)
        {
            this.center = position;
            this.height = height;
            this.width = width;
        }
    }

    public static bool IsBoxHit(Object obj1, Object obj2)
    {
        bool isHit = false;
        bool isMatchX = false;
        bool isMatchY = false;
        // ‰¡
        float distanceX = Mathf.Abs(obj1.center.x - obj2.center.x);
        float lengthX = obj1.width / 2 + obj2.width / 2;
        if (distanceX < lengthX)
            isMatchX = true;
        // c
        float distanceY = Mathf.Abs(obj1.center.y - obj2.center.y);
        float lengthY = obj1.height / 2 + obj2.height / 2;
        if (distanceY < lengthY)
            isMatchY = true;
        //
        if (isMatchX && isMatchY)
            isHit = true;

        return isHit;
    }
}
