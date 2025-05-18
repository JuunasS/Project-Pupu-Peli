using UnityEngine;

public class CameraStateMachine : MonoBehaviour
{
    private Animator states;

    void Start()
    {
        states = GetComponent<Animator>();
    }
    public void switchState(Collider other, bool walk = false)
    {
        Debug.Log(other.tag);
        if (walk)
            states.SetTrigger("Walk");
        else if (other.tag == "Camera Switch")
        {
            states.SetTrigger(other.name);
        }
    }
}
