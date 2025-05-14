using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed;
    private Vector2 heading;
    private Rigidbody rb;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    public Transform cam;

    private CameraStateMachine camStates;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //camStates = GameObject.Find("Gamemaster").GetComponent<CameraStateMachine>();
    }

    void Update()
    {
        Vector3 direction = new Vector3(heading.x, 0, heading.y);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(transform.position + moveDir.normalized * Time.deltaTime * speed);
        }
    }

    public void DirectionUpdate(InputAction.CallbackContext con)
    {
        Vector2 dir = con.ReadValue<Vector2>();
        heading = dir;
    }

    /*void OnTriggerEnter(Collider other)
    {
        camStates.switchState(other);
    }

    void OnTriggerExit(Collider other)
    {
        camStates.switchState(other, true);
    }*/
}
