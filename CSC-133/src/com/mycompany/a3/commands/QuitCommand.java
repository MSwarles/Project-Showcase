package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.Dialog;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that causes a dialog to pop-up, asking the user if they want to quit.
 * 
 * @author Matt
 *
 */
public class QuitCommand extends Command {
	private GameWorld gw;
	
	public QuitCommand(GameWorld gw) {
		super("Quit");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Quit] was pressed.");
			boolean bOk = Dialog.show("Confirm quit", "Are you sure you want to quit?", "OK", "Cancel");
			if (bOk)
				System.exit(0);
		}
	}
}