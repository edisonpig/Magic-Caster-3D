using UnityEngine;
using System.Collections;

public class csParticleMove : MonoBehaviour
{
    public float speed = 0.1f;
    bool collision;

    void Update()
    {

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, transform.lossyScale.x / 2, transform.forward, out hit, 0.5f + speed * Time.deltaTime))
        {
            collision = true;
            Debug.Log("touched");
            Destroy(gameObject, 1f);
        }
        else collision = false;
        if (!collision)
        {
            transform.Translate(Vector3.back * speed);
        }

    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(gameObject.name + " touched");
    }
}
