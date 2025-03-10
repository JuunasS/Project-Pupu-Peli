using TMPro;
using UnityEngine;

public class GeneralInfo : MonoBehaviour
{
    public GameObject generalInfoPanel;
    public TMP_Text generalInfoText;

    public IcoListObject activeInfoItem;


    public void ActivateInfoPanel(IcoListObject icoListObj)
    {
        generalInfoPanel.SetActive(true);

        if(activeInfoItem != null ) { activeInfoItem.isActive = false; }

        icoListObj.isActive = true;

        activeInfoItem = icoListObj;

        generalInfoText.text = icoListObj.icoData.name;
    }
}
