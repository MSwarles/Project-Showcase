package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that spawns a Space Station into the game world.
 * 
 * @author Matt
 *
 */
public class AddSpaceStationCommand extends Command {
private GameWorld gw;
	
	public AddSpaceStationCommand(GameWorld gw) {
		super("Add Space Station");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Add Space Station] was pressed.");
			gw.spawnSpaceStation();
		}
	}
}
