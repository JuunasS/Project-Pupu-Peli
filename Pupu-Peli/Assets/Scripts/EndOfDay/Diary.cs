using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    [SerializeField]
    public List<DayData> daysCompleted;

    public GameObject daySelectButtonPrefab;
    public Transform buttonPositionZero;
    public float buttonOffsetY;

    public GameObject scrollContent;

    // Stats panel UI
    public TMP_Text dayNumberText;
    public TMP_Text coinsText;
    public TMP_Text missionsText;
    public TMP_Text brewsText;
    public TMP_Text herbsText;

    private void OnEnable()
    {
        GenerateDaySelectButtons();
        ShowDayData(daysCompleted.Count-1);
    }

    public void AddDayData(int dayNum, int coins, string missions, string brews, string herbs)
    {
        daysCompleted.Add(new DayData(dayNum, coins, missions, brews, herbs));
    }

    public void GenerateDaySelectButtons()
    {
        int col = 0;
        for (int i = daysCompleted.Count-1; i >= 0 ; i--)
        {
            GameObject tempButton = Instantiate(daySelectButtonPrefab);

            tempButton.transform.SetParent(scrollContent.transform, false);

            tempButton.transform.localPosition = buttonPositionZero.localPosition + new Vector3(0, (-buttonOffsetY * col) - daySelectButtonPrefab.GetComponent<RectTransform>().sizeDelta.y * col);

            int tempIndex = i;
            tempButton.GetComponentInChildren<TMP_Text>().text = "Day " + (i+1);
            tempButton.GetComponent<Button>().onClick.AddListener(delegate { ShowDayData(tempIndex); });

            col++;
        }

        RectTransform temp = scrollContent.GetComponent<RectTransform>();
        temp.sizeDelta = new Vector2(temp.sizeDelta.x, temp.sizeDelta.y + daysCompleted.Count * daySelectButtonPrefab.GetComponent<RectTransform>().sizeDelta.y + daysCompleted.Count * buttonOffsetY);
    }

    public void ShowDayData(int i)
    {
        if (daysCompleted.Count != 0)
        {
            Debug.Log("Button " + i + " pressed!");
            // Set data into stats panel
            dayNumberText.text = "Day " + daysCompleted[i].dayNumber.ToString();
            coinsText.text = "Coins Collected: " + daysCompleted[i].coinsCollected.ToString();
            missionsText.text = daysCompleted[i].missionsCompleted;
            brewsText.text = daysCompleted[i].brewsCompleted;
            herbsText.text = daysCompleted[i].herbsCompleted;
        }
    }
}

[Serializable]
public class DayData
{
    public int dayNumber;
    public int coinsCollected;
    public string missionsCompleted;
    public string brewsCompleted;
    public string herbsCompleted;

    public DayData(int dayNum, int coins, string missions, string brews, string herbs)
    {
        dayNumber = dayNum;
        coinsCollected = coins;
        missionsCompleted = missions;
        brewsCompleted = brews;
        herbsCompleted = herbs;
    }
}
