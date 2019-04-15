# Secret Feature Plan (Incomplete)

## Attack Option Chosen
`Option B` Implementing necessary collision detections among different bullets.

## Design Impacts

### Partial Implementation
Our existing code allows for projectile collision detection between the enemies
and player bullets. This allows us to destroy both projectiles when they collide
with one and other. However, we do not have the ability for the projectiles to
have different mass or health at the moment to simulate the physics of projects
bouncing off of each other, or pushing each other.

### Necessary Changes
In order to implement the feature described for `Option B`, we will need to add
a projectile mass property so that larger massed projectiles can push its fellow
projectiles out of its way when a collision is detected. The mass of the
projectile can also be used to calculate how much health it has, so if a 
small-massed user projectile hits a large-mass enemy projectile the player’s
projectile will be destroyed and the enemy's will simply lose part of its mass.
Pertaining to the final boss fight, if two projectiles belonging to the final
boss collide, then the projectile with the least mass out of the two will
be pushed in its current direction. This behavior will be achieved by applying
a multiplyer to the pushed projectile's velocity attribute, which then is
reflected by its currently assigned path object (note that the path object
in our project is responsible for determining where exactly the game object
moves to next).

We also are planning to move our collision board to the controller (of
our MVC architecture). After moving our collision board, additional logic
to handle checking the mass of projectiles will need to be added. For instance,
determining the behavior of two colliding projectiles based on their
respective mass values.