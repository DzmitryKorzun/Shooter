using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour, IHealth
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float JumpPower;
    [SerializeField] private float maxHealth;
    [SerializeField] private PersonUIController uIController;

    private float x;
    private float z;
    private Vector3 velocity;
    private bool isGrounded;
    private float health;

    public float MaxHealth => maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void getDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        uIController.ChangeHealthStatus(health);
        if (health == 0)
        {
            this.gameObject.SetActive(false);            
        }
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpPower * -2f * gravity);
        }
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
