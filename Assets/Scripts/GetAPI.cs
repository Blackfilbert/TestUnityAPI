using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class GetAPI : MonoBehaviour
{
    [SerializeField] private TMP_Text usdToEur;
    private const string URL = "https://api.currencylayer.com/live?access_key=e14f364bbdac795f6fb7c703eef7235b&format=1";

    private Timer timer;

    private void Start() {
        timer = GetComponent<Timer>();
    }

    public void Request() {
        StartCoroutine(ProcessRequest(URL));
    }

    private IEnumerator ProcessRequest(string uri) {
        using (UnityWebRequest request = UnityWebRequest.Get(uri)) {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError) {
                Debug.Log(request.error);
            } else {
                JSONNode currencyData = JSON.Parse(request.downloadHandler.text);

                string actualCurrency = currencyData["quotes"]["USDEUR"];

                usdToEur.text = "USD to EUR: " + actualCurrency;

                timer.currentTime = timer.timerDuration;
            }
        }
    }
}
