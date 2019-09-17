package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that causes the Player Ship to turn left.
 * 
 * @author Matt
 *
 */
public class TurnLeftCommand extends Command {
	private GameWorld gw;
	
	public TurnLeftCommand(GameWorld gw) {
		super("Turn Left");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Turn Left] was pressed.");
			gw.turnPsLeft();
		}
	}
}
