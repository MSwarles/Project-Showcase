package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that causes a Non-Player Ship to fire a missile.
 * 
 * @author Matt
 *
 */
public class FireNpsMissileCommand extends Command {
	private GameWorld gw;
	
	public FireNpsMissileCommand(GameWorld gw) {
		super("Fire NPS Missile");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Fire NPS Missile] was pressed.");
			gw.fireNpsMissile();
		}
	}
}
