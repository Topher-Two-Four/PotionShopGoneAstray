using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 angularVelocity;
    public Space space = Space.Self;

    void Update()
    {
        transform.Rotate(angularVelocity * Time.deltaTime, space);
    }

    private void OnColliderStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            {
                GameManager.Instance.RotatePlayer(angularVelocity);
                Debug.Log(other);
            }

        }
    }
}
