using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float healInterval = 1.0f; 
    private float lastHealTime = 0f; 
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            if (Time.time - lastHealTime >= healInterval)
            {
                controller.ChangeHealth(+1);
                lastHealTime = Time.time; 
            }
        }
    }
}