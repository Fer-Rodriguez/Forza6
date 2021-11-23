using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform : MonoBehaviour
{
    Vector3[] vertices;
    float angulo;
    // Start is called before the first frame update
    void Start()
    {
        angulo = 0;
        Mesh malla = GetComponent<MeshFilter>().mesh;
        vertices = malla.vertices;

        // Usamos el vector 2 ya que con esto el eje z siempre estará en 0.
        transform.position = new Vector2(1, 1);

        // Movemos el objeto en un solo eje.
        Vector3 newPosition = transform.position; // Almacenamos la posición actual.
        newPosition.y = 0; 
        transform.position = newPosition; 
    }

    // Update is called once per frame
    void Update()
    {
        // Añadimos +1 a cada frame en el eje x, para moverse.
        // Time.deltaTime el tiempo en que se termina de completar el último frame.
        // Y como resultado el objeto se mueve una unidad en el eje X cada frame.
        transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
        //angulo += 1.0f;
        //Transformaciones();
    }

    void Transformaciones()
    {
        int nv = vertices.Length;
        Vector4[] vs = new Vector4[nv];
        Vector3[] formaFinal = new Vector3[nv];

        for(int i = 0; i < nv; i++)
        {
            vs[i] = vertices[i];
            vs[i].w = 1.0f;
        }

        //Rotación
        Matrix4x4 tranform = Transformations.RotateM(angulo, Transformations.AXIS.AX_Y);

        for(int j = 0; j < nv; j++)
        {
            Vector4 temp = new Vector4(vertices[j].x, vertices[j].y, vertices[j].z, 1);
            vs[j] = tranform * temp;
            formaFinal[j] = vertices[j];
        }

        GetComponent<MeshFilter>().mesh.vertices = formaFinal;
    }
}
