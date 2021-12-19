# WLF-SHP-SIM
Wolf-Sheep Simulation

This simulation doesn't work, and I'm not sure if I'll fix it.
But with that said, I'm going to note the major problem stopping it from being fixed right now.

Along with the behaviour programming being messy, the colliders intefere with one another.
A sphere collider is used for the behaviours, and a box collider is used for game collisions.
However, both trigger the same functions, so they don't work as intended.
The sphere collider is the bigger of the two, so that one takes priority.
I'm not sure how this problem could be fixed, but it'd be pretty complicated either way.

Other than that, I'm not sure if the life-death system works too well.
Also, naturally the simulation is pretty bare bones, so it would need more work on visuals too.

Once again, not sure if I'll fix thse problems, but I wanted to note them.