using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_cont : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private Cam_Control cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
                cam.Move_To_Newroom(nextRoom);
            else
                cam.Move_To_Newroom(previousRoom);

        }
    }

}
