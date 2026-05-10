using UnityEngine;


public class ZigZagMovement : IMovementStrategy
{
    private float timer;
    private readonly float amplitude;
    private readonly float frequency;

    public ZigZagMovement(float amplitude = 2f, float frequency = 3f)
    {
        this.amplitude = amplitude;
        this.frequency = frequency;
    }

    public void Move(Transform self, Transform target, float speed, float deltaTime)
    {
        if (target == null) return;

        timer += deltaTime;
        Vector3 forward = (target.position - self.position).normalized;
        Vector3 sideways = new Vector3(-forward.y, forward.x, 0);  // 90° rotated
        float wave = Mathf.Sin(timer * frequency) * amplitude;

        self.position += (forward * speed + sideways * wave) * deltaTime;
    }
}
