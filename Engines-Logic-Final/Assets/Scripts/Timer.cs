using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer Attributes")]
    public bool timerStart;
    public int timerAtStart = 10;

    [Header("Variable References")]
    public float currentTimerValue;

    [Header("Manual references")]
    public TextMeshProUGUI timerText;
    public GameObject timerUI;
    public GameObject waveManager;

    [Header("Automatic References")]
    public WaveManager waveManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Setting up the timer's start value
        TimerReset();
        waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        waveManagerScript = waveManager.GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // code for the timer
        if (timerStart)
        {
            currentTimerValue -= Time.deltaTime;
            timerText.text = Mathf.RoundToInt(Mathf.Ceil(currentTimerValue)).ToString();
        }

        // Starts the next wave and disables both the timer and its UI when over
        if (currentTimerValue < 0)
        {
            TimerReset();
            timerStart = false;
            timerUI.SetActive(false);
            waveManagerScript.StartNextWave();
            waveManagerScript.waveNumber.text = (waveManagerScript.waveID+1).ToString();
        }

    }

    // Resets the timer
    public void TimerReset()
    {
        currentTimerValue = timerAtStart;
    }
}

