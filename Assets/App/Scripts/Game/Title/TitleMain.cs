using App;
using UnityEngine;
using UnityEngine.UI;

public class TitleMain : MonoBehaviour
{
    [SerializeField] private Button btnStart;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Assert(btnStart != null);
        
        btnStart.onClick.AddListener(() => {
            GameMain.Instance.StartGame();
        });

        Fader.FadeIn(ConstDef.FADETIME);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
