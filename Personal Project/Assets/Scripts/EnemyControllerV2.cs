using UnityEngine;

public class SimpleEnemyController : MonoBehaviour
{
    public string playerTag = "Player";
    private float movementSpeed = 1f;

    private Transform player;

    void Start()
    {
        // Find the player object using the tag
        player = GameObject.FindGameObjectWithTag(playerTag).transform;

    }

    void Update()
    {
        // Look at the player
        LookAtPlayer();

        // Move toward the player
        MoveTowardsPlayer();
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void MoveTowardsPlayer()
    {
        // Calculate the movement direction towards the player
        Vector3 moveDirection = (player.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.World);
    }
}