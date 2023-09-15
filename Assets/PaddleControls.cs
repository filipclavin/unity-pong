using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleControls : MonoBehaviour
{
    [SerializeField] private InputAction direction;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float topY;
    [SerializeField] private float botY;
    
    // Start is called before the first frame update
    void Start()
    {
        direction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.ReadValue<float>() != 0)
        {
            Move();
        }
    }
    
    void Move()
    {
        Vector2 pos = transform.position;

        float newY = pos.y + direction.ReadValue<float>() * movementSpeed * Time.deltaTime;
        
        if (newY > topY)
        {
            newY = topY;
        }
        
        if (newY < botY)
        {
            newY = botY;
        }
        
        transform.position = new Vector2(pos.x, newY);
    }
    
}
