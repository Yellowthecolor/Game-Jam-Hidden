using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    [SerializeField] MrGoop goop;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            goop.ToggleHide();
        }
        
        Vector3 movement;
        if (goop.CheckIsHidden()){
            movement = Vector3.zero;
        } else {
            movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        goop.Move(movement.normalized);

        
    }
}
