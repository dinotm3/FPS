using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletHole;

    private float speed = 50f;
    private float timeToDestroy = 3f;

    public Vector3 target { get; set; }
    public bool hit { get; set; }
    public Inventory inventory;

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
        //inventory = GameObject.Find("Player").GetComponent<Inventory>();

    }

    // Switch with pooling
    void Update()
    {
        // add layermask as last argument in MoveTowards to stop bullets from colliding with everything
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // In case we miss (shoot in air)
        if (!hit && Vector3.Distance(transform.position, target) < .01f) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name);
        ContactPoint contact = collision.GetContact(0);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<EnemyManager>().TakeHit(GameObject.Find("Player").GetComponent<Inventory>().GetWeaponDamage());
            collision.gameObject.GetComponent<EnemyManager>().CounterAttack(GameObject.Find("Player"));
        } 
        else if (collision.gameObject.CompareTag("Pet")) 
        { 
            Destroy(gameObject);
        }
        else
        {
            GameObject.Instantiate(bulletHole, contact.point + contact.normal * .0001f, Quaternion.LookRotation(contact.normal));
            Destroy(gameObject);
        }
    }
}
