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
        // �}�E�X�{�^���������Ă���Ԃ�10�{��
        if (Input.GetMouseButton(0))
        {
            animator.speed = 10f;
        }
        // �}�E�X�{�^���𗣂�����ʏ푬�x
        else
        {
            animator.speed = 1f;
        }
    }
}