using System.Collections;
using UnityEngine;
using Zenject;

public class Player_Shooting : MonoBehaviour
{
    [SerializeField] float shoot_cd;
    [SerializeField] Transform shoot_point;

    const string tag_obstacle = "Obstacle_asteroid"; 
    float raycast_distance = 100;
    bool can_shoot = true;

    [Inject] Pool_Of_Bullets bullets_pool;

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(shoot_point.position, Vector3.forward, out hit, raycast_distance))
        {
            if (hit.transform.CompareTag(tag_obstacle))
                Perform_Shoot();
        }
    }

    public void Perform_Shoot()
    {
        if (!can_shoot) return;

        var _bullet = bullets_pool.Get_Bulet_From_Pool();
        _bullet.transform.position = shoot_point.position;

        StartCoroutine(Wait_For_Shoot_Delay());
    }

    IEnumerator Wait_For_Shoot_Delay()
    {
        can_shoot = false;
        yield return new WaitForSeconds(shoot_cd);
        can_shoot = true;
    }
}