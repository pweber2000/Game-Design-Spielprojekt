using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController charController;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float sprintspeedmax;
    private float sprintspeed;
    [SerializeField]
    private float jumpForce;

    //Wird benoetigt fuer die Rotationen
    [SerializeField]
    private Camera cam;
    private float xRotation;

    //Da wir ein Charactercontroller nutzen, haben wir kein Rigidbody und damit auch keine Physics
    //also hier die Variablen die wir benoetigen
    private float gravity;
    private Vector3 fallVelocity;

    // Start is called before the first frame update
    void Start()
    {
        sprintspeed = 1.0f;
        charController = GetComponent<CharacterController>();
        xRotation = 0.0f;
        Cursor.lockState = CursorLockMode.Locked;
        
        gravity = -9.81f;
        fallVelocity = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Geschwindigkeit durch Gravitaet berechnen
        fallVelocity.y += gravity * Time.deltaTime * 2 *1.5f;

        //Wenn Boden beruehrt wird, Geschwindigkeit zuruecksetzen
        if (charController.isGrounded)
        {
            fallVelocity.y = -2f;
        }

        //Ueberpruefen ob gesprintet werden soll und den Wert erhoehen oder zuruecksetzen
        if (Input.GetKey(KeyCode.LeftShift) && sprintspeed < sprintspeedmax)
        {
            sprintspeed += 0.01f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprintspeed = 1.0f;
        }

        //Ueberpruefen ob gesprungen werden soll und die y-koordinate berechnen
        if (Input.GetButtonDown("Jump") && charController.isGrounded)
        {
            fallVelocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }

        //Horizontale und Vertikale Bewegungen auffangen
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //Bewegungen ausfuehren
        Vector3 motion = transform.right * moveX + transform.forward * moveZ;
        charController.Move(motion * movementSpeed * Time.deltaTime * sprintspeed);

        //Mausbewegungen auffangen
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //Rotation der X-Achse berechnen
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //Rotationen ausfuehren
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up, mouseX);

        //Die darausfolgende Strecke berechnen und ausfuehren
        charController.Move(fallVelocity * Time.deltaTime);
    }
}
