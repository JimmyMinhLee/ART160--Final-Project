# ART160--Final-Project

This game was our final project for ART 160 @ UC Berkeley, Special Topics in Visual Studios: Game Design Methods. We learned a lot throughout the course of the semester, from what goes into a game - from development, to testing, to production and marketing - to the  minutae that underlies the design and feel of video games , specifically as a medium and form of art. We worked on this game over two to three months at the end of the semester, and it served as the culmination of everything Professor Greg Niemeyer taught us. 

## What is it? 

This is a simple 2D side scrolling RPG. You play as an unnamed heroine, who's tasked with clearing the way back home for her wizard uncle. We currently have 3 levels in total, two of which includes enemies that you must defeat, and a final chateau level that serves as your end-goal. 

## How did we do it? 

### Player Movement 

We modeled our player movement similarly to most other metroid-vanias. The game checks for your input on the horizontal axis, and when it receives it, applies a force to the player's RigidBody component that moves them in the direction you pressed. We also check if you press the Space button, as well as for how long, and that allows your character to jump. Pressing the Attack Button `("F")` casts a physics collider in front of the character and damages all enemies that are found. The same method is used for the enemy's attack behavior. 

### Interactions & NPC Dialogue 

Interactions with objects are handled eloquently (in our opinion.) If you're in the hitbox collider of an NPC and you press the `E` button, the game will re-enable the dialogue box associated with the NPC and change its text to be what the NPC wants to say. Specifically, for our main NPC's dialogue (your uncle), we retain a list of text that he needs to get through, as well as a counter for which text is currently shown on screen. If you leave his hitbox mid-conversation, he resets his counter to 0 for your next interaction. We do the same when you reach the end of the conversation. 

### Enemy Behavior 

The enemies are implemented pretty simply - and you can find all the code in `Enemy.cs`. For attacking, the enemy retains a counter for the amount of time spent in between attacks. Once it reaches 0, if the player is in their attack range (handled by the same raycast mechanism we used for player attacks), they'll call the Player's `TakeDamage()` method and deal damage to them. The Enemy also retains a Line of Sight hitbox that extends both forward and backwards - if the player enters it, they retain the player's position (so long as they stay in that hitbox) and slowly moves towards the player. We use the `Transform.MoveTowards()` function for this behavior. We also offset the enemy's position at the end of the walk, such that they don't stand directly on top of the player, by adding 2.5 to the Player's position's x. We also use this to flip the enemy model around when the player moves behind them. Finally, if their HP reaches 0, we set a boolean to True that disables all enemy behavior. 

## What did we learn? 

Building this game was an amazing experience - especially since none of us really had much Unity experience beforehand, especially on a larger project like this. We learned the ropes and basics of Unity at the very start, and we slowly got the hang of it as time went on - we were able to import and create animations, wire them to player behaviors, and implement whole features very quickly about a week before our presentation for the class. We also realized very quickly though that building an RPG in this vein is very complex: even though it might seem simple, you're responsible for implementing every single interaction and feature in the game. What's worse is that every one of these features is integral to the functioning of the game - nothing can really be skipped out on, and so we spent a lot of time working on every single feature and making sure it was bulletproof before moving on. If we could do this all over again, we'd choose a game that relied on one sole gameplay loop - that way, we can focus on polishing it completely and then moving onto the add-ons quickly afterwards. 

With all that being said though, again - we learned a lot throughout the duration of this semester and we're extremely happy with our final product. Seeing our character come to life and venture throughout the world made the experience worth it - and our professor was very supportive every step of the way. 

## Team 

Our group was comprised of me, Kaycee Pham, and Rhea Diaz. Rhea handled all of the artwork (and amazingly well, at that), and Kaycee and I worked on all of the programming. 

## How do I run this? 

We have an itch.io page that hosts the game! You can find it here: https://jimmyminhlee.itch.io/chateau-nonesuch-a160-final 

If you're interested in the actual mechanisms of the project, feel free to give it a download and flip through all of our code & artwork. We kept the project hierarchy pretty clean, so hopefully it's not too much of a headache to find yoru way through. 

## Conclusion 

That's our project! Thanks for taking the time to read through this and giving us a look. If you have any feedback or suggestions, feel free to reach out to me on email. We hope you enjoy it, or learned at least something from our process. 



