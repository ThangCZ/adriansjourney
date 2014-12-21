using UnityEngine;
using System.Collections;

public class DestroyByEnvironment : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}
