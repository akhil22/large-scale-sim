# Large Scale Simulation
A large Scale Simulation for Autonomous Driving based on Udacity's self driving car simulator and ROS



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
- Run the game binary that we generated in the third step this will show a window to select resolution and graphics quality, please choose the appropriate resolution and graphics, on the next window click on LAN Host(H) button.
- Next window will show a car on a track. In the input field you will see the process ID of the game, Delete it and type /car1 and hit the button above the input field.
- The car should start following the right lane. Please ignore the first part of the track (bridge). I am still working on creating a better track.  
- You can run more then one instances of the simulation, First the lane detection and path following for second instance under the name space /car2:
  > roslaunch camera_info_pub lane_detection.launch ns:=/car2
- Now run a new game instances(step 3). This time type /car2 in the input field and click on button above it.
- Please note that you can control the car using keyboard if you just run the game instance without lane detection and path following(WASD control)
- Also note that all the cars start at the same position so you have to wait for the first car to move a littel bit before starting the second instance of the game otherwise they will collide :). This can be fixed later.

