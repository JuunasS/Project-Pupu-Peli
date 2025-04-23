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
        if (walk)
            states.SetTrigger("Walk");
        else
        {
            states.SetTrigger(other.name);
        }
    }
}
