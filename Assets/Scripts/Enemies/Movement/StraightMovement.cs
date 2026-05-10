using UnityEngine;



public class StraightMovement : IMovementStrategy
{
    public void Move(Transform self, Transform target, float speed, float deltaTime)
    {
        if (target == null) return;
        Vector3 dir = (target.position - self.position).normalized;
        self.position += dir * speed * deltaTime;
    }
}