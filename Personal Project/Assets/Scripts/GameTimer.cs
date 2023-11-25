using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{

    public TextMeshProUGUI timerText;

    private float secondsCount;
    private int minuteCount;
    private int hourCount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
    }



    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        timerText.text = string.Format("{0:00}:{1:00}", minuteCount, secondsCount);
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }

    }

}
