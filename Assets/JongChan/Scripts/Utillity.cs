using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utillity
{
    public static Vector3 TransformXZ(Vector3 _Position)
    {
        return new Vector3(_Position.x, 0f, _Position.z);
    }

    public static Vector3 GetDirection(Vector3 _From, Vector3 _To)
    {
        return (_To - _From).normalized;
    }
}
