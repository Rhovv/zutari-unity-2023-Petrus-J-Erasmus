# zutari-unity-2023-Petrus-J-Erasmus
Zutari Unity Assessment

Methodology:
I start by setting up the project, creating a few folders (such as Scripts and Images). I added the "2D Sprite" package which I generally find useful for working with some graphics.

I created the 3 scenes and decided to use a script with static variables and functions as a bridge between scenes - see the "Controller" script. This is where I store any data and perform any functions that would be called from multiple scenes.

In the Controller script I created an initialize function that it is called by the Main Menu's Start function, but is set up to only run through once. This is useful for setting up variables and other parts of the project as needed. E.g. I added a CultureInfo call to set the culture info. This prevents random errors such as the software trying to use ',' in stead of '.' as the decimal deliminator.

In the Controller script I added a function to switch scenes by name. I considdered using the scene indexes or even an enum with there names for clarity, but I think calling the scenes by name is probably the simplest method in order to allow for further scene additions as needed.

I created a script for each of the different scenes to handle their own overall control. On the Main Menu script I added public functions for the buttons on the screen to call. These functions then refer back to the overall (static) functions on the Controller script.

The scene buttons call a load scene function on Controller, passing through the name of the required scene. I also added an Exit button that either stops play while in the editor, or quits the application if in a built version.

