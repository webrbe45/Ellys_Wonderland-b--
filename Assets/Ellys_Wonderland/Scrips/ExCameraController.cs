using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCameraController : MonoBehaviour
{
   
    
        public Transform player;
        public float heightOffset = 2f; // ī�޶� ���� ����

        void LateUpdate()
        {
            if (player != null)
            {
                transform.position = new Vector3(player.position.x, player.position.y + heightOffset, transform.position.z);
            }
        }
    
}
