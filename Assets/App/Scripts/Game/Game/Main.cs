using System;
using App;
using DG.Tweening;
using UnityEngine;

public class Main : Singleton<Main>
{
    public enum TypeMessage
    {
        Game,
        Smartphone,
    }

    [SerializeField] public TypeMessage ActiveMessage = TypeMessage.Game;
    [SerializeField] public MessageControll ScenarioMessage;
    [SerializeField] public MessageControll SmartphoneMessage;

    [SerializeField] private ScenarioForGame ScenarioForGame;
    [SerializeField] private ScenarioForSmartphone ScenarioForSmartphone;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMessageActive(TypeMessage.Game);
        ScenarioForGame.Event_Prologue();        
    }

    public void SetMessageActive(TypeMessage typeMes)
    {
        ActiveMessage = typeMes;
        switch (typeMes) {
            case TypeMessage.Game:
                // ScenarioMessage.gameObject.SetActive(true);
                ScenarioMessage.SelectCtrl.centerGui.transform.DOLocalMoveY(0, ConstDef.FADETIME);
                ScenarioMessage.cvgpMessageWithFace.transform.DOLocalMoveY(0, ConstDef.FADETIME);
                // ScenarioForGame.gameObject.SetActive(true);
                
                SmartphoneMessage.SelectCtrl.centerGui.transform.DOLocalMoveY(1000, ConstDef.FADETIME);
                SmartphoneMessage.cvgpMessageWithFace.transform.DOLocalMoveY(-1000, ConstDef.FADETIME).OnComplete(() => {
                    // SmartphoneMessage.gameObject.SetActive(false);
                });
                // ScenarioForSmartphone.gameObject.SetActive(false);
                smartphone.Instance.SetShow(false);
                break;
            
            case TypeMessage.Smartphone:
                ScenarioMessage.SelectCtrl.centerGui.transform.DOLocalMoveY(1000, ConstDef.FADETIME);
                ScenarioMessage.cvgpMessageWithFace.transform.DOLocalMoveY(-1000, ConstDef.FADETIME).OnComplete(() => {
                    // ScenarioMessage.gameObject.SetActive(false);
                });
                // ScenarioForGame.gameObject.SetActive(false);
                // SmartphoneMessage.gameObject.SetActive(true);
                SmartphoneMessage.SelectCtrl.centerGui.transform.DOLocalMoveY(0, ConstDef.FADETIME);
                SmartphoneMessage.cvgpMessageWithFace.transform.DOLocalMoveY(100, ConstDef.FADETIME);
                // ScenarioForSmartphone.gameObject.SetActive(true);
                ScenarioForSmartphone.StartFunction();
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
