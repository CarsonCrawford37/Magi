using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Spell : MonoBehaviour
{
    // Gets the scriptable object
    public SpellScriptableObject SpellToCast;

    private SphereCollider myCollider;
    private Rigidbody myRigidbody;

    private void Awake()
    {    
        // Gets the sphere collider and rigidbody components from the gameobject the script is assigned to
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = SpellToCast.SpellRadius;

        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.isKinematic = true;

        // Destroys object after its lifetime span is up
        Destroy(this.gameObject, SpellToCast.Lifetime);
    }

    private void Update()
    {
        // Moves the projectile forward
        if (SpellToCast.Speed > 0) transform.Translate(Vector3.forward * SpellToCast.Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Apply spell effects to whatever we hit
        //Apply hit particle effects
        //Apply sound effects

        // If projectile hits a gameobject tagged with enemy, remove health based off of damage from spell
        if (!other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                // Damage Enemy
                HealthComponent enemyHealth = other.GetComponent<HealthComponent>();
                float damage = Random.Range(SpellToCast.MinDamageAmount, SpellToCast.MaxDamageAmount);
                enemyHealth.TakeDamage(damage);


                // Play particle effect

                // Play hit sound
            }
            Destroy(this.gameObject);

            Vector3 GameObjectsPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

            AudioSource.PlayClipAtPoint(SpellToCast.HitNoise, GameObjectsPos);

            ParticleSystem psClone = Instantiate(SpellToCast.HitParticleSystem, transform.position, transform.rotation); //Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y + 90, Quaternion.identity.z)
            Destroy(psClone.gameObject, 0.5f);
        }
    }
}