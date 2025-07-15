using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public AnimationCurve acceleration;
    public float accelerationMultiplier;
    public float maxSpeed;
    public float linearFriction;
    public float slipNegation;
    private Vector2 heading;
    private Rigidbody rb;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    public Transform cam;

    private CameraStateMachine camStates;

    public Animator model;

    private Vector3 debugLastLoc = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //camStates = GameObject.Find("Gamemaster").GetComponent<CameraStateMachine>();
    }

    void FixedUpdate()
    {
        movingPlayer();

        if (Mathf.Abs(heading.y) + Mathf.Abs(heading.x) < 0.1f)
            linearFrictionForce();

        slipNegatingForce();
    }

    void movingPlayer()
    {
        Vector3 direction = new Vector3(heading.x, 0, heading.y);

        if (direction.magnitude >= 0.1f)
        {
            model.SetBool("run", true);
            model.SetBool("idle", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //float dot = Vector3.Dot(rb.linearVelocity.normalized, moveDir.normalized);
            //float velocityInDirection = rb.linearVelocity.magnitude * dot;
            float force = acceleration.Evaluate(Mathf.Clamp01(rb.linearVelocity.magnitude / maxSpeed));

            rb.AddForce(moveDir.normalized * Time.deltaTime * force * accelerationMultiplier * rb.mass);

            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), moveDir.normalized * Time.deltaTime * force * accelerationMultiplier, Color.green);
        }
        else
        {
            model.SetBool("run", false);
            model.SetBool("idle", true);
        }
    }

    void linearFrictionForce()
    {
        //Get current velocity
        float worldVel = rb.linearVelocity.magnitude;

        float desiredVelChange = -worldVel * linearFriction;

        float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

        rb.AddForce(rb.linearVelocity.normalized * desiredAccel * rb.mass);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), rb.linearVelocity.normalized * desiredAccel, Color.cyan);
    }

    void slipNegatingForce()
    {
        Vector3 slipDir = transform.right;

        Vector3 worldVel = rb.linearVelocity;

        float slipVel = Vector3.Dot(slipDir, worldVel);

        float desiredVelChange = -slipVel * slipNegation;

        float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

        rb.AddForce(slipDir * desiredAccel * rb.mass);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), slipDir * desiredAccel, Color.red);
    }

    public void DirectionUpdate(InputAction.CallbackContext con)
    {
        Vector2 dir = con.ReadValue<Vector2>();
        heading = dir;
    }
}
