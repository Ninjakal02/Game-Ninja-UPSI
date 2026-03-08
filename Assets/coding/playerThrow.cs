using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject ShirukenPrefab;
    public Transform ThrowPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowShiruken();
        }
    }

    void ThrowShiruken()
    {
        Instantiate(ShirukenPrefab, ThrowPoint.position, transform.rotation);
    }
}