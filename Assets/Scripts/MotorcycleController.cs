using UnityEngine;
using UnityEngine.SceneManagement;

public class MotorcycleController : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float maxTiltAngle;
    [SerializeField] float stabilizationCoefficient;
    [SerializeField] float accelerationSpeed;
    [SerializeField] float deAccelerationSpeed;
    
    public float speed;
    public float acceleration;
    [SerializeField] bool grounded = false;

    [SerializeField] Rigidbody bikeBody;

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        speed = inputY * maxSpeed;

        float rotationFactor = -inputX * Time.deltaTime * rotateSpeed;

        if(transform.localEulerAngles.z >= -1 && transform.localEulerAngles.z <= maxTiltAngle || transform.localEulerAngles.z <= 361 && transform.localEulerAngles.z >= 360-maxTiltAngle){
            if(speed > 0) transform.Rotate(0, -rotationFactor, rotationFactor, Space.Self);
            else transform.Rotate(0, rotationFactor, rotationFactor, Space.Self);

            if(grounded) {
                transform.position += transform.forward * speed * Time.deltaTime;
                transform.position += transform.forward * acceleration * Time.deltaTime;
            }

            if(speed > 0) acceleration += accelerationSpeed*Time.deltaTime;
            else if(speed < 0) acceleration = 0;
            if(acceleration > 0) acceleration -= deAccelerationSpeed*Time.deltaTime;
            if(acceleration > 50) acceleration = 50;
        }
        
        //Debug.Log("Rotation: " + transform.rotation + " Local Rot: " + transform.localRotation + " Local Eul: " + transform.localEulerAngles);

        if(transform.localEulerAngles.z > 210 && transform.localEulerAngles.z <= 361){
            transform.Rotate(0, 0, 1.01f*stabilizationCoefficient, Space.Self);
            //transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, transform.rotation.y, Quaternion.identity.z, 0), stabilizationCoefficient);
        }
        else if(transform.rotation.z >= -1 && transform.localEulerAngles.z < 150){
            transform.Rotate(0, 0, -1*stabilizationCoefficient, Space.Self);
            //transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, transform.rotation.y, Quaternion.identity.z, 0), stabilizationCoefficient);
        }

        if(Input.GetKeyDown(KeyCode.R)){
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y+1, transform.position.z), Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }

        bikeBody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionStay(Collision other) {
        grounded = true;
    }

    private void OnCollisionExit(Collision other) {
        grounded = false;   
    }
}