using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RouletteController:MonoBehaviour {

    [HideInInspector] public GameObject roulette;
    [HideInInspector] public float rotatePerRoulette;
    [HideInInspector] public RouletteMakeController rouletteMakeControllerClass;
    [HideInInspector] public TextMeshProUGUI resultText;    //結果出力テキスト

    private string result;
    private float rouletteSpeed;
    private float slowDownSpeed;
    private int frameCount;
    private bool isPlaying;                                 //実行判定
    private bool isStop;                                    //停止判定

    public Button startButton;              // スタートボタン
    public Button stopButton;               // ストップボタン
    public GameObject rouletteItemBaseObj;  // ルーレットアイテムの土台となるオブジェクト

    public void RouletteStartOnClick() {
        startButton.gameObject.SetActive(false);
        isStop = false;
        resultText.text = "";
        rouletteSpeed = 14f;
        Invoke("ShowStopButton", 1.5f);
        isPlaying = true;
        rouletteItemBaseObj.gameObject.SetActive(false);
    }
    private void ShowStopButton() {
        stopButton.gameObject.SetActive(true);
    }

    public void RouletteStopOnClick() {
        stopButton.gameObject.SetActive(false);
        isStop = true;
        slowDownSpeed = Random.Range(0.92f, 0.98f);
    }

    private void ShowResult(float x) {
        for(int i = 1; i <= rouletteMakeControllerClass.rouletteItemList.Count; i++) {
            if(((rotatePerRoulette * (i - 1) <= x) && x <= (rotatePerRoulette * i)) ||
                (-(360 - ((i - 1) * rotatePerRoulette)) >= x && x >= -(360 - (i * rotatePerRoulette)))) {
                result = rouletteMakeControllerClass.rouletteItemList[i - 1];
            }
        }
        resultText.text = "当選結果:" + result;
        startButton.gameObject.SetActive(true);
        rouletteItemBaseObj.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start() {
        isPlaying = false;
        isStop = false;
        startButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(false);
        resultText.text = "";
    }

    // Update is called once per frame
    void Update() {
        if(!isPlaying)
            return;
        roulette.transform.Rotate(0, 0, rouletteSpeed);
        frameCount++;
        if(isStop && frameCount > 3) {
            rouletteSpeed *= slowDownSpeed;
            slowDownSpeed -= 0.25f * Time.deltaTime;
            frameCount = 0;
        }
        if(rouletteSpeed < 0.05f) {
            isPlaying = false;
            ShowResult(roulette.transform.eulerAngles.z);
        }
    }
}
