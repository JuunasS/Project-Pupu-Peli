using System.Collections.Generic;
using System;
using UnityEngine;

public class MapLocationManager : MonoBehaviour
{

    [SerializeField]
    public List<Location> allLocations;

    public static MapLocationManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

}

[Serializable]
public enum Locations
{
    Albium,
    Gorrha,
    Knoc,
    Vshoosh,
    Ckbolcheivvtgnoolho
}

[Serializable]
public class Location
{

    [SerializeField]
    public Locations location;

    [SerializeField]
    public List<string> places;

    public string GetPlaces()
    {
        string locationText = "";
        for (int i = 0; i < places.Count; i++)
        {
            if(i == 0)
            {
                locationText += places[i];
            } else
            {
                locationText += ", " + places[i];
            }
        }

        return locationText;
    }
}

