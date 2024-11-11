using App;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class smartphone : Singleton<smartphone>
{
    public Button smartphoneButton;
    public GameObject smartphoneScreen;

    private bool isAcceptOperation = false;
    private CanvasGroup cgSmartphone;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cgSmartphone = GetComponent<CanvasGroup>();
        Debug.Assert(cgSmartphone != null);
        cgSmartphone.alpha = 0;
        smartphoneButton.onClick.AddListener(phoneClick);
        smartphoneButton.interactable = false;
        isAcceptOperation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Fader.Instance.GetFadeLevel(Fader.Layer.MAIN) > 0
            || Fader.Instance.GetFadeLevel(Fader.Layer.SUB) > 0
            || MessageControll.Instance.IsTeloping
            ) {
            // フェード中は操作不能にする
            if (isAcceptOperation) {
                smartphoneButton.interactable = false;
                isAcceptOperation = false;
                cgSmartphone.DOFade(0, ConstDef.FADETIME);
            }
        }
        else {
            // 操作を受け付ける
            if (isAcceptOperation == false) {
                isAcceptOperation = true;
                cgSmartphone.DOFade(1, ConstDef.FADETIME).OnComplete(() => {
                    smartphoneButton.interactable = true;
                });
            }
        }
    }

    
    void phoneClick()
    {
        smartphoneScreen.SetActive(true);
    }
}
