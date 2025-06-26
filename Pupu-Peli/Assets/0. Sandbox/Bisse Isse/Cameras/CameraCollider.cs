using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    CameraSettings settings = new();

    public bool useCustomSettings;

    public int cullMask;

    void Awake()
    {
        settings.cullMask = cullMask;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.root.GetComponent<CameraStateMachine>().switchState(transform.GetComponent<Collider>());

            if (useCustomSettings)
                transform.root.GetComponent<CameraStateMachine>().changeSettings(settings);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.root.GetComponent<CameraStateMachine>().switchState(transform.GetComponent<Collider>(), true);

            if (useCustomSettings)
                transform.root.GetComponent<CameraStateMachine>().resetSettings();
        }
    }
}

public class CameraSettings : MonoBehaviour
{
    public int cullMask;
}