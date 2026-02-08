using UnityEngine;

public class Guillotine : BaseObstacle
{
    protected override float GetRollDir()
    {
        return 1;
    }

    protected override Vector3 GetRotateAxis()
    {
        return Vector3.up;
    }
}
