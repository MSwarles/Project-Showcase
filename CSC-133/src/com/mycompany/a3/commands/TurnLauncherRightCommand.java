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
public class TurnLauncherRightCommand extends Command {
	private GameWorld gw;
	
	public TurnLauncherRightCommand(GameWorld gw) {
		super("Turn Launcher Right");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Turn Launcher Right] was pressed.");
			gw.turnPsLauncherRight();
		}
	}

}
