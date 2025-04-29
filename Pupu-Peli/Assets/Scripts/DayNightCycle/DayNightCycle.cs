using System.Collections;
using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public Light moon;

    [SerializeField, Range(0, 24)] public float timeOfDay;
    [SerializeField, Range(0, 12)] public float dayNightTransitionValue; // 12 = peak daylight, 0 = midnight

    public float rotationSpeed;

    public Gradient skyColor;
    public Gradient equatorColor;
    public Gradient sunColor;

    public TMP_Text timeText;


    private void Update()
    {
        timeOfDay += Time.deltaTime * rotationSpeed;
        if (timeOfDay > 24) { timeOfDay = 0; }

        SetTimeText();

        UpdateSunRotation();
        UpdateMoonRotation();
        UpdateLighting();

        if (timeOfDay > 0 && timeOfDay < 12)
        {
            // time of day 0 -> 12 == tValue: 0 -> 12
            dayNightTransitionValue = timeOfDay;
        }
        else // Less than 24 and more than 12
        {
            dayNightTransitionValue = 24 - timeOfDay;
        }

    }

    private void LateUpdate()
    {
        RenderSettings.skybox.SetColor("_LightColor", sun.color);
        RenderSettings.skybox.SetVector("_MoonDir", moon.transform.forward);
        /*
        int isActive = 1;
        if (moon.transform.forward.y < 0) { isActive = 0; }
        RenderSettings.skybox.SetInt("_SunActive", isActive);
        
        */
        RenderSettings.skybox.SetFloat("_TransitionValue", dayNightTransitionValue);
    }


    private void SetTimeText()
    {
        string[] currentTime = System.Math.Round(timeOfDay, 2).ToString().Split(',');

        if (timeOfDay < 10)
        {
            if (currentTime.Length == 1)
            {
                timeText.text = "Time: 0" + currentTime[0] + ":00";
            }
            else
            {
                timeText.text = "Time: 0" + currentTime[0] + ":" + currentTime[1];
            }
        }
        else
        {
            if (currentTime.Length == 1)
            {
                timeText.text = "Time: " + currentTime[0] + ":00";
            }
            else
            {
                timeText.text = "Time: " + currentTime[0] + ":" + currentTime[1];
            }
        }
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
