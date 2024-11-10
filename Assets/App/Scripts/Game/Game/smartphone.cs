using UnityEngine;
using UnityEngine.UI;

public class smartphone : MonoBehaviour
{
    public Button smartphoneButton;
    public GameObject smartphoneScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        smartphoneButton.onClick.AddListener(phoneClick);
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    void phoneClick()
    {
        smartphoneScreen.SetActive(true);
    }
}
