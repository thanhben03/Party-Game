using System.Collections;
using UnityEngine;

public class SmoothPingPongRotateZ : MonoBehaviour
{
    [Header("Rotation Range")]
    public float minZ = -80f;
    public float maxZ = 80f;

    [Header("Move Time")]
    public float moveDuration = 0.8f;

    [Header("Pause at ends")]
    public float pauseTime = 0.5f;

    private Coroutine rotateRoutine;

    void OnEnable()
    {
        rotateRoutine = StartCoroutine(RotateLoop());
    }

    void OnDisable()
    {
        if (rotateRoutine != null)
            StopCoroutine(rotateRoutine);
    }

    IEnumerator RotateLoop()
    {
        // giữ x,y hiện tại
        Vector3 baseEuler = transform.localEulerAngles;
        float x = baseEuler.x;
        float y = baseEuler.y;

        // random bắt đầu ở min hoặc max
        bool startAtMin = Random.value < 0.5f;

        float current = startAtMin ? minZ : maxZ;
        float target = startAtMin ? maxZ : minZ;

        // set đúng mốc ban đầu
        transform.localRotation = Quaternion.Euler(x, y, current);

        // dừng 1 tí ở mốc bắt đầu (cho tự nhiên)
        yield return new WaitForSeconds(pauseTime);

        while (true)
        {
            // xoay tới target
            yield return RotateZ(x, y, current, target);
            yield return new WaitForSeconds(pauseTime);

            // đảo hướng
            float temp = current;
            current = target;
            target = temp;
        }
    }

    IEnumerator RotateZ(float x, float y, float fromZ, float toZ)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration;

            float easedT = Mathf.SmoothStep(0f, 1f, t);
            float z = Mathf.Lerp(fromZ, toZ, easedT);

            transform.localRotation = Quaternion.Euler(x, y, z);

            yield return null;
        }

        // fix đúng mốc
        transform.localRotation = Quaternion.Euler(x, y, toZ);
    }
}
