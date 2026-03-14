using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    //field for Rigidbody2D component
    private Rigidbody2D rb;
    private float destryY = -10f;
    private float fallSpeed = 3f;

    //A list to store all active meteors
    public static List<Meteor> ActiveMeteors = new List<Meteor>();

    private void OnEnable()
    {
        ActiveMeteors.Add(this);
    }

    private void OnDisable()
    {
        ActiveMeteors.Remove(this);
    }

    private void Awake()
    {
        //store Rigidbody2D in the field
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.linearVelocity = new Vector2(0f, -fallSpeed);
    }

    private void Update()
    {
        if (transform.position.y < destryY)
        {
            Destroy(gameObject);
        }
    }

}
