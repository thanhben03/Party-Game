using UnityEngine;

public class Rotate360AndMoveAxis : MonoBehaviour
{
    [Header("Rotate 360")]
    public Vector3 rotateAxis = Vector3.forward; // trục lăn (Z)
    public float rotateSpeed = 180f;

    [Header("Move")]
    public Vector3 moveAxis = Vector3.right; // trục di chuyển (X)
    public float moveDistance = 2f;
    public float moveSpeed = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        if (moveAxis != Vector3.zero)
            moveAxis = moveAxis.normalized;

        if (rotateAxis != Vector3.zero)
            rotateAxis = rotateAxis.normalized;
    }

    void Update()
    {

        float sin = Mathf.Sin(Time.time * moveSpeed);
        float offset = sin * moveDistance;
        transform.position = startPos + moveAxis * offset;


        float direction = Mathf.Cos(Time.time * moveSpeed);

        // 3) Đồng bộ hướng lăn theo hướng di chuyển
        float rollDir = direction >= 0 ? 1f : -1f;

        // Nếu bạn thấy lăn ngược, chỉ cần đổi dấu:
        // float rollDir = direction >= 0 ? -1f : 1f;

        transform.Rotate(rotateAxis, rotateSpeed * rollDir * Time.deltaTime, Space.Self);
    }
}
