package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that spawns an Asteroid into the game world.
 * 
 * @author Matt
 *
 */
public class AddAsteroidCommand extends Command {	
	private GameWorld gw;
	
	public AddAsteroidCommand(GameWorld gw) {
		super("Add Asteroid");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
	   if (e.getKeyEvent() != -1) {
		   System.out.println("[Add Asteroid] was pressed.");
		   gw.spawnAsteroid();
	   }
	}

}
