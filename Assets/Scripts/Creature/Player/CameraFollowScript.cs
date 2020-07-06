using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform player;
    public float smoothing;
    public Vector3 offest;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position,player.transform.position+offest, smoothing);
        transform.position = newPosition;

        gameObject.transform.parent.GetComponent<WorldState>().LogicGeneration(gameObject.transform.position);
    }
}
