using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    CameraSettings settings;

    public bool useCustomSettings;

    public int cullMask;

    void Awake()
    {
        settings = new();
        settings.cullMask = cullMask;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<CameraStateMachine>().switchState(transform.GetComponent<Collider>(), other.GetComponent<Collider>());

            if (useCustomSettings)
                transform.parent.GetComponent<CameraStateMachine>().changeSettings(settings);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.parent.GetComponent<CameraStateMachine>().switchState(transform.GetComponent<Collider>(), other.GetComponent<Collider>(), true);

            if (useCustomSettings)
                transform.parent.GetComponent<CameraStateMachine>().resetSettings();
        }
    }
}

public class CameraSettings : ScriptableObject
{
    public int cullMask;
}