using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Game game;
    bool flag = true;
    GameObject currentViewPlayer;

    private Vector3 offset;
    private float smoothing = 5f;

    public void bind_currentViewPlayer(GameObject currentViewPlayer)
    {
        this.currentViewPlayer = currentViewPlayer;
    }

    public void bind_Game(Game game)
    {
        this.game = game;
    }
   

    public void CameraUpdate()
    {
        //if (game.start_flag && flag==true)
        //{
        //    flag = false;
        //    offset = transform.position - currentViewPlayer.transform.position;

        //}
        if (game.start_flag)
        {
            Vector3 tartgetCamPos = new Vector3(currentViewPlayer.transform.position.x, transform.position.y, currentViewPlayer.transform.position.z - 8f);//currentViewPlayer.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, tartgetCamPos, smoothing * Time.deltaTime);//tartgetCamPos;

        }

    }




}

