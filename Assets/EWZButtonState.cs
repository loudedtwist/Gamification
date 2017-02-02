using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EWZButtonState : MonoBehaviour { 

    Animator animator;

    void Start(){ 
        animator = GetComponent<Animator>();
    }
    void OnEnable(){  
        animator.Play("Normal", -1, 0); 
    }
    public void AnswerHit(bool trueFalse){  
        animator.SetBool("AnswerTrue", trueFalse); 
        if(trueFalse){
            animator.Play("PressedTrue", -1, 0);  
        } 
    } 
}
