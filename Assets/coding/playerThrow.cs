using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject ShirukenPrefab;
    public Transform ThrowPoint;

    public int maxShiruken = 10;
    private int currentShiruken = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowShiruken();
        }
    }

    void ThrowShiruken()
    {
        if (currentShiruken >= maxShiruken)
        {
            Debug.Log("Shiruken habis!");
            return;
        }

        Instantiate(ShirukenPrefab, ThrowPoint.position, transform.rotation);

        currentShiruken++;
        Debug.Log("Shiruken digunakan: " + currentShiruken);
    }
}