using UnityEngine;

public class CageController : MonoBehaviour
{
    public GameObject cageDoor;

    public void OpenCage()
    {
        if (cageDoor != null)
        {
            cageDoor.SetActive(false);
            Debug.Log("Sangkar terbuka!");
        }
    }
}