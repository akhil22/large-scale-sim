# Large Scale Simulation




# Dependencies

- Unity: Install [Unity for Linux](https://forum.unity.com/threads/unity-on-linux-release-notes-and-known-issues.350256/): 
- Open this project inside unity and [open](https://docs.unity3d.com/Manual/CreatingScenes.html) the new_track scene. Its located inside large-scale-sim/Assets
-  [Build](https://docs.unity3d.com/Manual/BuildSettings.html) the scene. You can select the target platform from the build settings menu. This will finally generate a binary file that can be run independently of the editor just like a game but we will come to this later. 
- ROS: install the Dataspeedinc's lane detection ros package from [here](https://bitbucket.org/DataspeedInc/dbw_mkz_simulation). Install all the packages listed on the site.
- Run the following commands to install addtional ros packages:
  > sudo apt install ros-kinetic-rosbridge-server
 - Build the ros controller package:
   > cd <path_to_catkin_ws>/src  
   > git clone https://github.com/akhil22/camera_info_pub.git  
   > cd <path_to_catkin_ws>  
   > catkin_make   
 - Now we have installed all the dependencies its time to run the simulation 

# Running
- Run the rosbridge websocket server:
  > roslaunch rosbridge_server rosbridge_websocket.launch
 - Run lane detection and path follower under namespace /car1:
   > roslaunch camera_info_pub lane_detection.launch ns:=/car1
- Run the game binary that we generated in the third step choose the appropriate resolution and graphics. on the next window click on LAN Host(H) button.
- Next window will show a car on a track. In the input field you will see the process ID of the game, Delete it and type /car1 and hit the button above the input field.
- The car should start following the right lane. Please ignore the first par of the track (bridge). I am still working on creating a better track.  

