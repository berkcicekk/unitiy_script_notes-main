using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Metrics")]
    public float damp;
    [Range(1, 20)]
    public float rotationSpeed;
    float normalFov;
    public float sprintFov;

    float inputX;
    float inputY;
    float maxSpeed;

    public Transform Model;

    Animator Anim;
    Vector3 StickDirection;
    Camera mainCamera;

    // Yavaþ ve Hýzlý koþma tuþ atamasý
    public KeyCode SprintButton = KeyCode.LeftShift;
    public KeyCode WalkButton = KeyCode.LeftAlt;

    void Start()
    {
        Anim = GetComponent<Animator>();
        mainCamera = Camera.main;
        normalFov = mainCamera.fieldOfView;
    }                                

    void LateUpdate()
    {
        InputMove();
        InputRotation();
        Movement();
    }

    void Movement()
    {
        StickDirection = new Vector3(inputX, 0, inputY);

        /*
             O   O   O          
                                (Diker) Y Ekseni
             O   M   O          (Yatay) X Ekseni
                                Tuþlara basýldýðýnda karakter "M"den "O"lara yönelecek.
             O   O   O
        */

        if (Input.GetKey(SprintButton)) // Koþuyor ise:
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, sprintFov, Time.deltaTime * 2);
            // Lerp(var olan deðer, gidilecek deðer)
            // Kameranýn açýsý deðiþtirilir

            maxSpeed = 2;
            inputX = 2 * Input.GetAxis("Horizontal"); // Yatay
            inputY = 2 * Input.GetAxis("Vertical"); // Dikey
        }
        else if (Input.GetKey(WalkButton)) // Daha yavaþ yürüyor ise:
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFov, Time.deltaTime * 2);
            // Lerp(var olan deðer, gidilecek deðer)
            // Kameranýn açýsý deðiþtirilir

            maxSpeed = 0.3f;
            inputX = Input.GetAxis("Horizontal"); // Yatay
            inputY = Input.GetAxis("Vertical"); // Dikey
        }
        else // Yürüyor ise:
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFov, Time.deltaTime * 2);
            // Lerp(var olan deðer, gidilecek deðer)
            // Kameranýn açýsý deðiþtirilir

            maxSpeed = 1;
            inputX = Input.GetAxis("Horizontal"); // Yatay
            inputY = Input.GetAxis("Vertical"); // Dikey
        }
    }

    void InputMove() // Hareket etme
    {
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
        // Hareketin 0 ile 1 deðeri arasýnda gidip gelmesi
        // damp = soft bir geçiþ saðlar.
    }

    void InputRotation() // Kamera-Hareket uyumu
    {
        Vector3 rotOfset = mainCamera.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Model.forward = Vector3.Slerp(Model.forward, rotOfset, Time.deltaTime * rotationSpeed);
        // Slerp =  dairsel dönüþ için

        // Kameranýn hareket etmesi sonucunda hareket vektörüleri belirlenir

        /*
        
        Ýlk bakýþ:

               (V)
            O   O   O          
                               V = Hareket yönü
            O   M   O          K = KAmera
                               
            O   O   O
               (K)

        Ýkinci bakýþ:


            O   O   O          
                               V = Hareket yönü
        (V) O   M   O (K)      K = KAmera
                               
            O   O   O
      */
    }
}
