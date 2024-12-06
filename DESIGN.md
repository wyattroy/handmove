Handmove Design

This was a bear of a project. It's so hard to build anything in Unity -- the scripts talk to each other in very unintuitive ways, and you attach them as "Components" to empty "Game Objects" that appear in a "Hierarchy". To make matters trickier, I'm building on top of Meta's incredibly powerful but woefully undocumented VR SDK, which makes it very hard to figure out how to receive inputs and influence outputs.

For this reason, I implemented the entire Pinch Teleport mechanic in one single script that can be attached to the OVR Camera prefab that Meta includes with their SDK. I wanted it to be easy for someone to drag and drop my script onto the current best-practice development tool for VR experiences.

My first iteration of the design had the user tap once to make a "reticle" dot appear, which would then stick to the ground wherever they were looking, and once the user tapped again, the dot would disappear and they would be teleported to its location.

The first 15 hours of development were primarily trying to figure out how to do hand tracking in VR and raycast a line from the headset to the ground in such a way that the app didn't lag. Unfortunately, due to esoteric Unity settings, the reticle would cause the frame rate to drop to a nauseating 1 FPS, even when running off of my computer. Eight hours of googling later, and rewriting the entire script, the frame rate issue was solved (but I'm not entirely sure which of my various efforts were responsible).

With a functioning high frame-rate reticle, I could focus on the fun parts. The next fifteen hours of coding were spent making the design in my mind a reality, and user testing. I created a boolean to track whether the reticle was hitting a valid place to teleport to or not (eg, in an earlier iteration you could teleport into the sky). I wrote a Renderer modifier to change the reticle's color to red if you were trying to teleport somewhere invalid, and you'd be prevented from doing so. In an early version the reticle lerped out from your position to wherever you were looking -- which felt intuitive, but also laggy. I added code to ensure that the reticle immediately appears where you're looking, creating a snappier feel that grants freer movement.

Lastly, I created a demo scene to allow the user to explore a world and get used to the mechanic. I created various topographies including mountains, ridges, planks, and valleys, to test the edge cases of this teleportation mechanic.

For future iterations, I want to extend this code in the following ways:
- Allow the user to "bend" the reticle's position by moving their pinching hand -- this would make it easier for users to teleport short distances without having to crane their head down
- Create a "max angle" variable that would prevent the user from teleporting onto a vertical or near-vertical surface
- Adding sound effects

Thank you for wading through my Unity project! Unity's no joke. I appreciate you!

Video: https://youtu.be/UeqSCYz189w
