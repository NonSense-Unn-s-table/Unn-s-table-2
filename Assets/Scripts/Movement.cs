using UnityEngine;

public class Movement : MonoBehaviour
{
    public float acceleration = 20f;
    public float maxSpeed = 10f;
    public float friction = 10f; 

    private Vector2 velocity = Vector2.zero;

    void Update()
    {
        // Get input (-1 to 1)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 inputDir = new Vector2(x, y).normalized;

        if (inputDir.sqrMagnitude > 0f)
        {
            velocity += inputDir * acceleration * Time.deltaTime;
        }
        else
        {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, friction * Time.deltaTime);
        }

        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        transform.position += new Vector3(velocity.x, 0f, velocity.y) * Time.deltaTime;
    }
}