using UnityEngine;

public class Rotate360AndMoveAxis : MonoBehaviour
{
    [Header("Rotate 360")]
    public float rotateSpeed = 180f;

    [Header("Move")]
    public Vector3 moveAxis = Vector3.right;
    public float moveDistance = 2f; 
    public float moveSpeed = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;


        if (moveAxis != Vector3.zero)
            moveAxis = moveAxis.normalized;
    }

    void Update()
    {

        transform.Rotate(rotateSpeed * Time.deltaTime, 0f, 0f, Space.Self);


        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = startPos + moveAxis * offset;
    }
}
