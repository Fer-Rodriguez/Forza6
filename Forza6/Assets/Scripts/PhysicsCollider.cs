using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCollider : MonoBehaviour
{
    public GameObject prefabauto;
    // Start is called before the first frame update
    void Start()
    {
        //Agregar colisionador
        var boxCollider2 = prefabauto.AddComponent<BoxCollider>();

        //Agregar físicas con RigidBody
        Rigidbody gameObjectsRigidBody = prefabauto.AddComponent<Rigidbody>(); // Add the rigidbody.
        gameObjectsRigidBody.mass = 5; // Set the GO's mass to 5 via the Rigidbody.
    }
}
