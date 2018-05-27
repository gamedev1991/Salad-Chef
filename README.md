# Salad-Chef

First Commit With Read ME file

Player 1 Controls :
Movement : WASD
Vegetable Pick Up : RBCTEF
Player 1 can keep vegetables back by pressing the same keys as the vegetable names.
Player 1 can throw vegetables in dustbin on standing near dustbin and pressing delete button , so whatever chopped vegetables the player 1 has will get thrown
Player 1 can server customers after reaching customerserve zone by pressing 1,2,3,4 for customers 1,2,3and 4 respectively

Player 2 Controls :
Movement : Arrow Keys
Vegetable Pick Up : Left Mouse Button Click on Vegetables
Player 2 can keep vegetables back by clicking on the vegetables again.
Player 2 can throw vegetables in dustbin on standing near dustbin and clicking on delete , so whatever chopped vegetables the player 1 has will get thrown
Player 2 can server customers after reaching customer server zone by clicking on the customers.


Note :Made vegetables RBCTEF as they would have otherwise conflicted with WASD.
Also, kept all vegetables on both sides so that players don't collide with one another.

Following Tasks Remain :
Powerups :
The above can be implemented by randomizing between 0 to 2 and selecting any random powerup to instantiate , which will have a collider but with the isTrigger Flag enabled , each Powerup object will have a PowerUp Script which will verify which Player can pick powerups with the help of tags. 
Speed can be varied using the move speed multiplier variable of ThirdPersonCharacter script
Time can be increased using the total time for playing variable of ThirdPersonUserControl script
Score can be varied using the player score variable of ThirdPersonUserControl script

LeaderBoard:
I will keep an array of PlayerPrefs , saving them using key+i iterations , depending on the number of iterations , I will remove all the extra ones after sorting all of them via score variable in descending order
