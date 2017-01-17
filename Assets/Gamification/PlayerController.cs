using UnityEngine;
 
public partial class PlayerController : MonoBehaviour { 
    public GameObject figure;
    void Update() {
        if (Input.GetKey(KeyCode.A)){
            figure.transform.position += new Vector3(-1.1f, 0.0f, 0.0f);
            Debug.Log("A Pressed" + this.GetInstanceID());
        }
        if (Input.GetKey(KeyCode.W))
            figure.transform.position += new Vector3(0.0f, 1.1f, 0.0f);

        if (Input.GetKey(KeyCode.S))
            figure.transform.position += new Vector3(0.0f, -1.1f, 0.0f);

        if (Input.GetKey(KeyCode.D))
            figure.transform.position += new Vector3(1.1f, 0.0f, 0.0f);
    }
}