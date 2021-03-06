﻿using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

/** <summary>An object that handles and spawns the player paper prefab.</summary>*/
public class Paper : MonoBehaviour, IPlayerWeapon
{

    // Bodies
    public GameObject playerObject;
    public Rigidbody2D paperBody;
    public GameObject paperPrefab;

    private int ammo;
    private int maxAmmo;
    private int MAX_FIREPOWER = 10;

    private static float TIMEOUT = 3f;
    private float countdown;

    public string Name { get { return "Paper"; } }

    public int Ammo
    {
        get { return ammo; }

        set
        {
            if (value > this.maxAmmo) ammo = maxAmmo;
            else ammo = value;
        }
    }

    public int MaxAmmo
    {
        get { return this.maxAmmo; }
        set { if (value > 10) MaxAmmo = 10; }
    }

    public float Damage
    {
        get { return 0.3f; }
        set { if (value > 0.3f) Damage = 0.3f; }
    }

    /**
    * <summary>Unused</summary>
    */
    public float DealDamage { get; set; }


    public void Fire(float damagePoints = 0.3f, Constants.DamageType damageType = Constants.DamageType.Static)
    {
        this.Damage = damagePoints;

        if (ammo > 0)
        {
            // Create the paper prefab
            var paper = (GameObject)Instantiate(
                paperPrefab,
                transform.position,
                transform.rotation
            );

            // Compute the velocity to fire the paper prefab
            paper.GetComponent<Rigidbody2D>().velocity = playerObject.GetComponent<Rigidbody2D>().transform.up * MAX_FIREPOWER +
                    new Vector3(playerObject.GetComponent<Rigidbody2D>().velocity.x, playerObject.GetComponent<Rigidbody2D>().velocity.y);

            // Set prefab's damage property
            float dealDamage = .3f;
            paper.GetComponent<PaperPrefabDamage>().DealDamage = dealDamage;

            // Set the prefabs lifetime
            Destroy(paper, .75f);
            //this.Ammo--;
        }
        else
        {
            // Need More Ammo
            Debug.Log("Paper- Need More Ammo");
        }
    }

    // Use this for initialization
    void Start()
    {
        maxAmmo = 100;
        ammo = 100;
        Physics2D.IgnoreCollision(playerObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
    }

    void FixedUpdate()
    {
        transform.position = playerObject.GetComponent<Rigidbody2D>().position;
    }
}

