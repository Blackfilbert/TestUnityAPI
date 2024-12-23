using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    public float timerDuration = 60f;
    public float currentTime;

    private GetAPI getAPI;

    private void Start() {
        getAPI = GetComponent<GetAPI>();

        currentTime = timerDuration;
        UpdateTimerText();

        getAPI.Request();
    }

    private void Update() {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0) {
            OnTimerEnd();
            currentTime = timerDuration;
        }

        UpdateTimerText();
    }

    private void UpdateTimerText() {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTimerEnd() {
        getAPI.Request();
    }
}
