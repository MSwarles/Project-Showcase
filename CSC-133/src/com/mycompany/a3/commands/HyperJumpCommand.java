package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that causes the Player Ship to return to the center
 * of the map.
 * 
 * @author Matt
 *
 */
public class HyperJumpCommand extends Command {
	private GameWorld gw;
	
	public HyperJumpCommand(GameWorld gw) {
		super("Hyperjump");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Hyperjump] was pressed.");
			gw.hyperJump();
		}
	}
}
