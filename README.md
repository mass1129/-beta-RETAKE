# RETAKE

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)


## Requirement

[Unity 2021.3.5f1 (LTS)](https://unity.cn/release-notes/lts/2020/2020.3.4f1)

## Game logic and functionality

* 오프닝 씬  
  <img src="Image/main_startScene.gif"></img>  
  * **UI 애니메이션**  
  <img src="Image/introScene1.png" height="200px"></img> <img src="Image/introsceneTimeline.png" height="200px"></img>    


* Player models
  * All the original models and their animations were found from **[Mixamo](https://www.mixamo.com/)**, which is a pretty good game model website run by Adobe
    * **Policeman**: a policeman-like model with yellow skin
    * <img src="Images/introScene1.png" height="200px"></img> <img src="Images/11.jpg" height="200px"></img> <img src="Images/10.jpg" height="200px"></img>

  * **Animations**:
    * **Walk** towards four different directions
    * **Run** towards four different directions
    * **Jump** without affecting upper part body (**achieved by unity3d body mask**)
    * **Shoot** without affecting lower part body (**achieved by unity3d body mask**)
    * **Unity Blend Tree**
      * This makes the player walk or run more naturally. It uses interpolation function to map different combinations of user input to different animations.
      * ![img](Images/4.jpg)



* Player movement
  * Walking
  * Jumping

* Bullet effects
  * Bullets hitting different materials will cause different effects
    * Wood
    <img src="Images/13.jpg" style="width:510px"></img>


## Script files

* **CameraRotation.cs**

### Input Devices

* Mouse and keyboard
  * The traditional way
  * Cheap and easy to use



## Contribution



## License



