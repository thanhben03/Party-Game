using StarterAssets;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerKnockback : MonoBehaviour
{
    [Header("Default Knockback Settings")]
    public float defaultPushDistance = 3f;
    public float defaultKnockUpHeight = 1.2f;
    public float pushDuration = 0.2f;

    public AnimationCurve knockbackCurve = AnimationCurve.Linear(0, 1, 1, 0);

    [Header("Animation")]
    public Animator animator;
    public string fallTriggerName = "FreeFall";

    private CharacterController controller;
    private bool isKnockback;
    public bool IsKnockback => isKnockback;
    private StarterAssets.ThirdPersonController thirdPersonController;


    private bool canControlMovement = true;
    public bool CanControlMovement => canControlMovement;

    private Coroutine knockCoroutine;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        thirdPersonController = GetComponent<StarterAssets.ThirdPersonController>();

        if (animator == null) animator = GetComponentInChildren<Animator>();
    }


    public void KnockbackFrom(Vector3 obstaclePosition)
    {
        Vector3 dir = transform.position - obstaclePosition;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.001f)
            dir = -transform.forward;

        Knockback(dir.normalized);
    }


    public void Knockback(Vector3 dir)
    {
        Knockback(dir, defaultPushDistance, defaultKnockUpHeight);
    }

    public void Knockback(Vector3 dir, float customPushDistance, float customKnockUpHeight)
    {
        if (isKnockback) return;

        dir.y = 0;
        if (dir.sqrMagnitude < 0.001f)
            dir = -transform.forward;

        dir.Normalize();

        if (knockCoroutine != null)
            StopCoroutine(knockCoroutine);

        knockCoroutine = StartCoroutine(KnockbackRoutine(dir, customPushDistance, customKnockUpHeight));
    }

    private IEnumerator KnockbackRoutine(Vector3 dir, float pushDistance, float knockUpHeight)
    {
        isKnockback = true;
        canControlMovement = false; // 👈 Khóa di chuyển trong phase đẩy ban đầu

        if (animator != null)
            animator.SetBool(fallTriggerName, true);

        float elapsed = 0f;

        float horizontalSpeed = pushDistance / pushDuration;

        float upSpeed = (knockUpHeight / pushDuration) * 3.2f;

        float verticalVelocity = upSpeed;
        float gravity = 12f;

        // Phase 1: Knockback chính (đẩy ngang + hất lên) - KHÔNG cho phép di chuyển
        while (elapsed < pushDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / pushDuration;

            float strength = knockbackCurve.Evaluate(t);

            Vector3 moveStep = Vector3.zero;


            moveStep += dir * horizontalSpeed * strength * Time.deltaTime;

            moveStep.y = verticalVelocity * Time.deltaTime;
            verticalVelocity -= gravity * Time.deltaTime;

            controller.Move(moveStep);

            yield return null;
        }

        // ✅ Sau phase đẩy, CHO PHÉP player di chuyển ngang trong khi rơi
        canControlMovement = true;

        // Phase 2: Rơi xuống - player có thể điều khiển di chuyển ngang
        //while (!controller.isGrounded)
        //{
        //    Vector3 fallStep = Vector3.zero;

        //    fallStep.y = verticalVelocity * Time.deltaTime;
        //    verticalVelocity -= gravity * Time.deltaTime;

        //    controller.Move(fallStep);

        //    yield return null;
        //}
        if (thirdPersonController != null)
            thirdPersonController.SetVerticalVelocity(verticalVelocity);

        isKnockback = false;
        canControlMovement = true; // 👈 Đảm bảo luôn true khi kết thúc

        if (animator != null)
            animator.SetBool(fallTriggerName, false);
    }


}
