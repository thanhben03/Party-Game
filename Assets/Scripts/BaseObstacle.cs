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

    //private void OnCol(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Rigidbody rb = other.GetComponent<Rigidbody>();
    //        Animator anim = other.GetComponentInChildren<Animator>();
    //        float pushForce = 8f;
    //        if (rb != null)
    //        {
    //            Vector3 dir = (other.transform.position - transform.position).normalized;
    //            dir.y = 0f;

    //            rb.AddForce(dir * pushForce, ForceMode.Impulse);
    //        }

    //        //if (anim != null)
    //        //{
    //        //    anim.SetTrigger(fallTriggerName);
    //        //}
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerKnockback knock = other.GetComponent<PlayerKnockback>();
        if (knock != null)
        {
            knock.Knockback(transform.position);
        }
    }
}
