using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Of_Asteroids : MonoBehaviour
{
    [SerializeField] List<GameObject> asteroids_list = new List<GameObject>();

    public GameObject Get_Asteroid_From_Pool()
    {
        foreach (var tmpObj in asteroids_list)
        {
            if (!tmpObj.activeInHierarchy)
            {
                tmpObj.SetActive(true);
                return tmpObj;
            }
        }

        return null;
    }

    public void Put_Asteroid_To_Pool(GameObject tmpObj)
    {
        tmpObj.SetActive(false);
        tmpObj.transform.parent = transform;
        tmpObj.transform.localPosition = Vector3.zero;
    }
}