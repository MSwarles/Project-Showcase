package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that spawns a Non-Player Ship into the game world.
 * 
 * @author Matt
 *
 */
public class AddNpsCommand extends Command {
	private GameWorld gw;
	
	public AddNpsCommand(GameWorld gw) {
		super("Add Non-Player Ship");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		System.out.println("[Add Non-Player Ship] was pressed.");
		if (e.getKeyEvent() != -1) {
			gw.spawnNps();
		}
	}
}
