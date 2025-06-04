using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int defaultMagazine;
    public int actualMagazine;
    public int inventoryAmmunition;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject ammunitionHud;
    public GameObject muzzleFlashParticle;
    public Transform muzzlePoint;

    public AudioSource vfxPlayer;
    public AudioClip gunShot;

    private float nextTimeToFire = 0f;

    void Start()
    {
        actualMagazine = defaultMagazine;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && actualMagazine > 0)//Si queremos hacer un arma automatica solo hay que quitar el "Down" de "GetButtonDown"
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            actualMagazine--;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        ammunitionHud.gameObject.GetComponent<TextMeshProUGUI>().text = actualMagazine.ToString() + " / " + inventoryAmmunition.ToString();
    }

    void Shoot()
    {
        RaycastHit hit;

        vfxPlayer.PlayOneShot(gunShot);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("Disparo");

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (muzzleFlashParticle != null)
            {
                if (muzzlePoint != null)
                {
                    Instantiate(muzzleFlashParticle, muzzlePoint.position, muzzlePoint.rotation);
                }
                else
                {
                    Debug.LogWarning("Muzzle Point not set. Instantiating muzzle flash at camera position.");
                    Instantiate(muzzleFlashParticle, fpsCam.transform.position + fpsCam.transform.forward * 0.5f, fpsCam.transform.rotation);
                }
            }

        }
    }

    void Reload()
    {
        if (actualMagazine != defaultMagazine && inventoryAmmunition > 0)
        {
            int diff = defaultMagazine - actualMagazine;
            if (diff <= inventoryAmmunition)
            {
                actualMagazine += diff;
                inventoryAmmunition -= diff;
                Debug.Log("recargando " + diff.ToString() + " balas");
            }
            else
            {
                actualMagazine += inventoryAmmunition;
                inventoryAmmunition = 0;
            }

        }

    }
}