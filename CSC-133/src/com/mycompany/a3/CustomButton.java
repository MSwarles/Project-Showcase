package com.mycompany.a3;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.Button;
import com.codename1.ui.Command;
import com.codename1.ui.Font;
import com.codename1.ui.plaf.Border;

/**
 * CustomButton is a button with preset styles for color.
 * 
 * @author Matt
 *
 */
public class CustomButton extends Button {
	private boolean enabledWhilePlaying;
	private boolean enabledWhilePaused;
	@SuppressWarnings("deprecation")
	public CustomButton(Command c, boolean enabledWhilePlaying, boolean enabledWhilePaused) {
		super(c);
		this.enabledWhilePlaying = enabledWhilePlaying;
		this.enabledWhilePaused = enabledWhilePaused;
		// set preferred height for buttons (so they all fit)
		this.setPreferredH(75);
		//calcPreferredSize();
		// create a font
		Font largeBoldSystemFont = Font.createSystemFont(Font.FACE_SYSTEM, Font.STYLE_BOLD, Font.SIZE_LARGE);
		// set the font for the buttons
		this.getAllStyles().setFont(largeBoldSystemFont);
		// colored border around the button
		this.getAllStyles().setBorder(Border.createLineBorder(2, ColorUtil.WHITE));
		// opacity of the background (255 for solid)
		this.getAllStyles().setBgTransparency(255);
		// background color
		this.getAllStyles().setBgColor(ColorUtil.BLACK);
		// text color
		this.getAllStyles().setFgColor(ColorUtil.GREEN);
		// background color when pressed
		this.getPressedStyle().setBgColor(ColorUtil.WHITE);
		// text color when pressed
		this.getPressedStyle().setFgColor(ColorUtil.rgb(230, 0, 0));
		// background color when disabled
		this.getDisabledStyle().setBgColor(ColorUtil.GRAY);
		// text color when disabled
		this.getDisabledStyle().setFgColor(ColorUtil.BLACK);
		// top padding to space buttons
		//this.getAllStyles().setPaddingTop(1);
	}
	
	public boolean isEnabledOnPlay() { return enabledWhilePlaying; }
	public boolean isEnabledOnPause() { return enabledWhilePaused; }
}
