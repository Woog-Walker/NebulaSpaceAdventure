using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Trigger_Zone : MonoBehaviour
{
    // triggering zone to detect asteroids ant back it to pool
    const string tag_asteroid = "Obstacle_asteroid";

    [Inject] Pool_Of_Asteroids asteroids_pool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag_asteroid))
            asteroids_pool.Put_Asteroid_To_Pool(other.gameObject);
    }
}