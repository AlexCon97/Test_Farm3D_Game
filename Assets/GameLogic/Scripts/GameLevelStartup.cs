using UnityEngine;
using PrototypeLogic.UI_Manager;
using PrototypeLogic.Task_Manager;
using PrototypeLogic.Game_Manager;
using System.Collections.Generic;

public class GameLevelStartup : MonoBehaviour
{
    [SerializeField] private ChickenCharacter chickenPrefab;
    [SerializeField] private int chickenAmount;
    private WayPoint[] wayPoints;
    public List<Vector3> ChickenWayPoints{get;private set;}
    public List<Vector3> ChickenFoodPoints{get;private set;}
    public static GameLevelStartup Instance;

    private void Awake()
    {
        if(Instance!=null) return;
        Instance=this;
        ChickenWayPoints = new List<Vector3>();
        ChickenFoodPoints = new List<Vector3>();

        wayPoints=FindObjectsByType<WayPoint>(FindObjectsSortMode.None);
        foreach(var waypoint in wayPoints){
            switch(waypoint.pointType){
                case PointType.CHICKEN_FOOD: 
                    ChickenFoodPoints.Add(waypoint.transform.position);
                    break;
                case PointType.CHICKEN_WAY: 
                    ChickenWayPoints.Add(waypoint.transform.position);
                    break;
            }
        }

        for(int i=0;i<chickenAmount;i++){
            Instantiate(chickenPrefab);
        }
        //TaskManager.ShowOrInitializeTaskTrigger(5);
        HUD.Active();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     TaskManager.ReloadTask();
        //     //UIManager.Show(WindowTypes.GamePause);
        // }
    }
}
