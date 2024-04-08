using UnityEngine;
using DG.Tweening;
using Zenject;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float turn_speed;

    [SerializeField] Transform player_gfx;
    [SerializeField] Transform player_holder;

    [SerializeField] ParticleSystem explo_vfx;

    [Inject] Canvas_Manager canvas_manager;
    [Inject] Level_Manager level_manager;
    [Inject] Score_Manager score_manger;
    
    bool is_dead = false;
    float anim_time = 1;
    float clam_x_axis = 25;

    private void Update()
    {
        if (is_dead) return;



#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.D)) Move_To_Side(1);
        if (Input.GetKey(KeyCode.A)) Move_To_Side(-1);
#endif

#if UNITY_IOS
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Stationary)
                {
                    if (touch.position.x < Screen.width / 2)
                        Move_To_Side(-1);
                    else
                        Move_To_Side(1);
                }
            }
        }
#endif

        float clampedPositionX = Mathf.Clamp(player_holder.transform.position.x, -clam_x_axis, clam_x_axis);
        player_holder.transform.position = new Vector3(clampedPositionX, player_holder.transform.position.y, player_holder.transform.position.z);
    }

    public void On_Level_Start()
    {
        player_holder.DOMove(Vector3.zero, anim_time);
    }

    public void Move_To_Side(int direction) // -1-L side | 1-R side
    {
        if (direction == 1)
            player_holder.Translate(Vector3.right * turn_speed * Time.fixedDeltaTime);

        if (direction == -1)
            player_holder.Translate(-Vector3.right * turn_speed * Time.fixedDeltaTime);

        if (direction == 0)
            player_holder.Translate(Vector3.zero * 0 * Time.fixedDeltaTime);
    }

    public void Deduct_Health(float amount)
    {
        if (is_dead) return;

        health -= amount;

        if (health <= 0) Death();
    }

    void Death()
    {
        is_dead = true;

        explo_vfx.Play();
        player_gfx.gameObject.SetActive(false);

        level_manager.Time_SpeedDown();
        canvas_manager.Show_Over_UI();
        score_manger.Save_Score();
    }
}