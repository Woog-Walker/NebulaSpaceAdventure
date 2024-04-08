using System.Collections;
using UnityEngine;
using Zenject;

public class Asteroid_Spawner : MonoBehaviour
{
    [SerializeField] Vector3 spawn_position;
    [SerializeField] float clamp_spawn_x_axis;
    [SerializeField] Transform player_holder;
    [SerializeField] float time_cd;

    float offet_z_axis = 130;

    [Inject] Pool_Of_Asteroids pool_asteroids;


    public void Start_Spawner()
    {
        StartCoroutine(Spawner_Engine());
    }

    IEnumerator Spawner_Engine()
    {
        yield return new WaitForSeconds(time_cd);

        var _asteroid = pool_asteroids.Get_Asteroid_From_Pool();
        float x_axis = Random.Range(-clamp_spawn_x_axis, clamp_spawn_x_axis);

        _asteroid.transform.position = new Vector3(x_axis, spawn_position.y, offet_z_axis);

        StartCoroutine(Spawner_Engine());
    }
}