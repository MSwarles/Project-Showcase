package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that causes the Player Ship's Missile Launcher to turn left.
 * 
 * @author Matt
 *
 */
public class TurnLauncherLeftCommand extends Command {
	private GameWorld gw;
	
	public TurnLauncherLeftCommand(GameWorld gw) {
		super("Turn Launcher Left");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Turn Launcher Left] was pressed.");
			gw.turnPsLauncherLeft();
		}
	}

}
