using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster02Animation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();

        // Animator�� �����ϰ�, �ִϸ����� ��Ʈ�ѷ��� ���°� ��� �ϳ� �̻� �ִ��� Ȯ��
        if (animator != null && animator.runtimeAnimatorController != null && animator.runtimeAnimatorController.animationClips.Length > 0)
        {
            // ù ��° �ִϸ��̼� Ŭ�� ��������
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[0];

            // �ִϸ��̼� ���� �ݺ� ����
            animator.Play(clip.name, -1, 0f);
            animator.speed = 0.4f; // �ִϸ��̼��� 0���� �����ϸ� �����ϹǷ� 1�� ����
        }
        else
        {
            Debug.LogError("Animator ������Ʈ �Ǵ� �ִϸ����� ��Ʈ�ѷ��� ã�� �� ���ų� �ִϸ��̼��� �����ϴ�.");
        }
    }
}
