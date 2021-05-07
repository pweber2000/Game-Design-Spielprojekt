using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController charController;
    [SerializeField]
    private float movementSpeed;
    
    //Wird benoetigt fuer die Rotationen
    [SerializeField]
    private Camera cam;
    private float xRotation;

    //Da wir ein Charactercontroller nutzen, haben wir kein Rigidbody und damit auch keine Physics
    //also hier die Variablen die wir benoetigen
    private float gravity;
    private Vector3 fallVelocity;
    private bool isGrounded;
    
    //Werden benoetigt um festzustellen ob der Player den Boden beruehrt
    [SerializeField]
    private Transform groundCollider;
    [SerializeField]
    private float checkRadius;
    public LayerMask groundMask;


    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        xRotation = 0.0f;
        Cursor.lockState = CursorLockMode.Locked;
        
        gravity = -9.81f;
        fallVelocity = new Vector3(0f, 0f, 0f);
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {

        //Horizontale und Vertikale Bewegungen auffangen
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //Bewegungen ausfuehren
        Vector3 motion = transform.right * moveX + transform.forward * moveZ;
        charController.Move(motion * movementSpeed * Time.deltaTime);

        //Mausbewegungen auffangen
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //Rotation der X-Achse berechnen
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //Rotationen ausfuehren
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up, mouseX);

        //Kollision mit Boden ueberpruefen
        isGrounded = Physics.CheckSphere(groundCollider.position, checkRadius, groundMask);

        //Wenn Boden beruehrt wird, Geschwindigkeit zuruecksetzen
        if (isGrounded)
        {
            fallVelocity.y = 0f;
        }

        //Geschwindigkeit durch Gravitaet berechnen
        fallVelocity.y += gravity * Time.deltaTime;

        //Die darausfolgende Strecke berechnen und ausfuehren
        charController.Move(fallVelocity * Time.deltaTime);
    }
}
