using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseManager : MonoBehaviour
{
    public float hitPoints = 100f;
    public float maxHitPoints = 100f;
    public HealthBar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = maxHitPoints;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        //healthbar.Slider.gameObject.SetActive(true);
    }

    public void TakeHit(float damage)
    {
        hitPoints -= damage;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
