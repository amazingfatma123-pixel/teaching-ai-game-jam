using UnityEngine;

public class ZigZagMovement : MonoBehaviour

{
    private AiCenter aiCore;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float zigzagSpeed = 7f;
    [SerializeField] private float zigzagAmount = 0.6f;

    private float offset;

    private void Start()
    {
        aiCore = AiCenter.Instance;
        offset = Random.Range(0f, 100f);
    }

    private void Update()
    {
        if (aiCore == null)
            return;

        Vector2 dir = (aiCore.transform.position - transform.position).normalized;
        Vector2 side = new Vector2(-dir.y, dir.x);

        float zig = Mathf.Sin(Time.time * zigzagSpeed + offset) * zigzagAmount;

        Vector2 finalDir = dir + side * zig;

        transform.position += (Vector3)(finalDir.normalized * speed * Time.deltaTime);
    }
}
