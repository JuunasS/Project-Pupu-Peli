using UnityEngine;

public class testCube : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
    }
}
