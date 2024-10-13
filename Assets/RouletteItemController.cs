using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteItemController:MonoBehaviour {

    /**
     * 
     */
    public Transform RouletteItemScrollContent;     // スクロールビュー表示用
    public RectTransform RouletteItemContent;       // ルーレットの項目（Prefab化）
    public RouletteMakeController rouletteMakeControllerClass;
    private int num = 0;

    /**
     * アニメーション設定
     */
    public Animator rouletteItemDisplaySwitchingAnimator;
    private bool isDispHideFlg;


    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void RouletteItemAddButton() {
        var item = Instantiate(RouletteItemContent);
        item.transform.SetParent(RouletteItemScrollContent, false);
        int temp = num + 0; // 一時変数に格納しないと引数の値が全て同じ数字となるバグ
        foreach(Button child in item.GetComponentsInChildren<Button>()) {
            child.name = "delete" + num;
            child.onClick.AddListener(() => RouletteItemDeleteButton(temp));
        }
        num++;
    }

    public void RouletteItemUpdateButton() {
        rouletteMakeControllerClass.RouletteItemUpdateButton();
    }

    public void RouletteItemDeleteButton(int j) {
        foreach(Button child in RouletteItemScrollContent.GetComponentsInChildren<Button>()) {
            if(child.name.Equals("delete" + j)) {
                Destroy(child.transform.parent.parent.gameObject);
            }
        }
    }

    public void RouletteItemDisplaySwitchButton() {
        if(isDispHideFlg) {
            rouletteItemDisplaySwitchingAnimator.SetBool("isHideFlg", isDispHideFlg);
            isDispHideFlg = false;
        } else {
            rouletteItemDisplaySwitchingAnimator.SetBool("isHideFlg", isDispHideFlg);
            isDispHideFlg = true;
        }
    }

    public void RouletteItemDisplaySwitchStartButton() {
        rouletteItemDisplaySwitchingAnimator.SetBool("isHideFlg", true);
        isDispHideFlg = false;
    }
}