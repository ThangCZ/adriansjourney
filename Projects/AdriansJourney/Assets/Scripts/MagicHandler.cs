using UnityEngine;
using System.Collections;

public class MagicHandler : MonoBehaviour
{

    private GameObject fireball;
    private float fireballSpeed = 15f;
    private float fireballLifeSpan = 1.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1"))
        {
            if (fireball == null)
            {
                fireball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                fireball.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                fireball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Object.Destroy(fireball, fireballLifeSpan);
            }
        }

        //move the fireball forward
        if (fireball != null)
        {
            fireball.transform.Translate(Vector3.forward * fireballSpeed * Time.deltaTime);

        }
    }
}
