using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform : MonoBehaviour
{
    public Vector3 coordenadasIniciales;
    public Vector3 coordenadasFinales;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //Determinamos las coordenadas iniciales.
        transform.position = coordenadasIniciales;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, coordenadasFinales, Time.deltaTime * speed);
    }
}
