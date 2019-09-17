package com.mycompany.a3.views;

import java.util.Observable;
import java.util.Observer;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.Container;
import com.codename1.ui.Label;
import com.codename1.ui.layouts.GridLayout;
import com.mycompany.a3.interfaces.IGameWorld;

/**
 * PointsView is a view that displays game state data, such as total
 * points, number of missiles, elapsed time, sound status, and lives.
 * 
 * @author Matt
 *
 */
public class PointsView extends Container implements Observer {	
	private Label lPointsText;
	private Label lPointsVal;
	private Label lMissileText;
	private Label lMissileVal;
	private Label lElapsedTimeText;
	private Label lElapsedTimeVal;
	private Label lSoundText;
	private Label lSoundVal;
	private Label lLivesText;
	private Label lLivesVal;
	private int textColor = ColorUtil.GREEN;
	private int valueColor = ColorUtil.WHITE;
	
	public PointsView(IGameWorld gw) {		
		// Set layout to Grid (for stable spacing of labels)
		this.setLayout(new GridLayout(1, 5));
		// Set style of the Points View
		this.getAllStyles().setBgTransparency(255);
		this.getAllStyles().setBgColor(ColorUtil.BLACK);
		
		// Setting points label text and values
		Container pointsContainer = new Container();
		lPointsText = new Label("Points:");
		lPointsText.getAllStyles().setFgColor(textColor);
		pointsContainer.add(lPointsText);
		
		lPointsVal = new Label ("" +  gw.getPoints());
		lPointsVal.getAllStyles().setFgColor(valueColor);
		pointsContainer.add(lPointsVal);
		this.add(pointsContainer);
		
		// Setting missile label text and values
		Container missileContainer = new Container();
		lMissileText = new Label("Missiles:");
		lMissileText.getAllStyles().setFgColor(textColor);
		missileContainer.add(lMissileText);
		
		lMissileVal = new Label("" + gw.getMissiles());
		lMissileVal.getAllStyles().setFgColor(valueColor);
		missileContainer.add(lMissileVal);
		this.add(missileContainer);
				
		// Setting elapsed time label text and values
		Container timeContainer = new Container();
		lElapsedTimeText = new Label("Time:");
		lElapsedTimeText.getAllStyles().setFgColor(textColor);
		timeContainer.add(lElapsedTimeText);
		
		lElapsedTimeVal = new Label("" + gw.getGameTime());
		lElapsedTimeVal.getAllStyles().setFgColor(valueColor);
		timeContainer.add(lElapsedTimeVal);
		this.add(timeContainer);
		
		// Setting sound label text and values
		Container soundContainer = new Container();
		lSoundText = new Label("Sound:");
		lSoundText.getAllStyles().setFgColor(textColor);
		soundContainer.add(lSoundText);
		
		lSoundVal = new Label((gw.isSoundOn() ? "ON " : "OFF"));
		lSoundVal.getAllStyles().setFgColor(valueColor);
		soundContainer.add(lSoundVal);
		this.add(soundContainer);
		
		// Setting lives label text and values
		Container livesContainer = new Container();
		lLivesText = new Label("Lives:");
		lLivesText.getAllStyles().setFgColor(textColor);
		livesContainer.add(lLivesText);
		
		lLivesVal = new Label("" + gw.getLives());
		lLivesVal.getAllStyles().setFgColor(valueColor);
		livesContainer.add(lLivesVal);
		this.add(livesContainer);
		
		// Revalidate the view
		this.revalidate();
	}
	
	public void update(Observable observable, Object data) {
		// Cast data as IGameWorld
		IGameWorld gw = (IGameWorld)data;		
		
		// Update value labels with the current values in game world
		lPointsVal.setText("" + gw.getPoints());	
		lMissileVal.setText("" + gw.getMissiles());
		lElapsedTimeVal.setText("" + gw.getGameTime());
		lSoundVal.setText((gw.isSoundOn() ? "ON" : "OFF"));
		lLivesVal.setText("" + gw.getLives());
		
		// Revalidate the view
		this.revalidate();
	}

}
