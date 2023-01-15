using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBack : MonoBehaviour, IDestroyable
{
    public float timeToShowText;

    public void TakeDamage()
    {
        StartCoroutine(BackToMenu());
    }

    private IEnumerator BackToMenu()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(timeToShowText);

        SceneManager.LoadScene("Menu");
    }
}
