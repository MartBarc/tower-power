using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chicken : MonoBehaviour
{
    public Animator chickenAnimator;
    public Transform spawnPlace;
    public float direction = 2f;
    public bool changeDirectionBool = true;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) * Time.deltaTime;
        //transform.rotation = Quaternion.identity;
        if (changeDirectionBool)
        {
            changeDirectionBool = false;
            direction = direction * -1;
            StartCoroutine(changeDirectionCooldown());
        }
    }

    IEnumerator changeDirectionCooldown()
    {
        yield return new WaitForSecondsRealtime(1);
        changeDirectionBool = true;
    }
}
