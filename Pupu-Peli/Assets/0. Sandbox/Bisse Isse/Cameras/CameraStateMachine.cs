using Unity.Cinemachine;
using UnityEngine;

public class CameraStateMachine : MonoBehaviour
{
    public CinemachineInputAxisController freelookCamera;
    public Animator states;
    public Camera cam;

    public void switchState(Collider other, Collider player, bool exit = false)
    {
        Debug.Log(other.tag);
        if (exit)
        {
            states.Play("Walking");
            freelookCamera.enabled = true;
            player.GetComponent<Movement>().updateAngle(true);
        }
        else if (other.tag == "Camera Switch")
        {
            states.Play(other.name);
            freelookCamera.enabled = false;
            player.GetComponent<Movement>().updateAngle(false, other.transform.GetChild(0).transform);
        }
    }

    public void changeSettings(CameraSettings settings)
    {
        cam.cullingMask &= ~(1 << settings.cullMask);
    }

    public void resetSettings()
    {
        cam.cullingMask = -1;
    }
}
