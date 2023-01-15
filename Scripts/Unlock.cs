using UnityEngine;

public class Unlock : MonoBehaviour, IDestroyable
{
    public GameObject input;
    public int ability;

    public void TakeDamage()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GameObject.Find("Walking").GetComponent<playerMovement>().UnlockAbility(ability);
        input.SetActive(true);
    }
}
