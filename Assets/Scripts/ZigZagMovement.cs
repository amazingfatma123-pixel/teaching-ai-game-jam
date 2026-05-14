using UnityEngine;

public class ZigZagMovement : MonoBehaviour

{
    private AiCenter aiCore;// Reference to the AiCenter, which is the target for the enemies to move towards.
    [SerializeField] private float speed = 2f;
    [SerializeField] private float zigzagSpeed = 7f;
    [SerializeField] private float zigzagAmount = 0.6f;

    private float offset;// Used to make each enemy's zigzag pattern different.

    private void Start()
    {
        aiCore = AiCenter.Instance;// Get the AiCenter from the singleton instance.
        offset = Random.Range(0f, 100f);// Makes each enemy's zigzag pattern start at a different point in the sine wave.
    }

    private void Update()
    {
        if (aiCore == null)
            return;// If there is no AiCenter assigned, stop the code here to avoid errors.

        Vector2 dir = (aiCore.transform.position - transform.position).normalized;// Get the direction toward the AiCenter. the direction toward the AI center is calculated here
        Vector2 side = new Vector2(-dir.y, dir.x);// Then the sideways direction is created here by swapping the x and y components of the direction and negating one of them. This gives a perpendicular direction that is used for the zigzag movement.

        float zig = Mathf.Sign(Mathf.Sin(Time.time * zigzagSpeed + offset)) * zigzagAmount;// Calculate the zigzag offset using a sine wave. Time.time makes it change over time, and offset makes each enemy's zigzag different. Mathf.Sign makes it switch between positive and negative to create a zigzag pattern.

        Vector2 finalDir = dir + side * zig;// Combine the direction toward the AiCenter, with the zigzag offset to get the final movement direction.

        transform.position += (Vector3)(finalDir.normalized * speed * Time.deltaTime);// Move the enemy in the final direction. Time.deltaTime makes it smooth and frame rate independent.
    }
}
