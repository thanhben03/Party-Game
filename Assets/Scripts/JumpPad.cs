using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("Jump Settings")]
    [Tooltip("Độ cao bật lên (càng cao bay càng cao)")]
    public float jumpHeight = 8f; // 👈 bật rất cao
    public float forwardPushDistance = 6f;

    [Tooltip("Lực đẩy ngang (thường để 0 hoặc nhỏ)")]
    public float horizontalPush = 0f; // 👈 đẩy ngang nhẹ

    [Header("Direction")]
    [Tooltip("Hướng đẩy ngang (để trống = hướng lên thẳng)")]
    public Vector3 pushDirection = Vector3.zero;

    [Header("Cooldown")]
    [Tooltip("Thời gian chờ giữa các lần kích hoạt")]
    public float cooldown = 0.5f;
    private float lastActivateTime = -999f;

    [Header("Visual Feedback (Optional)")]
    public ParticleSystem activateEffect;
    public AudioClip jumpSound;
    private AudioSource audioSource;

    private Animator animator;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Chỉ tác động lên player
        if (!other.CompareTag("Player")) return;
        animator.SetTrigger("Jump");

        // Kiểm tra cooldown
        if (Time.time - lastActivateTime < cooldown) return;
        lastActivateTime = Time.time;

        PlayerKnockback knock = other.GetComponent<PlayerKnockback>();
        if (knock == null) return;

        // Tính hướng đẩy
        Vector3 direction;
        if (pushDirection.sqrMagnitude > 0.001f)
        {
            // Nếu có set hướng custom
            direction = pushDirection.normalized;
        }
        else
        {
            // Mặc định: bật lên phía trước của JumpPad
            direction = transform.forward;
        }

        // ✅ Gọi knockback với lực bật cao
        knock.Knockback(direction, forwardPushDistance, jumpHeight);

        // Visual & Sound effects
        PlayEffects();
    }

    private void PlayEffects()
    {
        // Phát particle effect nếu có
        if (activateEffect != null)
        {
            activateEffect.Play();
        }

        // Phát sound nếu có
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    // Vẽ gizmo trong Editor để dễ debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 startPos = transform.position;
        
        Vector3 dir = pushDirection.sqrMagnitude > 0.001f 
            ? pushDirection.normalized 
            : transform.forward;
        
        Gizmos.DrawRay(startPos, dir * 3f);
        Gizmos.DrawWireSphere(startPos + dir * 3f, 0.3f);
    }
}
