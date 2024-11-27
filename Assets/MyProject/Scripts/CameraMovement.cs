using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject player;
    bool followPlayer = true;

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player2");
    }

    void Update() 
    {
        if (followPlayer == true)
        {
            canFollowPlayer();
        }
    }

    public void setFollowPlayer(bool val) 
    {
        followPlayer = val;
    }

    void canFollowPlayer() 
    {
        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        this.transform.position = newPosition;
    }
}
