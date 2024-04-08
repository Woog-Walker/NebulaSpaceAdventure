using System.Collections;
using UnityEngine;
using Zenject;

public class Laser_Beam_Ammo : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float speed;

    [Space]
    [SerializeField] GameObject ammo_gfx;
    [SerializeField] ParticleSystem hit_vfx;

    float tmp_speed;
    const string tag_asteroid = "Obstacle_asteroid";
    const string tag_trigger_pool = "Trigger_for_bullets"; // collider trigger - on interruct - put bullets to pool

    [Inject] Pool_Of_Bullets bullets_pool;

    private void Awake()
    {
        tmp_speed = speed;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }

    void Interact_With_Obstacle(Collider other)
    {
        other.GetComponent<Asteroid_Controller>().Deduct_Health(damage);

        speed = 0;
        ammo_gfx.gameObject.SetActive(false);

        hit_vfx.Play();

        StartCoroutine(Wait_Before_Back_To_Pool());
    }

    IEnumerator Wait_Before_Back_To_Pool()
    {
        yield return new WaitForSeconds(1);

        bullets_pool.Put_Bullet_To_Pool(transform.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag_asteroid))
            Interact_With_Obstacle(other);

        if (other.CompareTag(tag_trigger_pool)) bullets_pool.Put_Bullet_To_Pool(other.gameObject);

    }

    private void OnEnable()
    {
        ammo_gfx.gameObject.SetActive(true);

        speed = tmp_speed;
    }
}