using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CallbackEvent();

public class CommonManager
{
    public static Sprite LoadImage(string _path, string _nameId)
    {
        return Resources.Load<Sprite>($"{_path}/{_nameId}");
    }
    public static GameObject LoadPrefab(string _path, string _nameId)
    {
        return Resources.Load<GameObject>($"{_path}/{_nameId}");
    }

    public static List<T> ShuffleList<T>(List<T> array)
    {
        List<T> result = new List<T>();
        T temp;

        int random1 = 0;
        int random2 = 0;

        for (int index = 0; index < array.Count; index++)
        {
            random1 = UnityEngine.Random.Range(0, array.Count);
            random2 = UnityEngine.Random.Range(0, array.Count);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }
        result = array;
        return result;
    }

    public static T[] ShuffleArray<T>(T[] array)
    {
        T[] result = { };
        T temp;

        int random1 = 0;
        int random2 = 0;

        for (int index = 0; index < array.Length; index++)
        {
            random1 = UnityEngine.Random.Range(0, array.Length);
            random2 = UnityEngine.Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }
        result = array;
        return result;

    }

    public static Quaternion GetDirection(Vector3 _from, Vector3 _to)
    {
        Vector3 direction = (_to - _from).normalized;
        float angleValue = Vector3.Angle(Vector3.up, direction);
        angleValue = (direction.x >= 0f) ? (360f - angleValue) : angleValue;

        Quaternion currentAngle = Quaternion.identity;
        currentAngle.eulerAngles = new Vector3(0f, 0f, angleValue);
        return currentAngle;
    }

    public static float GetPercent(float _origin, float _percent)
    {
        float percent = (_percent > 0f) ? _percent : 0f;
        return _origin * (percent * 0.01f);
    }
}
