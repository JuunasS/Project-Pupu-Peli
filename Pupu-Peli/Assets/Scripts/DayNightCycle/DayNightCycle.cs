using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum TimeOfDay
{
    Morning = 6,
    Afternoon = 12,
    Evening = 18,
    Night = 24,
}

public class DayNightCycle : MonoBehaviour
{

    public Light sun;
    public Light moon;

    [SerializeField, Range(0, 24)] public float time;
    [SerializeField, Range(0, 12)] public float dayNightTransitionValue; // 12 = peak daylight, 0 = midnight

    public float rotationSpeed;

    public Gradient skyColor;
    public Gradient equatorColor;
    public Gradient sunColor;

    public TMP_Text timeText;

    // Day time variables
    public static event Action<TimeOfDay> OnDayTimeChanged;
    public TimeOfDay timeOfDay;

    public static DayNightCycle Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Debug.Log("Day Night Cycle manager is null!");
            Instance = this;
        }
    }


    private void Update()
    {
        time += Time.deltaTime * rotationSpeed;
        if (time > 24) { time = 0; }

        SetTimeText();

        UpdateSunRotation();
        UpdateMoonRotation();
        UpdateLighting();

        if (time >= 0 && time <= 12)
        {
            // time of day 0 -> 12 == tValue: 0 -> 12
            dayNightTransitionValue = time;
        }
        else // Less than 24 and more than 12
        {
            dayNightTransitionValue = 24 - time;
        }

        // TODO: Add events for times of day!
        CheckDayTime();

    }

    public void CheckDayTime()
    {
        // Invoke daytime change when a new day time starts (Should trigger once)
        if (time > 6 && time < 12 && timeOfDay != TimeOfDay.Morning)
        {
            timeOfDay = TimeOfDay.Morning;
        }
        else if (time > 12 && time < 18 && timeOfDay != TimeOfDay.Afternoon)
        {
            timeOfDay = TimeOfDay.Afternoon;
        }
        else if (time > 18 && time < 24 && timeOfDay != TimeOfDay.Evening)
        {
            timeOfDay = TimeOfDay.Evening;
        }
        else if (time > 0 && time < 6 && timeOfDay != TimeOfDay.Night)
        {
            timeOfDay = TimeOfDay.Night;
        }
        else
        {
            return;
        }
        Debug.Log("Day time changed to: " + timeOfDay);
        OnDayTimeChanged?.Invoke(timeOfDay);
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
        string[] currentTime = System.Math.Round(time, 2).ToString().Split(',');

        if (time < 10)
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
        float rotation = Mathf.Lerp(-90, 270, time / 24);
        sun.transform.rotation = Quaternion.Euler(rotation, sun.transform.rotation.y, sun.transform.rotation.z);
    }

    public void UpdateMoonRotation()
    {
        float rotation = Mathf.Lerp(-270, 90, time / 24);
        moon.transform.rotation = Quaternion.Euler(rotation, moon.transform.rotation.y, moon.transform.rotation.z);
    }


    public void UpdateLighting()
    {
        float timeFraction = time / 24;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = equatorColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);
    }

    public void SetTime(float newTime)
    {
        time = newTime;
    }
}
