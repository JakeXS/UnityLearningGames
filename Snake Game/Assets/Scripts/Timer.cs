using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float elapsedTime;
    private TimeSpan playedTime;
    private bool timerGoing;

    public TextMeshProUGUI timer;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        timerGoing = true;
        timer.text = $"00:00.00";
        StartCoroutine(UpdateTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            playedTime = TimeSpan.FromSeconds(elapsedTime);
            string timePlaying = playedTime.ToString("mm':'ss'.'fff");
            timer.text = timePlaying;

            yield return null;
        }
    }
}
