//using System.Collections;
//using System.Collections.Generic;
//using System.Globalization;
//using Unity.VisualScripting;
//using UnityEngine;

//public class ShipCombat : MonoBehaviour
//{
//    public GameObject projectilePrefab;
//    public Transform projectileSpawnPointLeft;
//    public Transform projectileSpawnPointRight;

//    public KeyCode shootKey = KeyCode.Space;
//    public KeyCode shootLeftKey = KeyCode.Q;
//    public KeyCode shootRightKey = KeyCode.E;

//    public float fireSpeed;
//    public float projectileLifetime = 5f;
//    public bool fireLeft = false;
//    public bool fireRight = false;
//    public float fireRate = 0.5f;
//    public bool canFire = true;
//    public float arcForce = 5f;

//    void Update()
//    {
//        CombatManager();
//    }

//    private void CombatManager()
//    {
//        if (Input.GetKeyDown(shootLeftKey))
//        {
//            fireLeft = !fireLeft;
//        }

//        if (Input.GetKeyDown(shootRightKey))
//        {
//            fireRight = !fireRight;
//        }

//        if (Input.GetKeyDown(shootKey) && canFire)
//        {
//            Shoot();
//            StartCoroutine(ResetFireRate());
//        }
//    }

//    private void Shoot()
//    {
//        if (fireLeft)
//        {
//            foreach (Transform child in projectileSpawnPointLeft)
//            {
//                GameObject projectile = Instantiate(projectilePrefab, child.position, child.rotation);
//                Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

//                Vector3 shootDirection = -transform.right;
//                projectileRb.velocity = shootDirection.normalized * fireSpeed;

//                projectileRb.AddForce(Vector3.up * arcForce, ForceMode.Impulse);

//                Destroy(projectile, projectileLifetime);
//            }
//        }

//        if (fireRight)
//        {
//            foreach (Transform child in projectileSpawnPointRight)
//            {
//                GameObject projectile = Instantiate(projectilePrefab, child.position, child.rotation);
//                Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

//                Vector3 shootDirection = transform.right;
//                projectileRb.velocity = shootDirection.normalized * fireSpeed;

//                projectileRb.AddForce(Vector3.up * arcForce, ForceMode.Impulse);

//                Destroy(projectile, projectileLifetime);
//            }
//        }
        
//    }
//    private System.Collections.IEnumerator ResetFireRate()
//    {
//        canFire = false;
//        yield return new WaitForSeconds(fireRate);
//        canFire = true;
//    }

//}
