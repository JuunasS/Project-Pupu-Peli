using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public Light moon;

    [SerializeField, Range(0, 24)] public float timeOfDay;

    public float rotationSpeed;

    public Gradient skyColor;
    public Gradient equatorColor;
    public Gradient sunColor;

    private void Update()
    {
        timeOfDay += Time.deltaTime * rotationSpeed;
        if(timeOfDay > 24) { timeOfDay = 0; }

        UpdateSunRotation();
        //UpdateMoonRotation();
        UpdateLighting();
    }

    private void OnValidate()
    {
        UpdateSunRotation();
        UpdateLighting();
    }

    public void UpdateSunRotation()
    {
        float rotation = Mathf.Lerp(-90, 270, timeOfDay / 24);
        sun.transform.rotation = Quaternion.Euler(rotation, sun.transform.rotation.y, sun.transform.rotation.z);
    }

    public void UpdateMoonRotation()
    {

        float rotation = Mathf.Lerp(-270, 90, timeOfDay / 24);
        moon.transform.rotation = Quaternion.Euler(rotation, moon.transform.rotation.y, moon.transform.rotation.z);
    }

    public void UpdateLighting()
    {
        float timeFraction = timeOfDay / 24;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = equatorColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);
    }
}
