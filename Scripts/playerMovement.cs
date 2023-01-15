using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject groundCheck;
    public GameObject blastBox;
    public GameObject punchBox;
    public GameObject sprint;
    public LayerMask groundMask;

    private bool isGrounded;
    private bool isJumping;

    private float gravity = -9.81f;
    private float jumpHeightOffset = 0.02f;
    private float groundCheckDistance = 0.1f;
    private float vSpeed;
    private float vSpeedBase = 0.02f;

    private Vector3 direction;
    private Vector3 ourPosition;

    public float speed = 8;
    public float jumpHeight = 1f;
    public float rotationSpeed;

    [Header("Punch")]
    public float startAfter;
    public float stopAfter;
    public float punchCD;

    [Header("Ability 1")]
    [SerializeField] public bool unlock1 = false;
    public float abil1Dur;
    public float abil1CD;

    [Header("Ability 2")]
    public bool unlock2 = false;
    public float superJumpHeight = 2f;
    public float abil2CD;

    [Header("Ability 3")]
    public bool unlock3 = false;
    public float startAb3;
    public float stopAb3;
    public float abil3CD;

    [Header("Hud")]
    public GameObject inputZ;
    public GameObject inputX;
    public GameObject inputC;
    public GameObject inputV;

    private Coroutine routine, routine1, routine2, routine3;

    void Update()
    {
        // Get Input
        float hInput = Input.GetAxis("Horizontal");
        direction.z = hInput * -speed;
        float vInput = Input.GetAxis("Vertical");
        direction.x = vInput * speed;

        // jump
        if (isGrounded && Input.GetButtonDown("Jump"))
            vSpeed = jumpHeight * jumpHeightOffset;

        // Punch
        if(Input.GetButtonDown("Fire1"))
        {
            if (routine != null) 
                return;

            routine = StartCoroutine("Punch");
        }
        // Ability 1
        if (Input.GetButtonDown("Fire2"))
        {
            if (routine1 != null)
                return;

            routine1 = StartCoroutine("Ability1");
        }
        //ability2
        if (Input.GetButtonDown("Fire3"))
        {
            if (routine2 != null)
                return;

            routine2 = StartCoroutine("Ability2");
        }
        //ability 3
        if (Input.GetButtonDown("Fire4"))
        {
            if (routine3 != null)
                return;

            routine3 = StartCoroutine("Ability3");
        }
    }


    private void FixedUpdate()
    {
        // Fall
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundCheckDistance, ~groundMask);
        isJumping = vSpeed > -0.01f;

        ourPosition = transform.position;

        vSpeed += gravity * Time.deltaTime * Time.deltaTime;

        if (!isJumping)
        {
            if (isGrounded)
            {
                vSpeed = -vSpeedBase;
            }
        }

        Vector3 vMovement = new Vector3(0, vSpeed, 0);
        controller.Move(vMovement);

        if (direction == Vector3.zero) return;

        // move
        controller.Move(direction * Time.deltaTime);

        // Turn
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isJumping) vSpeed = vSpeedBase;
    }

    public void UnlockAbility(int _ability)
    {
        switch(_ability)
        {
            case 0:
                unlock1 = true;
                break;
            case 1:
                unlock2 = true;
                break;
            case 2:
                unlock3 = true;
                break;
            default:
                break;
        }
    }

    // Punch - Z
    private IEnumerator Punch()
    {
        yield return new WaitForSeconds(startAfter);
        punchBox.GetComponent<SphereCollider>().enabled = true;
        punchBox.GetComponent<MeshRenderer>().enabled = true;
        inputZ.SetActive(false);

        yield return new WaitForSeconds(stopAfter);
        punchBox.GetComponent<SphereCollider>().enabled = false;
        punchBox.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(punchCD);
        inputZ.SetActive(true);
        routine = null;
    }

    // Ability 1 - X
    private IEnumerator Ability1()
    {
        if (unlock1)
        {
            speed = speed + 4;
            sprint.GetComponent<MeshRenderer>().enabled = true;

            inputX.SetActive(false);

            yield return new WaitForSeconds(abil1Dur);
            sprint.GetComponent<MeshRenderer>().enabled = false;
            speed = speed - 4;

            yield return new WaitForSeconds(abil1CD);
            inputX.SetActive(true);
            routine1 = null;
        }
        else if (!unlock2)
        {
            routine1 = null;
        }
    }

    // Ability 2 - C
    private IEnumerator Ability2()
    {
        if(unlock2 && isGrounded) {
            vSpeed = superJumpHeight * jumpHeightOffset;
            inputC.SetActive(false);

            yield return new WaitForSeconds(abil2CD);
            inputC.SetActive(true);

            routine2 = null;
        }
        else if(!unlock2)
        {
            routine2 = null;
        }
    }

    // Ability 3 - V
    private IEnumerator Ability3()
    {
        if (unlock3)
        {
            yield return new WaitForSeconds(startAb3);
            blastBox.GetComponent<SphereCollider>().enabled = true;
            blastBox.GetComponent<MeshRenderer>().enabled = true;
            inputV.SetActive(false);

            yield return new WaitForSeconds(stopAb3);
            blastBox.GetComponent<SphereCollider>().enabled = false;
            blastBox.GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(abil3CD);
            inputV.SetActive(true);
            routine3 = null;
        }
        else if(!unlock3) {
            routine3 = null;
        }
    }
}

