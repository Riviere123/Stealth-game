# Stealth Game

This is a project written in C# using the unity engine. It is currently not being worked on.

You can see all the C# code in the following path (RZU-7Project/Assets/Scripts) within you can se how I've implemented A* Pathfinding, AI states, Field of view systems, a quick and easy level infrastructure to quickly add levels and unlock requirements, Level Logic, ect...)

Pathfinding uses A*, the AI uses a system where you can create "Decisicions" and based on these decisions trigger "Actions". This can be implemented to create complex AI behaviors. Reference(RZU-7/RZU-7 Project/Assets/Scripts/AI/) you will see two sub folders called Decisions and Actions, all the scripts within inherit from the base Decision and Action class.
