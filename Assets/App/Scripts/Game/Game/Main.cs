using App;
using UnityEngine;

public class Main : Singleton<Main>
{
    [SerializeField] public MessageControll ScenarioMessage;
    [SerializeField] public MessageControll SmartphoneMessage;

    [SerializeField] private ScenarioForGame ScenarioForGame;
    [SerializeField] private ScenarioForSmartphone ScenarioForSmartphone;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScenarioForGame.Event_Prologue();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
