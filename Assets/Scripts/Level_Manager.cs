using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using DG.Tweening;

public class Level_Manager : MonoBehaviour
{
    [Inject] Canvas_Manager canvas_manager;
    [Inject] Player_Controller player_controller;
    [Inject] Score_Manager score_manager;
    [Inject] Asteroid_Spawner asteroid_spawner;

    private void Awake()
    {
        DOTween.KillAll(); 
    }

    private void Start()
    {
        Time_SpeedUp();
    }

    public void Scene_Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Scene_Start()
    {
        canvas_manager.Hide_UI();
        player_controller.On_Level_Start();
        score_manager.Start_Score_Count();
        asteroid_spawner.Start_Spawner();
    }

    public void Time_SpeedDown()
    {
        Time.timeScale = 0.1f;
    }

    public void Time_SpeedUp()
    {
        Time.timeScale = 1;
    }

}