using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public float speed = 0.01f;
    void Update()
    {
        Vector2 position = transform.position;
        position.y = position.y + speed;
        position.x = position.x + speed;
        transform.position = position;
     }
}
