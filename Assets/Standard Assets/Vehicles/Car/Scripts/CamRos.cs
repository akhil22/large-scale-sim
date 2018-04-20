using System;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;
using ROSBridgeLib;
using ROSBridgeLib.geometry_msgs;
using ROSBridgeLib.sensor_msgs;
using ROSBridgeLib.std_msgs;
using System.Diagnostics;
using UnityEngine.UI;
namespace UnityStandardAssets.Vehicles.Car{
public class CamRos : NetworkBehaviour
{

        private ROSBridgeWebSocketConnection ros = null;
      //  ros.AddPublisher (typeof(Turtle1Teleop));
        public void CallBackFromRos(double v, double h){
	  UnityEngine.Debug.Log("got the callback");
	} 
        public Camera cam;
	
	[SyncVar]
	bool start_flag = false ;
	
//	[SyncVar]
	public InputField inputField;

	string namsp = "no";
	
	public Button button;
        CompressedImageMsg msg;
        private int width = 800;
        private int height = 800;
        Texture2D texture2D; 
        RenderTexture targetTexture;
        void Start(){
        ros = new ROSBridgeWebSocketConnection ("ws://localhost", 9090);
	start_flag = false;
        if(!isLocalPlayer){
	   StartCoroutine(PubPosition());
	   UnityEngine.Debug.Log("not the local player");
           cam.enabled = false;
           return;
        }
	inputField.text = "no";
	button.onClick.AddListener(TaskOnClick);
	StartCoroutine(UploadPNG());
	StartCoroutine(StartRos());
	StartCoroutine(PubOwnPosition());
	}
	void TaskOnClick(){
	  if(!isLocalPlayer){
	    return;
	  }
	  start_flag = true;
	  namsp = inputField.text;
	  UnityEngine.Debug.LogWarning("Button Pressed");
	}
   IEnumerator PubPosition(){
     string pose_topic = string.Concat("/car",Process.GetCurrentProcess().Id, "/car",netId.ToString(),"/position");
     ros.AddPublisherNew(typeof(PoseStampedMsg),pose_topic);
     ros.Connect();
     while(true){
     if(!isLocalPlayer){
    // if(start_flag){
       UnityEngine.Debug.Log("not the local player");
       UnityEngine.Debug.Log(inputField.text);
       Vector3 bar = transform.position;
       PoseStampedMsg msg = new PoseStampedMsg(new HeaderMsg(0, new TimeMsg(0,0), "cam"), new PoseMsg(new PointMsg(bar.x,bar.z,0), new QuaternionMsg(0,0,0,0)));
       UnityEngine.Debug.Log(pose_topic);
       ros.Publish(pose_topic, msg);
       yield return null;
     //  }
     }
     }
   }
   IEnumerator PubOwnPosition(){
     string pose_topic = string.Concat("/car",Process.GetCurrentProcess().Id, "/car",netId.ToString(),"/position");
     ros.AddPublisherNew(typeof(PoseStampedMsg),pose_topic);
     ros.Connect();
     while(true){
     if(isLocalPlayer){
    // if(start_flag){
       UnityEngine.Debug.Log("local player");
       UnityEngine.Debug.Log(inputField.text);
       Vector3 bar = transform.position;
       PoseStampedMsg msg = new PoseStampedMsg(new HeaderMsg(0, new TimeMsg(0,0), "cam"), new PoseMsg(new PointMsg(bar.x,bar.z,0), new QuaternionMsg(0,0,0,0)));
       UnityEngine.Debug.Log(pose_topic);
       ros.Publish(pose_topic, msg);
       yield return null;
     //  }
     }
     }
   }
   IEnumerator StartRos(){
     while(!start_flag){
       yield return null;
     }
//	cam.Render();
       // targetTexture = new RenderTexture(width,height,24);
        texture2D = new Texture2D (width, height, TextureFormat.RGB24, true);
        targetTexture = new RenderTexture(width,height,24);
        UnityEngine.Debug.LogWarning("started");
       // ros = new ROSBridgeWebSocketConnection ("ws://localhost", 9090);
        ros.AddPublisher (typeof(CompressedImageMsg));
	ros.AddSubscriber(typeof(CmdVel));
        ros.Connect ();
	yield return null;
	}
   IEnumerator UploadPNG(){
      UnityEngine.Debug.LogWarning("InsideUploadPNG");
    while (true)
     {
       if(!isLocalPlayer){
	 UnityEngine.Debug.Log("not the local player");
	 yield return null;
       }
 //     Debug.LogWarning("InsideUploadPNG");
        
             yield return new WaitForEndOfFrame();
       if(!isLocalPlayer){
	 UnityEngine.Debug.Log("not the local player");
	 yield return null;
       }
     while(!start_flag){
       yield return null;
     }
             //RenderTexture targetTexture;
//             Texture2D texture2D = new Texture2D (width, height, TextureFormat.RGB24, true);
             cam.Render();
             cam.targetTexture = targetTexture;
	     Matrix4x4 m = cam.projectionMatrix;
	     UnityEngine.Debug.Log(m);
             yield return new WaitForEndOfFrame();
       if(!isLocalPlayer){
	 UnityEngine.Debug.Log("not the local player");
	 yield return null;
       }
             RenderTexture.active = targetTexture;
        //     yield return new WaitForEndOfFrame();
             texture2D.ReadPixels (new Rect (0, 0, width, height), 0, 0);
	   //  RenderTexture.active = null;
             byte[] image = texture2D.EncodeToJPG ();
          //   yield return new WaitForEndOfFrame();
             CompressedImageMsg msg = new CompressedImageMsg(new HeaderMsg(0, new TimeMsg(0,0),"cam"), "jpeg", image);
            // yield return new WaitForEndOfFrame();
             //ros.Publish(CompressedImageMsg.GetMessageTopic(), msg);
	     string image_topic = string.Concat(namsp,"/image/compressed");
	     UnityEngine.Debug.Log(image_topic);
             ros.Publish(image_topic, msg);
	    // targetTexture = null;
//	     texture2D = null;
	     msg = null;
	     image = null;
            // yield return null;
     }
 }
  /*  void FixedUpdate(){
   //   Debug.LogWarning("InsideUploadPNG");
   // while (true)
    // {
        
      //       yield return new WaitForEndOfFrame();
             //RenderTexture targetTexture;
             Texture2D texture2D = new Texture2D (width, height, TextureFormat.RGB24, true);
             RenderTexture targetTexture;
             targetTexture = new RenderTexture(width,height,24);
             cam.targetTexture = targetTexture;
             cam.Render();
        //     yield return new WaitForEndOfFrame();
             RenderTexture.active = targetTexture;
        //     yield return new WaitForEndOfFrame();
             texture2D.ReadPixels (new Rect (0, 0, width, height), 0, 0);
             byte[] image = texture2D.EncodeToJPG ();
          //   yield return new WaitForEndOfFrame();
             CompressedImageMsg msg = new CompressedImageMsg(new HeaderMsg(0, new TimeMsg(0,0),"cam"), "jpeg", image);
            // yield return new WaitForEndOfFrame();
             ros.Publish(CompressedImageMsg.GetMessageTopic(), msg);
            // yield return null;
  //   }
 }*/
}
}
