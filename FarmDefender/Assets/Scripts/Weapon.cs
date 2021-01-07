using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    bool allowShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && allowShoot)
        {
            StartCoroutine(ShootTImer());
        }
    }
    IEnumerator ShootTImer()
    {
        Shoot();
        allowShoot = false;
        yield return new WaitForSeconds(0.3f);
        allowShoot = true;
    }

    void Shoot() {
        // shooting logic
        Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
    }
}
