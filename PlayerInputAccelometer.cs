using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
 
    class PlayerInputAccelometer:MonoBehaviour
    {
        packmanController player;
        
        void Start()
        {
            player = GetComponent<packmanController>();
            player.currentAcceleration = Input.acceleration.y;
        }

        void Update()
        {
            float x = Input.acceleration.x;
            //float z = Input.acceleration.z;
            float y = Input.acceleration.y;

            if (player.currentDirection != player.down)
            {

                if (y > player.currentAcceleration && (x < 0.04f || x > -0.04f))
                    player.currentDirection = player.up;

                if (x > 0.06f)
                {
                    player.currentDirection = player.right;
                    y = player.currentAcceleration;
                }


                if (x < -0.06f)
                {
                    player.currentDirection = player.left;
                    y = player.currentAcceleration;
                }


                if (y < player.currentAcceleration - 0.02f && (x < 0.06f || x > -0.06f))
                    player.currentDirection = player.down;


            }
            if (player.currentDirection != player.up)
            {
                if (y > player.currentAcceleration + 0.02f && (x < 0.06f || x > -0.06f))
                    player.currentDirection = player.up;
                if (x > 0.06f)
                {
                    player.currentDirection = player.right;
                    y = player.currentAcceleration;
                }

                if (x < -0.06f)
                {
                    player.currentDirection = player.left;
                    y = player.currentAcceleration;
                }

                if (y < player.currentAcceleration && (x < 0.06f || x > -0.06f))
                    player.currentDirection = player.down;
            }
        }
    }
}
