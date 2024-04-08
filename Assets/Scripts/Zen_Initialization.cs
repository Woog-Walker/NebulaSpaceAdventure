using UnityEngine;
using Zenject;

public class Zen_Initialization : MonoInstaller
{
    [SerializeField] private Player_Controller player_controller;
    [SerializeField] private Asteroid_Spawner asteroid_spawner;

    [SerializeField] private Score_Manager score_manager;
    [SerializeField] private Level_Manager level_manager;
    [SerializeField] private Canvas_Manager canvas_manger;

    [SerializeField] private Pool_Of_Asteroids pool_asteroids;
    [SerializeField] private Pool_Of_Bullets pool_bullets;

    public override void InstallBindings()      
    {
        Container.Bind<Player_Controller>().FromInstance(player_controller);
        Container.Bind<Asteroid_Spawner>().FromInstance(asteroid_spawner);

        Container.Bind<Level_Manager>().FromInstance(level_manager);
        Container.Bind<Canvas_Manager>().FromInstance(canvas_manger);
        Container.Bind<Score_Manager>().FromInstance(score_manager);

        Container.Bind<Pool_Of_Asteroids>().FromInstance(pool_asteroids);
        Container.Bind<Pool_Of_Bullets>().FromInstance(pool_bullets);
    }
}