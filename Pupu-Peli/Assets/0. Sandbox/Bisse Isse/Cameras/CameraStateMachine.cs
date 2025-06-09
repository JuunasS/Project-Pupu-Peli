using UnityEngine;

public class CameraStateMachine : MonoBehaviour
{
    public Animator states;
    public Camera cam;

    public void switchState(Collider other, bool exit = false)
    {
        Debug.Log(other.tag);
        if (exit)
            states.Play("Walking");
        else if (other.tag == "Camera Switch")
        {
            states.Play(other.name);
        }
    }

    public void changeSettings(CameraSettings settings)
    {
        cam.cullingMask &= ~(1 << settings.cullMask);
    }

    public void resetSettings()
    {
        Debug.Log("Resetting culling mask");
        cam.cullingMask = -1;
    }
}
