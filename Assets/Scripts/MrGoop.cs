using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MrGoop : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed = 50;
    [SerializeField] bool isHidden = false;
    [SerializeField] bool isDead = false;
    [SerializeField] bool hasWon = false;
    Vector3 movement = Vector3.zero;
    
    [Header("Animation State")]
    [SerializeField] string walk = "Walk";
    [SerializeField] string idle = "Idle";
    [SerializeField] string idleHidden = "IdleHidden";
    [SerializeField] string hide = "Hide";
    [SerializeField] string unHide = "UnHide";
    string currentAnimationState = "Idle";

    [Header("Helpers")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AnimationStateChanger animationStateChanger;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask endGoal;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidBody.velocity = movement * speed * Time.fixedDeltaTime;
        if (rigidBody.velocity.x == 0){
            //Do nothing
        } else if (rigidBody.velocity.x > 0) {
            transform.localScale = new Vector3(2,2,2);
        } else {
            transform.localScale = new Vector3(-2,2,2);
        }
    }

    public void Move(Vector3 newMovement){
        if (animationStateChanger.CheckCurrentAnimationState(hide) || animationStateChanger.CheckCurrentAnimationState(unHide)){
            return;
        }

        movement = newMovement;
        if (movement != Vector3.zero){
            currentAnimationState = walk;
            animationStateChanger.TriggerAnimation(walk);
        } else if (!isHidden){
            currentAnimationState = idle;
            animationStateChanger.TriggerAnimation(idle);
        } else {
            currentAnimationState = idleHidden;
            animationStateChanger.TriggerAnimation(idleHidden);
        }
    }

    public void ToggleHide(){
        if(isHidden){
            animationStateChanger.TriggerAnimation(unHide);
        } else {
            animationStateChanger.TriggerAnimation(hide);
        }
        isHidden = !isHidden;
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Soldier") && !CheckDeathStatus() && !CheckIsHidden()){
            animationStateChanger.TriggerAnimation(hide);
            isDead = true;
        }
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EndGoal")){
            hasWon = true;
        }

        if(other.CompareTag("Soldier") && !CheckDeathStatus() && !CheckIsHidden()){
            animationStateChanger.TriggerAnimation(hide);
            isDead = true;
        }


    }


    public bool CheckIsHidden(){
        return isHidden;
    }


    public bool CheckDeathStatus(){
        return isDead;
    }

    public bool CheckWinStatus(){
        return hasWon;
    }
}
