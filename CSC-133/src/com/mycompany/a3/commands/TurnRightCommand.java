package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that causes the Player Ship to turn right.
 * 
 * @author Matt
 *
 */
public class TurnRightCommand extends Command {
	private GameWorld gw;
	
	public TurnRightCommand(GameWorld gw) {
		super("Turn Right");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Turn Right] was pressed.");
			gw.turnPsRight();
		}
	}
}
