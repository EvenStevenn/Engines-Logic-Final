using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public bool timerStart;
    public int timerAtStart = 25;
    public float currentTimerValue;
    public TextMeshProUGUI timerText;
    public GameObject timerUI;
    public GameObject waveManager;
    public WaveManager waveManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        //setting up the timer's start value
        TimerReset();
        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        waveManagerScript = waveManager.GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //code for the timer
        if (timerStart)
        {
            currentTimerValue -= Time.deltaTime;
            timerText.text = Mathf.RoundToInt(Mathf.Ceil(currentTimerValue)).ToString();
        }

        if (currentTimerValue < 0)
        {
            TimerReset();
            timerStart = false;
            timerUI.SetActive(false);
            waveManagerScript.StartNextWave();
            waveManagerScript.waveNumber.text = (waveManagerScript.waveID+1).ToString();
        }

    }
        public void TimerReset()
        {
            currentTimerValue = timerAtStart;
        }
}

