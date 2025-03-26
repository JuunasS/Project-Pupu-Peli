using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed;
    private Vector2 heading;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Accelerate();
        
        Vector3 lookDirection = Vector3.RotateTowards(transform.forward, transform.right * heading.x, Time.deltaTime * 1.5f, 0);
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    private void Accelerate()
    {
        rb.AddForce(transform.forward * speed * heading.y);
        Debug.DrawRay(transform.position, heading, Color.cyan);
    }

    public void DirectionUpdate(InputAction.CallbackContext con)
    {
        Vector2 dir = con.ReadValue<Vector2>();
        heading = dir;
    }
}
