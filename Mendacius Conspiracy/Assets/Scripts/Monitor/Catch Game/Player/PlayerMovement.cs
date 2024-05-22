using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool player_active = false;

    // References
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        player_active = false;
    }
    private void Update()
    {
        if (player_active)
        {
            float movement_input = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(speed * movement_input, rb.velocity.y);
        }
    }
}
