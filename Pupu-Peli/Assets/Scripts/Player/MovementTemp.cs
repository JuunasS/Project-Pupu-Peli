using UnityEngine;
using UnityEngine.EventSystems;

public class MovementTemp : MonoBehaviour
{
    public Transform cam;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player movement update");
        // TODO: Add sprint speed?
        // float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;


        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            this.transform.position += moveDir * speed * Time.deltaTime;
        }
        //this.transform.rotation = Quaternion.LookRotation(this.transform.position - (this.transform.position + moveDir));
    }
}
