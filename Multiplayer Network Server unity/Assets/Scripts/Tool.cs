using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Tool
{


    static public void printFrameMsgList(string text, Frame frame)
    {
        Debug.Log("Tool "+text+" frame_count = " + frame.syncFrame.frame_count);
         foreach(CustomSyncMsg msg in frame.syncFrame.msg_list){

            if (msg.msg_type == (int)RequestType.INPUT)
            {
                InputMessage input = msg as InputMessage;


                Debug.Log("Tool msg_type = INPUT"+ "input.moving_x = " + input.moving_x + "input.moving_z = " + input.moving_z);
            }


        }
    }
    static public void Print(string text)
    {
        Debug.Log("Tool"+text);
    }




}

