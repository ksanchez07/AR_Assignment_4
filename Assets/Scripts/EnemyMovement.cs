using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;

    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float stopDistance = 0.2f;

    void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            return;
        }

        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;

        Vector3 direction = targetPos - transform.position;
        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }
}