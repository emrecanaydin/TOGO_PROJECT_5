using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{

    public Mesh updatedMesh;
    public Material updatedMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gate")
        {
            GetComponent<MeshFilter>().mesh = updatedMesh;
            GetComponent<MeshRenderer>().material = updatedMaterial;
            gameObject.tag = "CollectedSphere";
        }
    }

}
