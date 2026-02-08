using UnityEngine;

public class SpikeCube : BaseObstacle
{
    [Header("Move Settings")]
    public float distance = 2f;     
    public float duration = 1f;   

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    protected override void Update()
    {

        float t = Mathf.PingPong(Time.time / duration, 1f);


        float smoothT = Mathf.SmoothStep(0f, 1f, t);


        Vector3 targetPos = startPos + new Vector3(0f, 0f, distance);


        transform.localPosition = Vector3.Lerp(startPos, targetPos, smoothT);
    }
}
