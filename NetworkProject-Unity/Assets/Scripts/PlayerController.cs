using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Properties
    float walkSpeed = 5.0f;
    float rotateSpeed = 100.0f;
    bool onGround = false;
    // Components
    Rigidbody rigidbody;

    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Walk
        var translation = Input.GetAxis("Vertical") * this.walkSpeed * Time.deltaTime;
        transform.Translate(0, 0, translation);
        // Rotate
        var rotation = Input.GetAxis("Horizontal") * this.rotateSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && this.onGround)
        {
            this.rigidbody.AddForce( 0, 5, 0, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Floor")
            this.onGround = true;
    }

    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Floor")
            this.onGround = false;
    }
}
