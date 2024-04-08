using System.Collections;
using UnityEngine;
using Zenject;

public class Asteroid_Controller : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float score_per_destroy;

    [Space]
    [SerializeField] float movement_speed;
    [SerializeField] float rotation_speed;

    [Space]
    [SerializeField] Transform asteroid_gfx;
    [SerializeField] ParticleSystem explo_vfx;

    [Space]
    [SerializeField] Collider sphere_collider;
    [SerializeField] Vector3 rotation_direction;

    [Inject] Player_Controller player_controller;
    [Inject] Pool_Of_Asteroids asteroids_pool;
    [Inject] Score_Manager score_manager;

    float tmp_health;
    float damage_to_player = 34;
    bool is_down = false;

    private void Awake()
    {
        tmp_health = health;
    }

    private void FixedUpdate()
    {
        asteroid_gfx.Rotate(rotation_direction * Time.fixedDeltaTime * rotation_speed);
        transform.Translate(Vector3.back * Time.fixedDeltaTime * movement_speed);
    }

    public void Deduct_Health(float amount)
    {
        health -= amount;

        if (health <= 0)
            Asteroid_Death();
    }

    public void Asteroid_Death()
    {
        if (is_down) return;

        StartCoroutine(Explosion_Delay());
    }

    IEnumerator Explosion_Delay()
    {
        is_down = true;
        explo_vfx.Play();

        sphere_collider.enabled = false;
        asteroid_gfx.gameObject.SetActive(false);

        score_manager.Add_Score(score_per_destroy);

        yield return new WaitForSeconds(1);

        asteroids_pool.Put_Asteroid_To_Pool(transform.gameObject);
    }

    private void OnEnable()
    {
        is_down = false;

        sphere_collider.enabled = true;

        health = tmp_health;

        asteroid_gfx.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_controller.Deduct_Health(damage_to_player);
            Asteroid_Death();
        }
    }
}