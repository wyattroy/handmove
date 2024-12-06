HANDMOVE
By Wyatt Roy, 12/6/2024

OVERVIEW
Handmove is a Unity package that lets you teleport around a virtual reality scene without controllers, using a novel UX locomotion mechanic designed by Wyatt.
It contains a script, prefabs, and a sample scene. It extends Meta All-In-One SDK and runs on Unity 2202.3.9f1 LTS.

VIDEO
https://youtu.be/UeqSCYz189w

HOW TO DEMO
1) Side-load the provided APK on a Meta Quest headset, Quest 2 or later. For help sideloading, see https://www.uploadvr.com/sideloading-quest-how-to/
2) Open the APK
3) Put your controllers down
4) Pinch your right forefinger and thumb together
5) Look around, and see a dot track where your head moves
6) Release your pinched fingers, and you'll teleport to where the dot was

HOW TO USE IN YOUR OWN PROJECT
1) Download the repo from https://github.com/wyattroy/handmove (or just the unity package linked within)
2) Download Unity Hub
3) Install Unity 2202.3.9f1 LTS
4) Open the Sample Scene
5) Click on '[BuildingBlock] Camera Rig' in the Unity Hierarchy
6) Scroll down to Pinch To Teleport script in the Inspector
7) Adjust the maximum teleportable distance
8) Change the look and materials of the "reticle" dot
9) Drag and drop the OVR Prefab into your own scene

SCRIPT
The mechanics of Handmove are programmed in PinchToTeleport.cs, a C# Script included in the package. Feel free to extend it or change it.

BACKGROUND CONTEXT
Virtual worlds are often much bigger than the room we stand or sit in to access them from. When we slip into a headset, we become able to move through space in two ways: (1) with our physical body, like by looking around or taking small steps, and (2) with our digital perspective, by teleporting the transform of our VR camera around the scene.

Most locomotion mechanics are clunky, unintuitive, and cause motion sickness. They also rely on controllers. I wanted to create a natural gestural way to move through a virtual world with as little friction as possible -- by simply tapping your fingers together. Keep your hand down by your waist, and look where you want to go.

Thanks for building new worlds in VR for everyone to explore!
