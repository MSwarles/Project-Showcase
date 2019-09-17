package com.mycompany.a3.commands;

import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that increments the elapsed game time.
 * 
 * @author Matt
 *
 */
public class TickCommand extends Command {
	private GameWorld gw;
	
	public TickCommand(GameWorld gw) {
		super("Game Clock Tick");		
		this.gw = gw;		
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			System.out.println("[Game Clock Tick] was pressed.");
			//gw.tick();
		}
	}
}
