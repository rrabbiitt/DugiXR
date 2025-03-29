using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster02Animation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        // Animator가 존재하고, 애니메이터 컨트롤러에 상태가 적어도 하나 이상 있는지 확인
        if (animator != null && animator.runtimeAnimatorController != null && animator.runtimeAnimatorController.animationClips.Length > 0)
        {
            // 첫 번째 애니메이션 클립 가져오기
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[0];

            // 애니메이션 무한 반복 설정
            animator.Play(clip.name, -1, 0f);
            animator.speed = 0.4f; // 애니메이션을 0으로 설정하면 정지하므로 1로 설정
        }
        else
        {
            Debug.LogError("Animator 컴포넌트 또는 애니메이터 컨트롤러를 찾을 수 없거나 애니메이션이 없습니다.");
        }
    }
}
