using TMPro;
using UnityEngine;

public class GeneralInfo : MonoBehaviour
{
    public GameObject generalInfoPanel;
    public TMP_Text generalInfoText;


    public void ActivateInfoPanel(string icoObjName)
    {
        generalInfoPanel.SetActive(true);

        generalInfoText.text = icoObjName;
    }
}
