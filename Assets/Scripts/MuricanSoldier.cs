using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuricanSoldier : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed = 50;
    Vector3 movement = Vector3.zero;
    
    [Header("Animation State")]
    [SerializeField] string walk = "Walk";
    [SerializeField] string idle = "Idle";
    string currentAnimationState = "Idle";

    [Header("Helpers")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AnimationStateChanger animationStateChanger;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] LayerMask enemyLayer;

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
            transform.localScale = new Vector3(3,3,3);
        } else {
            transform.localScale = new Vector3(-3,3,3);
        }
    }

    public void Move(Vector3 newMovement){

        movement = newMovement;
        if (movement != Vector3.zero){
            currentAnimationState = walk;
            animationStateChanger.TriggerAnimation(walk);
        } else {
            currentAnimationState = idle;
            animationStateChanger.TriggerAnimation(idle);
        }

    }

}