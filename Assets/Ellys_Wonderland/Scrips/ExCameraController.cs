using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCameraController : MonoBehaviour
{
   
    
        public Transform player;
        public float heightOffset = 2f; // 카메라 높이 조절

        void LateUpdate()
        {
            if (player != null)
            {
                transform.position = new Vector3(player.position.x, player.position.y + heightOffset, transform.position.z);
            }
        }
    
}
