﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Tool
{


    static public void printFrameMsgList(string text, Frame frame)
    {
        string str = "Tool " + text + " frame_count = " + frame.syncFrame.frame_count;
        foreach (CustomSyncMsg msg in frame.syncFrame.msg_list)
        {

            if (msg.msg_type == (int)RequestType.INPUT)
            {
                InputMessage input = msg as InputMessage;


                str += " msg_type = INPUT" + "input.moving_x = " + input.moving_x + "input.moving_z = " + input.moving_z;
            }


        }

        Debug.Log(str);

    }

    static public void printMsgList(string text, List<CustomSyncMsg> msg_list)
    {
        string str = "Tool " + text;
       
        if (msg_list==null)
        {
            str+=" msg_list is empty";
          
        }
        else
        {
            foreach (CustomSyncMsg msg in msg_list)
            {

                if (msg.msg_type == (int)RequestType.INPUT)
                {
                    InputMessage input = msg as InputMessage;


                    str += " msg_type = INPUT" + "input.moving_x = " + input.moving_x + "input.moving_z = " + input.moving_z+ "input.moving_y="+input.moving_y;
                }


            }
        }
        Debug.Log(str);
    }
    static public void printExecueQue_MsgList(string text, List<SyncFrame> syn_list)
    {
        string str = "Tool " + text + " frame_count = " + syn_list[0].frame_count; 

       foreach(SyncFrame syncFrame in syn_list)
        {
            if (syncFrame.msg_list == null)
            {
                str += " |msg_list_is_empty| ";
                
            }
            else
            {
                foreach (CustomSyncMsg msg in syncFrame.msg_list)
                {

                    if (msg.msg_type == (int)RequestType.INPUT)
                    {
                        InputMessage input = msg as InputMessage;


                        str += " |msg_type = INPUT" + "moving_x = " + input.moving_x + "moving_z = " + input.moving_z +"clientID = "+msg.player_id+"|";
                    }


                }
            }
            
        }
        Debug.Log(str);
    }
    static public void Print(string text)
    {
        Debug.Log("Tool" + text);
    }




}

