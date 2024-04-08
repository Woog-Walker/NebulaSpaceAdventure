using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Score_Manager : MonoBehaviour
{
    [SerializeField] float current_score;
    [SerializeField] float cd_to_tick;

    [SerializeField] float score_per_tick;

    [Inject] Canvas_Manager canvas_manager;

    private bool can_change_score = true;

    private const string pref_score = "pref_score";

    private void Start()
    {
        Load_Score_OnStart();
    }

    private void Load_Score_OnStart()
    {
        if (PlayerPrefs.HasKey(pref_score))
        {
            float score_value = PlayerPrefs.GetFloat(pref_score);
            canvas_manager.UI_Repaint_HighScore(score_value);
        }
        else
            canvas_manager.UI_Repaint_HighScore(0);
    }

    public void Save_Score()
    {
        PlayerPrefs.SetFloat(pref_score, current_score);
    }

    public void Start_Score_Count()
    {
        StartCoroutine(Start_Score_Engine());
    }

    IEnumerator Start_Score_Engine()
    {
        yield return new WaitForSeconds(cd_to_tick);

        if (can_change_score)
        {
            current_score += score_per_tick;
            canvas_manager.UI_Repaint_Score_Current(current_score);
        }

        StartCoroutine(Start_Score_Engine());
    }

    public void Add_Score(float amount)
    {
        current_score += amount;

        canvas_manager.UI_Repaint_Score_Current(current_score);
    }

    public float Get_Score_Value()
    {
        return current_score;
    }
}