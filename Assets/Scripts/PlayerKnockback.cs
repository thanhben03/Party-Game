using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerKnockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    public float pushDistance = 3f;
    public float pushDuration = 0.2f;
    public AnimationCurve knockbackCurve = AnimationCurve.Linear(0, 1, 1, 0);

    [Header("Animation")]
    public Animator animator;
    public string fallTriggerName = "FreeFall";

    private CharacterController controller;
    private bool isKnockback;
    public bool IsKnockback => isKnockback;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    public void KnockbackFrom(Vector3 obstaclePosition)
    {
        if (isKnockback) return;

        Vector3 dir = transform.position - obstaclePosition;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.001f)
            dir = -transform.forward;

        StartCoroutine(KnockbackRoutine(dir.normalized));
    }

    private IEnumerator KnockbackRoutine(Vector3 dir)
    {
        isKnockback = true;

        if (animator != null)
            animator.SetBool(fallTriggerName, true);

        float elapsed = 0f;

        while (elapsed < pushDuration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / pushDuration;

            float strength = knockbackCurve.Evaluate(normalizedTime);

            Vector3 moveStep = dir * (pushDistance / pushDuration) * strength * Time.deltaTime;

            moveStep.y -= 9.81f * Time.deltaTime;

            controller.Move(moveStep);
            yield return null;
        }
        StartCoroutine(WaitForFreeFall());
    }

    IEnumerator WaitForFreeFall()
    {
        yield return new WaitForSeconds(1.6f);
        isKnockback = false;
        animator.SetBool(fallTriggerName, false);

    }
}