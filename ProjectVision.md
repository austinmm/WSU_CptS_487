# Project Vision

## Game Rules/Controls

* Use the up/down/left/right arrow keys to move.
* Hold S to move slowly.
* Press SPACE to shoot.

## Overview:

### There are 4 Phases:
+ #### General Layout:
| Phase | Type | Player Health | Enemy Count | Enemy Type 1 | Enemy Type 2 | Projectile Path 1 | Projectile Path 2 |
|--------------|------------------------|:---:|:---:|----------------|------------|---------------|--------------|
| Phase One    | Regular play w/ Grunts | 50  | 10  | Easy - 7       | Medium - 3 | Straight | Triangle     |
| Phase Two    | Mid Boss Attack        | 75  | 2   | Mid Boss - 2   | N/A        | Spiral   | Semicircle   |
| Phase Three  | More Grunts            | 75  | 15  | Hard - 10      | Medium - 5 | Circle   | Spiral Down  |
| Phase Four   | Final Boss             | 100 | 1   | Final Boss - 1 | N/A        | Firework | Checkerboard |

#### Phase 1. Regular Play w/ Grunts:
* The first phase will render 10 Enemies; however, only four will ever be on the screen at one time.
* Everytime and Enemy dies a new one is rendered on screen until all 10 have been killed.
* The Enemies will be...
    * Easy Enemy (7): 10 Health and 5pts Projectile Damage
    * Medium Enemy (3): 25 Health and 15pts Projectile Damage
* The Enemy Projectiles will be...
    * Easy Enemy: Straight Path (Rockets)
    * Medium Enemy: Triangle Path (Fire Balls)
* The Player will have...
    * 50 Health
    * Projectiles: Straight Path (Lazers) - 5pts Damage
* Once all 10 enemies are killed then Mid Boss Attack begins.

#### Phase 2. Mid Boss Attack:
* Two Mid Boss Enemies will be rendered during this stage; the first will spawn immediately and the other will spawn shortly after.
* The Mid Boss Enemies will have...
    * 80 Health and 15pts Projectile Damage
    * Projectiles:
        * Fire Works (Fire Balls) - 7pts Damage
        * Lazers - 5pts Damage
* The Player will have...
    * 75 Health
    * Projectiles: Straight Path (Lazers) - 8 pts Damage

#### Phase 3. More Grunts:
* The third phase will render 15 Enemies; one Medium and one Hard enemy will be rendered at a time until none are left of each type.
* The Enemies will be...
    * Medium Enemy (5): 25 Health and 15pts Projectile Damage
    * Hard Enemy (10): 35 Health and 20pts Projectile Damage
* The Enemy Projectiles will be...
    * Medium Enemy: Triangle Path (Fire Balls)
    * Hard Enemy: Fire Works (Fire Balls)
* The Player will have...
    * 75 Health
    * Projectiles: Straight Path (Lazers) - 6 pts Damage

#### Phase 4. Final Boss Attack:
* One Final Boss will be rendered during this stage.
* The Final Boss will have...
    * 200 Health
    * Projectiles: 
        * Fire Works (Fire Balls) - 10pts Damage
        * Chekerboard (Arrow Heads) - 15pts Damage 
* The Player will have...
    * 100 Health
    * Projectiles: Straight Path (Lazers) - 10pts Damage

##### Bosses
Bosses need more sophisticated attacks
Bosss must have two different stages of attack, mimicking the first stage and third stage of the demo video
01:36-02:22
03:07-03:52