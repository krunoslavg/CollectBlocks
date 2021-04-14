using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class Extensions
{
    /***************************************** <GameObject> EXTENSIONS ****************************************/
    public static GameEntity GameEntity(this GameObject p_gameObject)
    {
        GameEntity l_object = null;
        l_object = p_gameObject.GetComponent<GameEntity>();
        if (l_object == null)
            l_object = p_gameObject.GetComponentInParent<GameEntity>();
        return l_object;
    }

    public static GameEntity GameEntity(this Transform p_transform)
    {
        GameEntity l_object = null;
        l_object = p_transform.GetComponent<GameEntity>();
        if (l_object == null)
            l_object = p_transform.GetComponentInChildren<GameEntity>(true);
        return l_object;
    }

    public static bool IsActive(this GameObject p_gameObject)
    {
        return p_gameObject.activeInHierarchy;
    }

    public static bool IsActive(this Transform p_transform)
    {
        return p_transform.gameObject.activeInHierarchy;
    }
       
    public static void ResetLocalPositionZtoValue(this Transform p_transform, float p_zIndex)
    {
        p_transform.localPosition = new Vector3(0f, 0f, p_zIndex);
    }

    public static float RandomValue(this Vector2 _v2)
    {
        return Random.Range(_v2.x, _v2.y);
    }
       
    public static List<T> CopyArrayToList<T>(this T[] p_array)
    {
        List<T> l_result = new List<T>();

        foreach (T l_element in p_array)
        {
            l_result.Add(l_element);
        }
        return l_result;
    }

}