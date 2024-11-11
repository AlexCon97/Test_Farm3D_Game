using PrototypeLogic.Game_Manager;
using Unity.VisualScripting;
using UnityEngine;

public class ChickenCharacter : Actor, IUpdateble
{
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private float walkSpeed=10;
    [SerializeField] private float rotateSpeed=10;

    private AnimalState animalState=AnimalState.WALK;
    private float stopDistance=0.1f;
    private int waypointIndex=0;
    private float hungryPercent=100;
    private float hungrySpeed=1;
    private int foodPointIndex=0;
    private float stopTimerValue=5;
    private float stopTimer=0;
    private GameLevelStartup startup;

    private void Awake(){
        startup=GameLevelStartup.Instance;
        transform.position=startPoint;
        waypointIndex=Random.Range(0,startup.ChickenWayPoints.Count);
        hungrySpeed=Random.Range(1,5);
        GameManager.Instance.AddUpdatableItem(this);
    }

    public void MyUpdate()
    {
        switch(animalState){
            case AnimalState.WALK:
                WalkLogic();
                break;
            case AnimalState.EAT:
                EatLogic();
                break;
        }
        
    }

    private void EatLogic()
    {
        
        if(Vector3.Distance(transform.position, startup.ChickenFoodPoints[foodPointIndex])<=stopDistance){
            if(hungryPercent>=100) animalState=AnimalState.WALK;
            hungryPercent+=Time.deltaTime*hungrySpeed;
        }
        else{
            transform.position=Vector3.MoveTowards(transform.position,startup.ChickenFoodPoints[foodPointIndex],walkSpeed*Time.deltaTime);
        }
        Debug.Log("EAT");
    }

    private void WalkLogic(){
        
        Vector3 targetDirection;
        float singleStep;
        Vector3 newDirection;

        if(Vector3.Distance(transform.position, startup.ChickenWayPoints[waypointIndex])<=stopDistance){
            if(stopTimer>0) {
                stopTimer-=Time.deltaTime;
                return;
            }
            waypointIndex=Random.Range(0,startup.ChickenWayPoints.Count);
            stopTimer=stopTimerValue;
            Debug.Log("WALK CHANGE");
        }
        targetDirection=startup.ChickenWayPoints[waypointIndex]-transform.position;
        singleStep=rotateSpeed*Time.deltaTime;
        newDirection=Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.localRotation = Quaternion.LookRotation(newDirection);
        
        transform.position=Vector3.MoveTowards(transform.position,startup.ChickenWayPoints[waypointIndex],walkSpeed*Time.deltaTime);
        hungryPercent-=Time.deltaTime*hungrySpeed;
        if(hungryPercent<=10){
            foodPointIndex=Random.Range(0,startup.ChickenFoodPoints.Count);
            animalState=AnimalState.EAT;
        }
        Debug.Log("WALK");
    }
    private void OnDestroy(){
        GameManager.Instance.RemoveUpdatableItem(this);
    }
}

public enum AnimalState{
    WALK,
    EAT,
    DRINK
}