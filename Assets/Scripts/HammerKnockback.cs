using UnityEngine;

public class HammerKnockback : MonoBehaviour
{
    [Header("Hit Settings")]
    public float hitCooldown = 0.35f;
    private float lastHitTime = -999f;

    [Header("Knockback Settings")]
    public float pushDistance = 5f;

    public float knockUpHeight = 2f;

    [Header("Direction")]
    public bool invertDirection = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (Time.time - lastHitTime < hitCooldown) return;
        lastHitTime = Time.time;

        PlayerKnockback knock = other.GetComponent<PlayerKnockback>();
        if (knock == null) return;

        // Vì búa xoay trục Z => hướng vung là trục X
        Vector3 dir = transform.right;
        if (invertDirection) dir = -dir;

        knock.Knockback(dir, pushDistance, knockUpHeight);
    }
}
