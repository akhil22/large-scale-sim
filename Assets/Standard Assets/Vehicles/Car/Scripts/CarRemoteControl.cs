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
namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarRemoteControl : NetworkBehaviour
    {
        private ROSBridgeWebSocketConnection ros = null;
      //  ros.AddPublisher (typeof(Turtle1Teleop));
        private CarController m_Car; // the car controller we want to use

        public float SteeringAngle { get; set; }
        public float Acceleration { get; set; }
      //  [SerializeField] private Camera cam;
        public Camera cam2;
        private Steering s;
        void Start(){
        if(!isLocalPlayer){
       //  cam.enabled = false;
         cam2.enabled = false;
         return;
        // GetComponent<Rigidbody>().position = new Vector3(100,100,200);
        }
        
       // cam.targetTexture.width = 1024;
       // cam.targetTexture.height = 768;
       // cam.targetTexture.width = 640;
       // cam.targetTexture.height = 480;
       // cam.targetTexture.width = 320;
      //  cam.targetTexture.height = 240;
       // cam.Render();
      //  ros = new ROSBridgeWebSocketConnection ("ws://localhost", 9090);
      //  ros.AddPublisher (typeof(CompressedImageMsg));
      //  ros.Connect ();
        // cam.Render();
        m_Car = GetComponent<CarController>();
        s = new Steering();
        s.Start();
        Vector3 init = new Vector3 (195,5,-116);
        transform.position = init;
        transform.rotation = Quaternion.Euler(0,-110, 0);
     //   m_Car = GetComponent<CarController>();
        //    s = new Steering();
        //    s.Start();
        }
        private void Awake()
        { 
             

           // get the car controller
         //   m_Car = GetComponent<CarController>();
         //   s = new Steering();
         //   s.Start();
        }
private IEnumerator renderScreen()
 {
      //if(!isLocalPlayer){
       //    yield return null;
         //   }
    // while (true)
    // {
       //  yield return new WaitForEndOfFrame();
        //  yield return new WaitForEndOfFrame();
        //  yield return new WaitForEndOfFrame();
         
      //  cam.Render();
            yield return 5;
          //   yield return null;
           //  RenderTexture targetTexture = cam.targetTexture;
            
           //  RenderTexture.active = targetTexture;
            
           
           // Texture2D texture2D = new Texture2D (targetTexture.width, targetTexture.height, TextureFormat.RGB24, false);
           // texture2D.ReadPixels (new Rect (0, 0, targetTexture.width, targetTexture.height), 0, 0);
           // texture2D.Apply ();
           // byte[] image = texture2D.EncodeToJPG ();
           // UnityEngine.Object.DestroyImmediate (texture2D);
          //  CompressedImageMsg msg = new CompressedImageMsg(new HeaderMsg(0, new TimeMsg(0,0),"cam"), "jpeg", image);
          //  ros.Publish(CompressedImageMsg.GetMessageTopic(), msg);
         //   yield return null;
    // }
 }
        void Update(){
  //private void OnPostRender() {      
            if(!isLocalPlayer){
            return;
            }
          // return;
         //return new WaitForEndOfFrame();
        // StartCoroutine(renderScreen());
         return;
        //     cam.Render();
        //     yield return null;
         //    yield return null;
         //    RenderTexture targetTexture = cam.targetTexture;
            
        //     RenderTexture.active = targetTexture;
            
           
        //    Texture2D texture2D = new Texture2D (targetTexture.width, targetTexture.height, TextureFormat.RGB24, false);
         //   texture2D.ReadPixels (new Rect (0, 0, targetTexture.width, targetTexture.height), 0, 0);
         //   texture2D.Apply ();
         //   byte[] image = texture2D.EncodeToJPG ();
         //   UnityEngine.Object.DestroyImmediate (texture2D);
         //   CompressedImageMsg msg = new CompressedImageMsg(new HeaderMsg(0, new TimeMsg(0,0),"cam"), "jpeg", image);
         //   ros.Publish(CompressedImageMsg.GetMessageTopic(), msg);
           // yield return null;
        }
        private void FixedUpdate()
        {
            if(!isLocalPlayer){
            return;
            }
         //   cam.Render();

        //    RenderTexture targetTexture = cam.targetTexture;
            
        //    RenderTexture.active = targetTexture;
            
           
         //   Texture2D texture2D = new Texture2D (targetTexture.width, targetTexture.height, TextureFormat.RGB24, false);
        //    texture2D.ReadPixels (new Rect (0, 0, targetTexture.width, targetTexture.height), 0, 0);
        //    texture2D.Apply ();
         //   byte[] image = texture2D.EncodeToJPG ();
          //  UnityEngine.Object.DestroyImmediate (texture2D);
          //  CompressedImageMsg msg = new CompressedImageMsg(new HeaderMsg(0, new TimeMsg(0,0),"cam"), "jpeg", image);
         //   ros.Publish(CompressedImageMsg.GetMessageTopic(), msg);
            // If holding down W or S control the car manually
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || true)
            {
                s.UpdateValues();
//		s.H = 0f;
//		s.V = 0.2f;
                m_Car.Move(s.H, s.V, s.V, 0f);
            } else
            {
				m_Car.Move(SteeringAngle, Acceleration, Acceleration, 0f);
            }
        }
    }
}
