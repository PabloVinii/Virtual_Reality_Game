using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistol : MonoBehaviour
{
[SerializeField] private GameObject bullet;
[SerializeField] private Transform spawnPoint;
[SerializeField] private float fireRate = 20f;

void Start()
{
    XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
    grabbable.activated.AddListener(FireBullet);
}

public void FireBullet(ActivateEventArgs arg)
{
    GameObject spawnedBullet = Instantiate(bullet);
    spawnedBullet.transform.position = spawnPoint.position;
    spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireRate;
    Destroy(spawnedBullet, 5);
}
}