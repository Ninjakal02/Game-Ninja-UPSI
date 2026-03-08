using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float speed = 10f;        // kelajuan terbang
    public float rotateSpeed = 600f; // kelajuan pusing
    public float destroyTime = 3f;   // masa sebelum hilang

    private float direction = 1f;    // 1 = kanan, -1 = kiri

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    public void SetDirection(float dir)
    {
        direction = dir;

        // Paksa scale.x ikut direction supaya shuriken flip betul
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        transform.localScale = scale;
    }

    void Update()
    {
        // Terbang lurus dalam world space ikut direction
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime, Space.World);

        // Pusing shuriken
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}