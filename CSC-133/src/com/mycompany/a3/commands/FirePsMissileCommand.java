package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that causes the Player Ship to fire a missile.
 * 
 * @author Matt
 *
 */
public class FirePsMissileCommand extends Command {
	private GameWorld gw;
	
	public FirePsMissileCommand(GameWorld gw) {
		super("Fire PS Missile");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Fire PS Missile] was pressed.");
			gw.firePsMissile();
		}
	}
}
