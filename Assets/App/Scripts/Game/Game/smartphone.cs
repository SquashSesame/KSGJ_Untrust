using App;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class smartphone : Singleton<smartphone>
{
    public Button smartphoneButton;
    public GameObject smartphoneScreen;
    public float timer;

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
    private void OnEnable()
    {
        timer = Random.Range(10, 25);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
        }

        else
        {

            Debug.Log("time up");
            smartphoneScreen.SetActive(false);
            gameObject.SetActive(false);
        }
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
        if (smartphoneScreen.activeInHierarchy == false)
            smartphoneScreen.SetActive(true);
        else if (smartphoneScreen.activeInHierarchy == true)
        {
            smartphoneScreen.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}
