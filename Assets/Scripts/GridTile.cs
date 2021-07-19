using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    [SerializeField] GameObject applePrefab;
    GameObject apple;
    bool hasApple = false;


    public bool HasApple()
    {
        return hasApple;
    }

    public bool SetApple()
    {
        if (hasApple)
        {
            return false;
        }
        else
        {
            apple = Instantiate(applePrefab, transform.position, Quaternion.identity);
            apple.transform.parent = transform;
            hasApple = true;
            return true;
        }
    }

    public bool TakeApple()
    {
        if(!hasApple)
        {
         return false;
        }
        else
        {
            hasApple = false;
            Destroy(applePrefab.gameObject);
            applePrefab = null;
            return true;
        }
        }
}
