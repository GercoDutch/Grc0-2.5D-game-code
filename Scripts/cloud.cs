using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    public float getal;
    public LayerMask mask;
    private Coroutine routine;

    public IEnumerator BegoneThot()
    {
        yield return new WaitForSeconds(getal);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(getal);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;

        routine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GroundCheck")
        {
            Debug.Log(1);

            if (routine != null)
                return;

            Debug.Log(2);

            routine = StartCoroutine("BegoneThot");
        }
    }
}
