using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Transform shootPoint;
    public Animator m_Animator;

    private void Start()
    {
        StartCoroutine(ShootBulletLoop());
    }

    private IEnumerator ShootBulletLoop()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSecondsRealtime(2f);
        }
    }

    private void Shoot()
    {
        transform.LookAt(GameReferenceManager.Instance.playerCenterPointTransform.position);
        Bullet bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity =
            (GameReferenceManager.Instance.playerCenterPointTransform.position - transform.position).normalized * 15;
        m_Animator.SetTrigger("Shoot");
    }
}
