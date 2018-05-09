using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ROSBridgeLib;
using System;
using SimpleJSON;
using ROSBridgeLib.geometry_msgs;
using UnityEngine.UI;
using System.Diagnostics;
namespace UnityStandardAssets.Vehicles.Car{
    [RequireComponent(typeof(CarController))]
    public class CmdVel : ROSBridgeSubscriber{
      public static ROSBridgeMsg in_msg;
      public InputField inputField;
  //    public Button button;

      string namsp = string.Concat("no",Process.GetCurrentProcess().Id);
      static bool start_flag = false;
      public static string command_topic;
      public static bool msg_flag;
      public static CarController m_Car; // the car controller we want to use
 //     GameObject car;
      //s = new Steering();
      public static string GetMessageTopic(){
	UnityEngine.Debug.LogWarning(command_topic);
	start_flag = true;
	return command_topic;
      }
      void Start(){
	if(!isLocalPlayer){
	  return;
	}
	inputField.text = string.Concat("no",Process.GetCurrentProcess().Id);
//	button.onClick.AddListener(TaskOnClick);
	msg_flag = false;
        m_Car = GetComponent<CarController>();
//	car = GameObject.Find("Car_obj(Clone)");
      }
 //     void TaskOnClick(){
//	namsp = inputField.text;
//	Debug.LogWarning("button pressed");
 //     }
      public new static string GetMessageType(){
	return "geometry_msgs/Twist";
      }
      public new static ROSBridgeMsg ParseMessage(JSONNode msg){
	return new TwistMsg(msg);
      }
      public new static void CallBack(ROSBridgeMsg msg){
//	CamRos.CallBackFromRos(cmdvel.GetLinear().GetX(), cmdvel.GetAngular().GetZ());
        msg_flag = true;
	in_msg = msg;
//	Application.Quit();
        UnityEngine.Debug.Log("Message Recieved");
	// s.UpdateCmdVel(cmdvel.linear.x, cmdvel.angular.z);
      }
      void FixedUpdate(){
	if(!isLocalPlayer){
	  return;
	}
	if(!start_flag){
	command_topic = string.Concat(inputField.text,"/cmd_vel");
	}
	if(!msg_flag){
	  return;
	}
	TwistMsg cmdvel = (TwistMsg) in_msg;
	//m_Car.Move((float)(-1*cmdvel.GetAngular().GetZ()), (float)(cmdvel.GetLinear().GetX()),(float)(cmdvel.GetLinear().GetX()), 0f);
	m_Car.Move((float)(-1*cmdvel.GetAngular().GetZ()), 0.3f,0.3f, 0f);
//	m_Car.Move((float)(cmdvel.GetAngular().GetZ()),(float)0.1 ,(float)0.1, 0f);
      }
    }
}
