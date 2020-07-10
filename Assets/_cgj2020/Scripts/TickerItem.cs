using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class TickerItem : MonoBehaviour
{
    float tickerWidth;
    float pixelsPerSecond;
    RectTransform rt;

    public float GetXPosition { 
        get {
                return rt.anchoredPosition.x;
        }
    }
    public float GetWidth { 
        get {
                return rt.rect.width;
        } 
    }

    public void Initialize(float tickerWidth, float pixelsPerSecond, string message, float value)
    {
        this.tickerWidth = tickerWidth;
        this.pixelsPerSecond = pixelsPerSecond;
        rt = GetComponent<RectTransform>();
        GetComponent<TextMeshProUGUI>().text = message + ": " + value + " ";
    }

    // Update is called once per frame
    void Update()
    {
        rt.position += Vector3.left * pixelsPerSecond * Time.deltaTime;

        if (GetXPosition <= 0 - tickerWidth - GetWidth)
            Destroy(gameObject);
    }
}
