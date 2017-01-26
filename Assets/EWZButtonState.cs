using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EWZButtonState : MonoBehaviour { 

    void OnEnable(){  
        gameObject.GetComponent<Animator>().Play("Normal", -1, 0); 
    }
}
