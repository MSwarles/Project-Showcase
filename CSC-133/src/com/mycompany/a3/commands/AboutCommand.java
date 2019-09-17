package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.Dialog;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;
/**
 * Command that displays information about the program, including
 * author, the college course it was created for, and its version.
 * 
 * @author Matt
 *
 */
public class AboutCommand extends Command {
	private GameWorld gw;
	
	public AboutCommand(GameWorld gw) {
		super("About");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		System.out.println("[About] was pressed.");
		
		// displays simple dialog with 'About' information
		Dialog.show("About", "Author: Matt Swart      Course: CSC 133      Version: 2.0.0", "OK", null);
	}
}