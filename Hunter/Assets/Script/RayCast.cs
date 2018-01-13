using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public static Light lantern;
    public AudioClip audioClip;
    public AudioSource audioSource;

    bool enabledLantern = true;

    void Start()
    {
        lantern = FindObjectsOfType<Light>().Where(c => c.name == "FlashLight").First();
    }

    // Update is called once per frame
    void Update()
    {

        KeysCount.EnableFinishInteraction(false);

        RaycastHit hit = new RaycastHit();
        Vector3 dir = transform.TransformDirection(Vector3.forward);

        Debug.DrawRay(transform.position, dir * 10, Color.red);

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(transform.position, dir, out hit, 10f))
            {
                if (hit.collider.gameObject.tag == "Item")
                {
                    Destroy(hit.collider.gameObject);
                    GetItem();
                }
            }
        }

        if (Physics.Raycast(transform.position, dir, out hit, 5f))
        {
            if (hit.collider.gameObject.tag == "Finish")
            {
                KeysCount.EnableFinishInteraction(true);

                if (Input.GetButtonDown("Fire1"))
                {
                    KeysCount.validateFinish();
                }
            }
            else
            {
                KeysCount.EnableFinishInteraction(false);
                KeysCount.interactionInfo.gameObject.SetActive(false);
            }
        }
        else
        {
            KeysCount.interactionInfo.gameObject.SetActive(false);
        }

        if (Physics.Raycast(transform.position, dir, out hit, 15f))
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                //audio
            }
            else
            {
                //IA_BIXU.roar.gameObject.SetActive(false);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            enabledLantern = !enabledLantern;
            lantern.gameObject.SetActive(enabledLantern);
        }
    }

    private void GetItem()
    {
        KeysCount.KeyQuantity++;
        IA_BIXU.minDistance -= 5;
    }
}
