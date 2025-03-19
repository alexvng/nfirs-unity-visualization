using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoAdjustHeight : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        var x = this.GetComponent<RectTransform>().sizeDelta.x;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(x, text.preferredHeight);
    }
}
