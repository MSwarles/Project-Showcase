package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that reloads the Player Ship's missiles from a Space Station.
 * 
 * @author Matt
 *
 */
public class ReloadMissilesCommand extends Command {
	private GameWorld gw;
		
	public ReloadMissilesCommand(GameWorld gw) {
		super("Reload Missiles");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		System.out.println("[Reload Missiles] was pressed.");
		if (e.getKeyEvent() != -1) {
			gw.reloadPsMissiles();
		}
	}
}
