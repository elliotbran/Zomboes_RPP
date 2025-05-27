using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int defaultMagazine;
    public int actualMagazine;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    void Start()
    {
        actualMagazine = defaultMagazine;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && actualMagazine > 0)//Si queremos hacer un arma automatica solo hay que quitar el "Down" de "GetButtonDown"
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
            actualMagazine--;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("Disparo");

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //falta hacer la animacion de disparo
            //Destroy(impactGO, 2f);
        }
    }

    void Reload()
    {
        if(actualMagazine != defaultMagazine)
        {
            int diff = defaultMagazine - actualMagazine;
            actualMagazine += diff;
            Debug.Log("recargando " + diff.ToString() + " balas");
        }
        
    }
}
