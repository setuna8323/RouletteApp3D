using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RouletteMakeController:MonoBehaviour {

    public Transform rouletteBaseTransform;                 // ルーレット土台
    public Image rouletteBaseImage;                         // ルーレット土台の画像
    public RouletteController rouletteControllerClass;      // RouletteControllerのスクリプト
    public GameObject rouletteItemContent;                  // ルーレットアイテム

    [HideInInspector] public List<String> rouletteItemList; // ルーレットアイテムのリスト化（テキストのみ）

    // Start is called before the first frame update
    void Start() {

        // スタートボタンを非表示に設定
        rouletteControllerClass.startButton.gameObject.SetActive(false);

        // ルーレットの土台を初期化
        var obj = Instantiate(rouletteBaseImage, rouletteBaseTransform);
        obj.color = Color.cyan;
        obj.fillAmount = 1;
        obj.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }

    /** 
     * 再更新処理
     * 　ルーレットの土台となる設定を再度行う
     */
    public void RouletteItemUpdateButton() {

        // ルーレット土台を子要素含め全破棄
        foreach(Transform n in rouletteBaseTransform.transform) {
            GameObject.Destroy(n.gameObject);
        }

        // ルーレットアイテムの配列内を全てクリア
        rouletteItemList.Clear();

        // 結果出力テキストをクリア
        rouletteControllerClass.resultText.GetComponent<TextMeshProUGUI>().text = "";

        // オブジェクトに付いているタグから"RouletteItem"のオブジェクトのみ全取得
        foreach(var rouletteItem in GameObject.FindGameObjectsWithTag("RouletteItem")) {
            // インプットフィールドのテキストからルーレットアイテムを取得してリスト化
            rouletteItemList.Add(rouletteItem.GetComponent<TMP_InputField>().text);
        }

        // ルーレット土台の回転軸をリセット
        rouletteBaseTransform.transform.transform.rotation = Quaternion.Euler(0, 0, 0);

        // ルーレットアイテムの個数に応じてルーレット土台を分割する数量や回転位置と色で塗る量を再計算
        float ratePerRoulette = 1 / (float)(rouletteItemList.Count);
        float rotatePerRoulette = 360 / (float)(rouletteItemList.Count);

        if(rouletteItemList.Count > 1) { // ルーレットアイテムの個数が 1個 を超過する場合
            for(int i = 0; i < rouletteItemList.Count; i++) {
                // Prefab化したルーレット土台の画像をインスタンス化
                // ルーレット土台の子要素として生成する
                var obj = Instantiate(rouletteBaseImage, rouletteBaseTransform);
                //  ルーレット土台の画像を色で塗る量を設定
                obj.color = new Color(Random.value, Random.value, Random.value, 255);
                //  ルーレット土台の画像を塗る量を設定
                obj.fillAmount = ratePerRoulette;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = rouletteItemList[i];
                obj.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 255, 255, 255);
                // 親オブジェクト（分割したルーレットの土台）の位置を先にリセット
                obj.transform.transform.rotation = Quaternion.Euler(0, 0, 0);
                // 子オブジェクト（ルーレットの項目）の位置を設定
                obj.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, -(rotatePerRoulette / 2));
                // 親オブジェクト（分割したルーレットの土台）の位置を設定
                obj.transform.transform.rotation = Quaternion.Euler(0, 0, -rotatePerRoulette * i);
            }
            // スタートボタンを表示に設定
            rouletteControllerClass.startButton.gameObject.SetActive(true);
        } else if(rouletteItemList.Count > 0) { // ルーレットアイテムの個数が 0個 超過する場合
            var obj = Instantiate(rouletteBaseImage, rouletteBaseTransform);
            obj.color = obj.color = Color.cyan;
            obj.fillAmount = 1;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = rouletteItemList[0];
            obj.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 255, 255, 255);
            obj.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
            // スタートボタンを表示に設定
            rouletteControllerClass.startButton.gameObject.SetActive(true);
        } else { // ルーレットアイテムの個数が 0個 の場合
            var obj = Instantiate(rouletteBaseImage, rouletteBaseTransform);
            obj.color = obj.color = Color.cyan;
            obj.fillAmount = 1;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = "";
            obj.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
            // スタートボタンを非表示に設定
            rouletteControllerClass.startButton.gameObject.SetActive(false);
        }


        rouletteControllerClass.rouletteMakeControllerClass = this;
        rouletteControllerClass.rotatePerRoulette = rotatePerRoulette;
        rouletteControllerClass.roulette = rouletteBaseTransform.gameObject;
    }

    // Update is called once per frame
    void Update() {
    }
}
