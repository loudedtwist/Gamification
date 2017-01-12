using UnityEngine;
 
public class PlayerController : MonoBehaviour { 
    void Update() {
        if (Input.GetKey(KeyCode.A))
            gameObject.transform.position += new Vector3(-0.1f, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.W))
            gameObject.transform.position += new Vector3(0.0f, 0.1f, 0.0f);

        if (Input.GetKey(KeyCode.S))
            gameObject.transform.position += new Vector3(0.0f, -0.1f, 0.0f);

        if (Input.GetKey(KeyCode.D))
            gameObject.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
    }
}