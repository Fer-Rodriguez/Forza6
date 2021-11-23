using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{

    public GameObject prefabAuto;
    public int numCars;
    List<GameObject> listCars;

    // Start is called before the first frame update
    void Start()
    {
        listCars = new List<GameObject>();
        for (int i = 0; i < numCars; i++)
        {
            float x = Random.Range(-500, 500);
            float y = 0f;
            float z = Random.Range(-500, 500);
            listCars.Add(Instantiate(prefabAuto, new Vector3(x, y, z), Quaternion.identity));
        }
    }
}
