package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that spawns a Player Ship into the game world.
 * 
 * @author Matt
 *
 */
public class RefuelCommand extends Command {
	private GameWorld gw;
	
	public RefuelCommand(GameWorld gw) {
		super("Refuel Missile");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Refuel Missile] was pressed.");
			gw.refuelMissile();
		}
	}
}
