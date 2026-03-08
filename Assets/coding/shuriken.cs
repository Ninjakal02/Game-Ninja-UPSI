using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float speed = 10f;
    public float destroyTime = 3f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}