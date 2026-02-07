using System.Collections;
using UnityEngine;

public class MoveYEaseInOut : MonoBehaviour
{
    [Header("Move Settings")]
    public float moveDistance = 2f;
    public float moveDuration = 1f;
    public float delayAtEnds = 0.5f;

    private Vector3 startPos;
    private Vector3 endPos;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.up * moveDistance;

        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {

            yield return MoveSmooth(startPos, endPos, moveDuration);
            yield return new WaitForSeconds(delayAtEnds);

            yield return MoveSmooth(endPos, startPos, moveDuration);
            yield return new WaitForSeconds(delayAtEnds);
        }
    }

    IEnumerator MoveSmooth(Vector3 from, Vector3 to, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / duration);

            float eased = Mathf.SmoothStep(0f, 1f, normalized);

            transform.position = Vector3.Lerp(from, to, eased);
            yield return null;
        }

        transform.position = to;
    }
}
