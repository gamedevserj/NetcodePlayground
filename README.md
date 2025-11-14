# NetcodePlayground

A simple project to try out the Netcode.  

Player has some scripts that have use client authority to update animator and transform as this is meant for co-op style games where cheating is not the main concern. And setting it up this is easier.  
Interactable items use server authority just to keep things better syncronized.  
Player uses state machine as a means to filter inputs.  
