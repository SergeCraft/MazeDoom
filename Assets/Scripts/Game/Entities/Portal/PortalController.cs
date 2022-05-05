using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} hit {other.name}");
        GameObject.Find("GameController").GetComponent<GameController>().NextLevel();
    }
}
