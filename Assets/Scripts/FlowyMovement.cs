using UnityEngine;

public class FlowyMove : MonoBehaviour
{
    private AiCenter aiCore;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waveSpeed = 3f;
    [SerializeField] private float waveAmount = 0.5f;

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

        float wave = Mathf.Sin(Time.time * waveSpeed + offset) * waveAmount;

        Vector2 finalDir = dir + side * wave;

        transform.position += (Vector3)(finalDir.normalized * speed * Time.deltaTime);
    }
}
