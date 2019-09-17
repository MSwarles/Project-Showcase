package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that saves the game.
 * 
 * @author Matt
 *
 */
public class SaveCommand extends Command {
	private GameWorld gw;
	
	public SaveCommand(GameWorld gw) {
		super("Save");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		System.out.println("[Save] was pressed");
		// TODO: provide implementation
	}
}