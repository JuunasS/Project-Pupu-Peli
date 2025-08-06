using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
    public float tutorialStartDelay;
    public List<tutorialObject> tutorialObjects = new();
    List<tutorialObject> addedMissions = new();

    public tutorialObject testObject;

    TMP_Text text;

    private void OnEnable()
    {
        text = transform.GetChild(0).GetComponent<TMP_Text>();
        StartCoroutine(startTutorial());
    }
    IEnumerator startTutorial()
    {
        yield return new WaitForSeconds(tutorialStartDelay);

        GetComponent<Animator>().Play("Tutorial Enter");
    }

    public void advanceTutorial(string missionName)
    {
        foreach (var tutorialObject in tutorialObjects)
            if (tutorialObject.missionName == missionName)
                addedMissions.Add(tutorialObject);

        text.text = "<s>";

        for (int i = 0; i < addedMissions.Count; i++)
        {
            if (i == addedMissions.Count - 1)
            {
                text.text += "</s>";
                text.text += addedMissions[i].tutorialText;
            }
            else
            {
                text.text += addedMissions[i].tutorialText;
                text.text += "\n";
            }
        }
    }

    [Button("Add Test Object")]
    public void testTutorial()
    {
        advanceTutorial(testObject.name);
    }
}
