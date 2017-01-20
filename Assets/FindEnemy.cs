using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindEnemy : MonoBehaviour
{
    public Text question;

    private void Update()
    {
        GameObject myObj = GameObject.FindGameObjectWithTag("Question");
        if (myObj != null)
        {
            var question = myObj.GetComponent<Question>();
            if (question == null) return;
            this.question.text = question.question1;
        }
    }
}
