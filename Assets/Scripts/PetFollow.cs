using UnityEngine;

public sealed class PetFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;
    public float stopDistance = 2f;

    void Update()
    {
        //Calculate the distance between pet and player
        float distance = Vector3.Distance(transform.position, player.position);

        //Only move if the pet is further away than the stopDistance
        if (distance > stopDistance)
        {
            // Look at the player
            transform.LookAt(player);

            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }
    }
}