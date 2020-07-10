using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    public TickerItem tickerItemPrefab;
    
    [Range(1f, 10f)]
    public float itemDuration = 3f;

    public string[] fillerItems;

    float width;
    float pixelsPerSecond;
    TickerItem currentItem;

    public void AddTickerItem(string message, float variable)
    {
        currentItem = Instantiate(tickerItemPrefab, transform);
        currentItem.Initialize(width, pixelsPerSecond, message, variable);
    }

    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        pixelsPerSecond = width / itemDuration;
        AddTickerItem(fillerItems[0], GameManager.Instance.score);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentItem.GetXPosition <= -currentItem.GetWidth)
        {
            int random = Random.Range(0, fillerItems.Length);
            switch(random)
            {
                case 0:
                    AddTickerItem(fillerItems[random], GameManager.Instance.score);
                    break;
                case 1:
                    AddTickerItem(fillerItems[random], GameManager.Instance.rounds);
                    break;
                case 2:
                    AddTickerItem(fillerItems[random], GameManager.Instance.totalSeconds);
                    break;
            }
        }
    }
}
