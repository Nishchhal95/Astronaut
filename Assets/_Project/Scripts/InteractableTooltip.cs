using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableTooltip : MonoBehaviour
{
    public TextMeshProUGUI tooltipText;
    public Image toolTipBG;

    public void SetToolTipText(string msg)
    {
        tooltipText.SetText(msg);
    }

    public float GetToolTipWidth()
    {
        return toolTipBG.GetPixelAdjustedRect().width;
    }
    
    public float GetToolTipHeight()
    {
        return toolTipBG.GetPixelAdjustedRect().height;
    }
}
