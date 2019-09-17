package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that decreases the Player Ship's speed.
 * 
 * @author Matt
 *
 */
public class DecelerateCommand extends Command {
	private GameWorld gw;
	
	public DecelerateCommand(GameWorld gw) {
		super("Decelerate");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Decelerate] was pressed.");
			gw.decreasePsSpeed();
		}
	}
}
