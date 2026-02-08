using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour
{
    [Header("Rotate 360")]
    public float rotateSpeed = 180f;
    public Space rotateSpace = Space.Self;

    protected virtual void Update()
    {
        Vector3 axis = GetRotateAxis();

        if (axis == Vector3.zero) return;

        axis = axis.normalized;

        transform.Rotate(axis,GetRollDir()* rotateSpeed * Time.deltaTime, rotateSpace);
    }


    protected virtual Vector3 GetRotateAxis()
    {
        return Vector3.zero;
    }

    protected virtual float GetRollDir()
    {
        return 1f;
    }
}
