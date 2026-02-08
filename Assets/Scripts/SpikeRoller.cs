using UnityEngine;

public class SpikeRoller : BaseObstacle
{
    [Header("Move X")]
    public float moveDistance = 3f;
    public float moveSpeed = 1f;

    private Vector3 startPos;
    public Vector3 rotateAxis = Vector3.forward;
    protected override Vector3 GetRotateAxis()
    {
        return rotateAxis;
    }

    void Start()
    {
        startPos = transform.position;
    }

    protected override void Update()
    {
        
        base.Update();

        // 4) Apply position
        transform.position = GetNewPos();
    }

    protected override float GetRollDir()
    {

        float direction = Mathf.Cos(Time.time * moveSpeed);
        float rollDir = direction >= 0 ? -1f : 1f;

        return rollDir;
    }

    Vector3 GetNewPos()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        Vector3 newPos = startPos + Vector3.right * offset;

        return newPos;
    }
}
