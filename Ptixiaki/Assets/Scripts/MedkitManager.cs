using UnityEngine;

public class MedkitManager : MonoBehaviour
{
    private GameObject[] medkits;

    void Start()
    {
        // Cache all medkits in the scene
        medkits = GameObject.FindGameObjectsWithTag("Medkit");
    }

    public void ResetMedkits()
    {
        foreach (GameObject medkit in medkits)
        {
            if (medkit != null)
                medkit.SetActive(true);
        }
    }
}


