package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that undoes the last command entered.
 * 
 * @author Matt
 *
 */
public class UndoCommand extends Command {
	private GameWorld gw;
	
	public UndoCommand(GameWorld gw) {
		super("Undo");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		System.out.println("[Undo] was pressed");
		// TODO: provide implementation
	}
}