using UnityEngine;
using DG.Tweening;
using TMPro;
using Zenject;
using Unity.VisualScripting;

public class Canvas_Manager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Transform play_btn;
    [SerializeField] Transform store_btn;
    [SerializeField] Transform settings_btn;

    [Space]    
    [Header("Pannels")]
    [SerializeField] Transform over_panel;
    [SerializeField] Transform current_score_panel;
    [SerializeField] Transform max_score_panel;
    [SerializeField] Transform reward_panel;

    [Space]
    [Header("Texts")]
    [SerializeField] TMP_Text score_current_text;
    [SerializeField] TMP_Text score_highest_text;
    [SerializeField] TMP_Text over_panel_score_text;

    float animation_time = 1;
    float animation_offset = 500;

    [Inject] Score_Manager score_manager;

    public void Hide_UI()
    {
        play_btn.transform.DOMove(new Vector3(play_btn.position.x, play_btn.position.y - animation_offset, play_btn.position.z), animation_time);
        settings_btn.transform.DOMove(new Vector3(settings_btn.position.x - animation_offset, settings_btn.position.y, settings_btn.position.z), animation_time);

        store_btn.transform.DOMove(new Vector3(store_btn.position.x + animation_offset, store_btn.position.y, store_btn.position.z), animation_time)
            .OnComplete(() =>
            {
                play_btn.gameObject.SetActive(false);
                store_btn.gameObject.SetActive(false);
                settings_btn.gameObject.SetActive(false);
            });

        current_score_panel.transform.DOScale(Vector3.one, animation_time);

        max_score_panel.transform.DOMove(new Vector3(max_score_panel.position.x, max_score_panel.position.y + animation_offset, max_score_panel.position.z), animation_time);
    }

    public void UI_Repaint_Score_Current(float tmp_score)
    {
        score_current_text.text = tmp_score.ToString();
    }

    public void Show_Over_UI()
    {
        current_score_panel.transform.DOScale(Vector3.zero, animation_time / 3);

        over_panel.transform.DOLocalMove(Vector3.zero, 0.2f);
        over_panel.transform.DOScale(Vector3.one, 0.2f);

        over_panel_score_text.text = score_manager.Get_Score_Value().ToString();
    }

    public void UI_Repaint_HighScore(float tmpAmount)
    {
        score_highest_text.text = tmpAmount.ToString();
    }

    public void Show_Reward_Panel_1()
    {
        reward_panel.gameObject.SetActive(true);

        reward_panel.localScale = Vector3.zero;

        reward_panel.DOScale(Vector3.one, animation_time);
    }

    public void Hide_Reward_Panel_1()
    {
        reward_panel.DOScale(Vector3.zero, animation_time).OnComplete(() =>
        {
            reward_panel.gameObject.SetActive(false);
        });
    }
}