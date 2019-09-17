package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that begins a new game.
 * 
 * @author Matt
 *
 */
public class NewCommand extends Command {
	private GameWorld gw;
	
	public NewCommand(GameWorld gw) {
		super("New");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		System.out.println("[New] was pressed.");
		// TODO: provide implementation
	}
}