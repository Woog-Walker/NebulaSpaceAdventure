using System.Collections.Generic;
using UnityEngine;

public class Pool_Of_Bullets : MonoBehaviour
{
    [SerializeField] List<GameObject> bullets_list = new List<GameObject>();

    public GameObject Get_Bulet_From_Pool()
    {
        foreach (var tmpObj in bullets_list)
        {
            if (!tmpObj.activeInHierarchy)
            {
                tmpObj.SetActive(true);
                return tmpObj;
            }
        }

        return null;
    }

    public void Put_Bullet_To_Pool(GameObject tmpObj)
    {
        tmpObj.transform.parent = transform;
        tmpObj.transform.localPosition = Vector3.zero;

        tmpObj.SetActive(false);

    }
}