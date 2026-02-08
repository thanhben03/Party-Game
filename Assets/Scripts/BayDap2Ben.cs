using UnityEngine;

public class BayDap2Ben : BaseObstacle
{
    [Header("Rotation Settings")]
    public float minAngle = -40f;
    public float maxAngle = 40f;

    public float duration = 1.2f;

    private float timer;

    protected override Vector3 GetRotateAxis()
    {
        return Vector3.right;
    }

    protected override void Update()
    {

        timer += Time.deltaTime;


        float t = Mathf.PingPong(timer / duration, 1f);


        float smoothT = Mathf.SmoothStep(0f, 1f, t);


        float angleX = Mathf.Lerp(minAngle, maxAngle, smoothT);


        transform.localRotation = Quaternion.Euler(angleX, 0f, 0f);
    }
}
