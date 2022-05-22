using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSparksController : MonoBehaviour
{
    private GameObject playerGO;
    private Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.Find("Player");
        playerRB = playerGO.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerGO.transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, - playerRB.velocity);
    }
}
