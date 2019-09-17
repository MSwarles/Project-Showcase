package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that increases the Player Ship's speed.
 * 
 * @author Matt
 *
 */
public class AccelerateCommand extends Command {
	private GameWorld gw;
	
	public AccelerateCommand(GameWorld gw) {
		super("Accelerate");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Accelerate] was pressed.");
			gw.increasePsSpeed();
		}
	}
}
