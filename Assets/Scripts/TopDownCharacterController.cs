using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;
        private AudioSource audioSource;
        private Rigidbody2D rb;

        public CoinCounter coinCounter;

        private InputSystem_Actions controls;
        private Vector2 moveInput;

        private void Awake()
        {
            controls = new InputSystem_Actions();
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            controls.Enable();
            controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        private void Start()
        {
            GameManager.Reset();

            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (moveInput.x < 0)
                animator.SetInteger("Direction", 3);
            else if (moveInput.x > 0)
                animator.SetInteger("Direction", 2);
            else if (moveInput.y > 0)
                animator.SetInteger("Direction", 1);
            else if (moveInput.y < 0)
                animator.SetInteger("Direction", 0);

            animator.SetBool("IsMoving", moveInput.magnitude > 0);
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = moveInput.normalized * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Coin"))
            {
                Destroy(other.gameObject);
                audioSource.Play();
                if (coinCounter != null)
                {
                    coinCounter.AddCoin();
                }
            }
        }
    }
}
