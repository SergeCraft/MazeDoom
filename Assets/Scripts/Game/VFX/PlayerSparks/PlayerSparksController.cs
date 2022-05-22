using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSparksController : MonoBehaviour
{
    private GameObject playerGO;
    private Rigidbody playerRB;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.Find("Player");
        playerRB = playerGO.GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerGO.transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, - playerRB.velocity);
    }

    public void BurstAtCollision (Collision collision)
    {
        transform.position = collision.GetContact(0).point;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, collision.impulse);
        ps.Emit(30);
    }
}
