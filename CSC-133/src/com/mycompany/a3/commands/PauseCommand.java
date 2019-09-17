package com.mycompany.a3.commands;

import com.codename1.ui.Button;
import com.codename1.ui.Command;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.Game;
import com.mycompany.a3.GameWorld;

/**
 * Command that causes the game to pause and unpause.
 * 
 * @author Matt
 *
 */
public class PauseCommand extends Command {
	private Game g;
	private GameWorld gw;
	
	public PauseCommand(Game g, GameWorld gw) {
		super("Pause");		
		this.g = g;
		this.gw = gw;
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getKeyEvent() != -1) {
			if (e.getComponent() instanceof Button) {
				Button b = (Button)e.getComponent();
				b.setText((gw.isPaused() ? "Pause" : "Resume"));
			}
			
			if (gw.isPaused()) {			
				g.resume();
				gw.setPaused(false);
				System.out.println("Game is RESUMED");
			} else {
				g.pause();
				gw.setPaused(true);
				System.out.println("Game is PAUSED");
			}
		}
	}
}
