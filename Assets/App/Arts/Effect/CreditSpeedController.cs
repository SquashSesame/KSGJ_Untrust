using UnityEngine;

public class CreditSpeedController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // マウスボタンを押している間は10倍速
        if (Input.GetMouseButton(0))
        {
            animator.speed = 10f;
        }
        // マウスボタンを離したら通常速度
        else
        {
            animator.speed = 1f;
        }
    }
}