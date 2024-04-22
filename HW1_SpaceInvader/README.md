# Basic:
## Visual effect when enemy being shot and particle effect when enemy die:
  In enemy controller, change the enemy renderer to red when it being hit for 0.15 seconds, and also create a particle called EnemyDeathEffect and play it when enemy die.
## Weapons:
  Add three different guns: pistol, laser sniper, and RPJ, and their original position are at (-6, 0.5, 1), (-6, 0.5, 6), and (-6, 0.5, 11) respectively.
## Grab/Drop weapons:
  In the Update() in DistanceGrabber.cs, call GrabBegin() to let players grab the gun when press ‘A’ button, and in GrabBegin(), if closestGrabble is null, I call GrabEnd() to let player drop the gun.
  
  Also, use GrabbleGun.cs to show whether the gun can be grab now, if it is too far or your hand not point to it, it is untargeted(white), else, it is targeted(blue).
## Haptic feedback:
  In Onfire() of PlayerController.cs, add haptic feedback so player can get feedback when shooting.
# Advanced:
## Enemy HP system and health bar:
  In EnemyController.cs, add max health and current health, and creat health bar attaching to every enemy when enemy manager create it.
  
  In the Update() of HealthBar.cs, renew the fill amount of the bar with (current health) / (max health).
## Multiple game levels:
  Add three game levels, and each level has diff number of enemies, separation, speed, max health, and H/V movement.
## Skybox:
  Change the skybox after game start, and set it back to empty if player win/lose the game.
## Gun features:
  In the Update() of PlayerController.cs, check which gun is grabbed now, and set the gun to gun_1~3 ( Pistol, RPJ, and LaserSniper respectively).
  
  Each gun has diff bullet, shoot audio, speed of bullet, cooldown, and damage.
  
  The bullet of Pistol and RPJ are bullet prefabs, and the bullet of LaserSniper is a light saber.
##### Pistol: 
Short cooldown and medium damage.
##### RPJ: 
Long cooldown but high damage.
##### LaserSniper: 
Medium cooldown and damage but its bullet go through multiple enemies.
## Audios:
##### BGM: 
Played after game start
##### ShootAudio_1~3: 
Three diff fire audio of three diff guns
##### EnemyDeadAudio: 
Played when enemy die
##### Lose/Win Audio: 
Played when player lose/win the game
##### Start Audio: 
The countdown audio played before game sta
