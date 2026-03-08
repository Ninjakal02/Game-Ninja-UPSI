using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject shurikenPrefab;
    public Transform throwPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowShuriken();
        }
    }

    void ThrowShuriken()
    {
        Instantiate(shurikenPrefab, throwPoint.position, transform.rotation);
    }
}