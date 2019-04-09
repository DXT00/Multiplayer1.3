using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    Player currentPlayer;
    public GameObject player_prefabs;//ViewManager从prefabs中spawn players!!
    public Dictionary<int, ViewPlayer> viewPlayers = new Dictionary<int, ViewPlayer>();//存储localPlayer在本地模拟的其他viewPlayers  (clientID--->ViewPlayer)
    Vector3 init = new Vector3(-74, 2, 37);
    public float distBetweenViewplayers =0.1f;

    public GameObject spawn_view_player(int clientID)
    {
        GameObject instance = null;

        if (player_prefabs != null)
        {
            Vector3 initPos = new Vector3(init.x + clientID * distBetweenViewplayers, init.y, init.z );
            transform.eulerAngles = new Vector3(0, -90, 0);//注意不能直接设置transform.rotation！！rotation是四元数

            instance = Instantiate(player_prefabs, initPos, transform.rotation) as GameObject;

           
        }
        else
            Debug.Log("player_prefabs is null !");
        return instance;
    }
    public void bind_currentPlayer(Player currentPlayer)
    {
        this.currentPlayer = currentPlayer;
    }

    public ViewPlayer generate_other_viewPlayer(int clientID)
    {
        GameObject instance = spawn_view_player( clientID);
        ViewPlayer v_player = instance.GetComponent<ViewPlayer>();
        viewPlayers.Add(clientID, v_player);

        return v_player;
    }

    public void execute_frames(List<SyncFrame> syncFrames)
    {

        foreach (SyncFrame syncFrame in syncFrames)
        {
            if (syncFrame.msg_list == null)
            {
                Debug.Log("executing frame----" + syncFrame.frame_count + "no msg_list");
                continue;
            }
            foreach (CustomSyncMsg msg in syncFrame.msg_list)
            {

                int clientID = msg.player_id;
                //Debug.Log("executing frame----" + syncFrame.frame_count + "clientID = " + clientID + "msg.type=" + msg.msg_type);
                ViewPlayer viewPlayer;
                if (viewPlayers.ContainsKey(clientID))
                {
                    viewPlayer = viewPlayers[clientID];
                }
                else
                {
                    viewPlayer = generate_other_viewPlayer(clientID);
                    viewPlayer.Start();
                    viewPlayer.connectID = clientID;
                }

             
                if (msg.msg_type == (int)RequestType.INPUT)
                {
                    InputMessage Input_msg = msg as InputMessage;                    
                    viewPlayer.Move(Input_msg.moving_x, Input_msg.moving_z, Input_msg.moving_y);


                    Debug.Log("executing frame----" + syncFrame.frame_count + " clientID " + clientID.ToString() + "..........is moving..."
                        +" dist_x =  "+Input_msg.moving_x*viewPlayer.get_speed()*Time.deltaTime+" dist_z= "+ Input_msg.moving_z * viewPlayer.get_speed() * Time.deltaTime
                        +"y = "+Input_msg.moving_y);
                }
                else if (msg.msg_type == (int)RequestType.ROTATE)
                {
                    RotateMessage rot_msg =  msg as RotateMessage;
                    viewPlayer.Rotate(rot_msg.delta_x, rot_msg.delta_y);
                }
                else if (msg.msg_type == (int)RequestType.SPAWN)
                {

                }
                //如果是currentPlayer -->cameraFolllow
                if(viewPlayer.connectID==currentPlayer.connectID)
                    viewPlayer.camera.CameraUpdate();



            }



        }



    }

}

