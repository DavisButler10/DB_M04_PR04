using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStuff : MonoBehaviour
{
    public GameObject bullet;
    Rigidbody rb;
    [SerializeField] Transform gun1;
    [SerializeField] Transform gun2;
    [SerializeField] Transform gun3;
    [SerializeField] Transform gun4;

    public void Shoot()
    {
        GameObject bulletObject = Instantiate(bullet, gun1.transform.position, Quaternion.identity);
        rb = bulletObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.right * 100);
        bulletObject = Instantiate(bullet, gun2.transform.position, Quaternion.identity);
        rb = bulletObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * -100);
        bulletObject = Instantiate(bullet, gun3.transform.position, Quaternion.identity);
        rb = bulletObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.right * -100);
        bulletObject = Instantiate(bullet, gun4.transform.position, Quaternion.identity);
        rb = bulletObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 100);
    }
}
