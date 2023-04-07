# zutari-unity-2023-Petrus-J-Erasmus
Zutari Unity Assessment

Methodology:
I start by setting up the project, creating a few folders (such as Scripts and Images). I added the "2D Sprite" package which I generally find useful for working with some graphics.

I created the 3 scenes and decided to use a script with static variables and functions as a bridge between scenes - see the "Controller" script. This is where I store any data and perform any functions that would be called from multiple scenes.

In the Controller script I added a function to switch scenes by name. I considdered using the scene indexes or even an enum with there names for clarity, but I think calling the scenes by name is probably the simplest method in order to allow for further scene additions as needed.

I created a script for each of the different scenes to handle their own overall control. On the Main Menu script I added public functions for the buttons on the screen to call. These functions then refer back to the overall (static) functions on the Controller script.

The scene buttons call a load scene function on Controller, passing through the name of the required scene. I also added an Exit button that either stops play while in the editor, or quits the application if in a built version.

Next I started implementing the Level One scene. I set the camera to orthographic as required, and added and centered the cube. I also created a material to add to the cube in order to change it's color as required, and a rigidbody component to interact with as specified.

I removed gravity (on the rigidbody) and added some drag so that the cube slides to a stop after the buttons have been released.

In the script for the cube controls I apply a force to the cube while the keys are pressed. The force is applied in the FixedUpdate function to ensure it works the same regardless of the framerate.

On update, I check the velocity of the cube and switch the cube's color according to the direction it is travelling the fastest in. I added a small color compass to show the colors for the specific directions.

I added a bit of UI to allow a return to the main menu, and to view and change the cube speed. I also added some limits to ensure the speed doesn't go below 0 or above a 1000.

As to the cube reappearing on the opposite side of the screen, I added a box-collider which includes a size component that I use in conjuction with the cube's position to determine its position on screen (with WorldToScreenPoint) and see if it has passed off of the screen. When it does, I simply position it on the opposite side of where it left the screen, thus bringing it back into view.

Moving on to the weather app scene, I wrote a co-routine (off of a tutorial) that creates a web request and casts the data to the required class layout. This co-routine is run for each of the cities. When it has been completed, the values are written to the displayed text on screen.