using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableTooltip : MonoBehaviour
{
    public TextMeshProUGUI tooltipText;

    public void SetToolTipText(string msg)
    {
        tooltipText.SetText(msg);
    }
}
