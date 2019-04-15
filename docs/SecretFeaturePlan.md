# Secret Feature Plan (Incomplete)
## Attack Option Chosen
Option B, Implementing necessary collision detections among different bullets.

## Design Impacts
### Partial Implementation
Our existing code allows for projectile collision detection between the enemies
and users bullets. This allows us to destroy both projectiles when they collide
with one and other. However, we do not have the ability for the projectiles to
have different mass or health at the moment. 
### Necessary Changes
In order to implement the feature described for option B, we will need to add
a projectile mass property so that larger massed projectiles can push its fellow
projectiles out of its way when a collision is detected. The mass of the
projectile can also be used to calculate how much health it has so if a 
small-massed user projectile hits a large-masses enemy projectile the user’s
will die and the enemies will simply lose part of its mass.
