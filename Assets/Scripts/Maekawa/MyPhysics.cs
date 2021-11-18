using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPhysics : MonoBehaviour
{
    public struct BoxObject
    {
        public Vector3 center;
        public float height;
        public float width;

        public BoxObject(Vector3 position, float height, float width)
        {
            this.center = position;
            this.height = height;
            this.width = width;
        }
    }

    public static bool IsBoxHit(BoxObject obj1, BoxObject obj2)
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

    public static Vector3 ComputeShiftPosition(BoxObject targetObj, BoxObject staticObj)
    {
        float shiftX = 0;
        float shiftY = 0;
        // ‰¡
        float distanceX = Mathf.Abs(targetObj.center.x - staticObj.center.x);
        float lengthX = targetObj.width / 2 + staticObj.width / 2;
        if (distanceX < lengthX)
        {
            shiftX = targetObj.center.x < staticObj.center.x ? (lengthX - distanceX) * -1 : lengthX - distanceX;
        }
        // c
        float distanceY = Mathf.Abs(targetObj.center.y - staticObj.center.y);
        float lengthY = targetObj.height / 2 + staticObj.height / 2;
        if (distanceY < lengthY)
        {
            shiftY = targetObj.center.y < staticObj.center.y ? (lengthY - distanceY) * -1 : lengthY - distanceY;
        }

        Vector3 shiftPos = new Vector3(shiftX, shiftY);
        return shiftPos;
    }
}
