using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable") {
            other.gameObject.tag = "CollectedCube";
            other.gameObject.AddComponent<CollisionManager>();
            StackManager.instance.StackCollectable(other.gameObject, StackManager.instance.collectedList.Count - 1);
        }
    }
}
