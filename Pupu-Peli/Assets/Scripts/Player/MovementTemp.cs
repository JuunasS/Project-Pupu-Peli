using UnityEngine;
using UnityEngine.EventSystems;

public class MovementTemp : MonoBehaviour
{
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player movement update");
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // TODO: Add sprint speed?
        // float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        this.transform.position += moveDir * speed * Time.deltaTime;
        //this.transform.rotation = Quaternion.LookRotation(this.transform.position - (this.transform.position + moveDir));
    }
}
