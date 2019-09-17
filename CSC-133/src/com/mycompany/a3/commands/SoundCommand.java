package com.mycompany.a3.commands;

import com.codename1.ui.CheckBox;
import com.codename1.ui.Command;
import com.codename1.ui.Form;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.GameWorld;

/**
 * Command that toggles the sound on/off.
 * 
 * @author Matt
 *
 */
public class SoundCommand extends Command {
	private Form f;
	private GameWorld gw;
	private CheckBox cb;
	
	public SoundCommand(Form f, GameWorld gw, CheckBox cb) {
		super("Sound");	
		this.f = f;
		this.gw = gw;	
		this.cb = cb;
	}
	
	@Override
	public void actionPerformed(ActionEvent e) {
		System.out.println("[Sound] was pressed.");
		
		if (gw.isSoundOn()) {
			gw.setSound(false);
			cb.setSelected(false);
		} else {
			gw.setSound(true);
			cb.setSelected(true);
		}
		
		// close the side menu
		f.getToolbar().closeSideMenu();
	}
}