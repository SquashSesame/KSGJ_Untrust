using System;
using App;
using DG.Tweening;
using UnityEditor.Searcher;
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
                ScenarioMessage.gameObject.SetActive(true);
                ScenarioMessage.transform.DOMoveY(0, ConstDef.FADETIME);
                ScenarioForGame.enabled = true;
                SmartphoneMessage.transform.DOMoveY(-1000, ConstDef.FADETIME).OnComplete(() => {
                    SmartphoneMessage.gameObject.SetActive(false);
                });
                ScenarioForSmartphone.enabled = false;
                break;
            case TypeMessage.Smartphone:
                ScenarioMessage.transform.DOMoveY(-1000, ConstDef.FADETIME).OnComplete(() => {
                    ScenarioMessage.gameObject.SetActive(false);
                });
                ScenarioForGame.enabled = false;
                SmartphoneMessage.gameObject.SetActive(true);
                SmartphoneMessage.transform.DOMoveY(100, ConstDef.FADETIME);
                ScenarioForSmartphone.enabled = true;
                ScenarioForSmartphone.Event_Prologue();
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
