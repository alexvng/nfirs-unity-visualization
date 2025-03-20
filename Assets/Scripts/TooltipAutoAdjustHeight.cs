using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Make tooltip react to text height (w/ padding
/// </summary>
public class TooltipAutoAdjustHeight : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    void Update()
    {
        var x = this.GetComponent<RectTransform>().sizeDelta.x;
        //TODO fix magic numbers
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(x, text.preferredHeight+35f);
    }
}
