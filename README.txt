EECS481HW2
==========

Homework 2 for EECS 481: Simple pong game on Microsoft XNA Game Studio


This game is a basic pong game. The ball always starts in the middle of the screen and is assigned a random
direction and speed. Each player is trying to prevent the ball from passing their side of the screen.
If the ball passes the player then the opposing player is awarded one point and the ball is set in a random motion
starting from the middle of the screen once again. The game has no set ending point and only ends when the players 
decide to stop playing. The window can be closed by pressing "esc" or closing the window manually.

Controls:

Left Player (Nadal)
A: Move Nadal paddle up
Z: Move Nadal paddle down

Right Player (Federer)
UP KEY: Move Federer paddle up
DOWN KEY: Move Federer paddle down

General:
ESC: Close window, exit game


DOWNLOAD AND BUILD INSTRUCTIONS:

The code can be downloaded at https://github.com/spradhan12/EECS481HW2 by running:

git clone spradhan12@github.com:EECS481HW2 .

in GitBash. This will copy the repo into the current working directory (hence the period).

The file WindowsGame1 can then be opened in VisualStudio10 and built and run there as a Windows C# solution.
