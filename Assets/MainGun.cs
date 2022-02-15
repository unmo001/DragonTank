using System.Collections;
using UnityEngine;
public class MainGun : MonoBehaviour
{
    [SerializeField]
    Transform bulletSpawn = null;
    [SerializeField, Min(1)]
    int damage = 1;
    [SerializeField, Min(1)]
    int maxAmmo = 30;
    [SerializeField, Min(1)]
    float maxRange = 30;
    [SerializeField]
    LayerMask hitLayers = 0;
    [SerializeField, Min(0.01f)]
    float fireInterval = 0.1f;


    [SerializeField]
    ParticleSystem muzzleFlashParticle = null;
    [SerializeField]
    GameObject bulletHitEffectPrefab = null;


    bool fireTimerIsActive = false;
    RaycastHit hit;
    WaitForSeconds fireIntervalWait;
    void Start()
    {
        fireIntervalWait = new WaitForSeconds(fireInterval);  // WaitForSecondsをキャッシュしておく（高速化）
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }
    // 弾の発射処理
    void Fire()
    {
        if (fireTimerIsActive)
        {
            return;
        }
        muzzleFlashParticle.Play();

        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, maxRange, hitLayers, QueryTriggerInteraction.Ignore))
        {
            BulletHit();
        }
        StartCoroutine(nameof(FireTimer));
    }
    // 弾がヒットしたときの処理
    void BulletHit()
    {
        Instantiate(bulletHitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
    }
    // 弾を発射する間隔を制御するタイマー
    IEnumerator FireTimer()
    {
        fireTimerIsActive = true;
        yield return fireIntervalWait;
        fireTimerIsActive = false;
    }
}