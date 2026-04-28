using UnityEngine;

public class EnemyMoveToAI : MonoBehaviour
{

    [SerializeField] private GameObject aiCore;
    [SerializeField] private float speed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (aiCore == null)
            return;

        // Direction toward AI
        Vector2 direction = (aiCore.transform.position - transform.position).normalized;

        // Sideways direction (perpendicular)
        Vector2 sideDirection = new Vector2(-direction.y, direction.x);

        // Wobble movement
        float wobble = Mathf.Sin(Time.time * 3f) * 0.5f;

        // Combine forward + sideways
        Vector2 finalDirection = direction + sideDirection * wobble;

        transform.position += (Vector3)(finalDirection.normalized * speed * Time.deltaTime);

    }
}
