using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] MrGoop goop;
    [SerializeField] MuricanSoldier soldier;
    [SerializeField] LayerMask walls;
    [SerializeField] Transform coneEnd;
    [SerializeField] float idleTime = 3f;
    Vector3 direction = Vector3.right;

    delegate void AIState();
    AIState currentState;

    float stateTime = 0;
    bool changeState = false;


    void Start(){
        soldier = GetComponent<MuricanSoldier>();
        ChangeState(PatrolState);
    }

    void ChangeState(AIState newState){
        currentState = newState;
        changeState = true;
    }

    void IdleState(){

        soldier.Move(Vector3.zero.normalized);

        if (stateTime >= idleTime)
        {
            direction *= -1;
            ChangeState(PatrolState);
            return;
        }
    }

    void PatrolState(){
        soldier.Move(direction.normalized);

        RaycastHit2D hit = Physics2D.Raycast(coneEnd.position, direction, 1f, walls);
        if (hit.collider != null)
        {
            ChangeState(IdleState);
            return;
        }
    }

    void AITick(){
        if(changeState){
            stateTime = 0;
            changeState = false;
        }
        currentState();
        stateTime += Time.deltaTime;
    }

    void Update(){
        AITick();
    }

}

