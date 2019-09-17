package com.mycompany.a3;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.Button;
import com.codename1.ui.CheckBox;
import com.codename1.ui.Command;
import com.codename1.ui.Component;
import com.codename1.ui.Container;
import com.codename1.ui.Dialog;
import com.codename1.ui.Form;
import com.codename1.ui.events.ActionListener;
import com.codename1.ui.layouts.BorderLayout;
import com.codename1.ui.layouts.BoxLayout;
import com.codename1.ui.plaf.Border;
import com.codename1.ui.util.UITimer;
import com.codename1.ui.Label;
import com.codename1.ui.TextField;
import com.codename1.ui.Toolbar;
import com.codename1.ui.events.ActionEvent;
import com.mycompany.a3.commands.*;
import com.mycompany.a3.views.*;
import java.lang.String;
import java.util.ArrayList;

/**
 * Encapsulates the flow of control of the game. 
 * <p>
 * Accepts input in form of keyboard commands from the human player and 
 * invokes appropriate methods in @GameWorld to perform the requested commands.
 *  
 * @author Matt
 *
 */
@SuppressWarnings("unused")
public class Game extends Form implements Runnable {
	/** 
	 * The current game world
	 */
	private GameWorld gw;
	/**
	 * The map view that displays the game objects in the game world
	 */
	private static MapView mapView;
	/**
	 * The points view that displays the game state values
	 */
	private PointsView pointsView;
	/**
	 * Left container
	 */
	private ArrayList<CustomButton> leftContainerButtons = new ArrayList<CustomButton>();
	/**
	 * The frames per second the game is running at
	 */
	private int FPS = 60;
	
	Command addAsteroidCommand;
	Command addNpsCommand;
	Command addSpaceStationCommand;
	Command addPsCommand;
	Command accelerateCommand;
	Command decelerateCommand;
	Command turnLeftCommand;
	Command turnRightCommand;
	Command turnLauncherLeftCommand;
	Command turnLauncherRightCommand;
	Command firePsMissileCommand;
	Command fireNpsMissileCommand;
	Command hyperJumpCommand;
	Command reloadMissilesCommand;
	Command tickCommand;
	Command quitCommand;
	Command pauseCommand;
	Command refuelCommand;
	Command newCommand;
	Command saveCommand;
	Command undoCommand;
	Command soundCommand;
	Command aboutCommand;

	/**
	 * Class constructor that creates and initializes a game world. 
	 * Also sets up the GUI that the user will use to interact with
	 * the game and issue various commands.
	 */
	public Game() {
		// create a timer and provide a runnable (this form)
		UITimer timer = new UITimer(this);
		// make the timer tick according to fps setting
		timer.schedule(msPerFrame() - 6, true, this);
		
		// get the toolbar
		Toolbar toolbar = this.getToolbar();
		// set title of this form
		this.setTitle("Asteroid Game");

		// set main form layout to Border
		this.setLayout(new BorderLayout());
		this.getAllStyles().setBgColor(ColorUtil.WHITE);
		
		// create an instance of GameWorld
		gw = new GameWorld();
		
		// create the views
		pointsView = new PointsView(gw);
		mapView = new MapView(gw);
		
		// add map view to center portion of border layout
		this.add(BorderLayout.CENTER, mapView);
		// add points view to north portion of border layout
		this.add(BorderLayout.NORTH, pointsView);		
	
		// add the observers to the Game World
		gw.addObserver(pointsView);
		gw.addObserver(mapView);	
		
		//-------------------------------------------------------------------
		//----------------------- Command Creation --------------------------
		//-------------------------------------------------------------------
		// create commands
		addAsteroidCommand = new AddAsteroidCommand(gw);
		addNpsCommand = new AddNpsCommand(gw);
		addSpaceStationCommand = new AddSpaceStationCommand(gw);
		addPsCommand = new AddPsCommand(gw);
		accelerateCommand = new AccelerateCommand(gw);
		decelerateCommand = new DecelerateCommand(gw);
		turnLeftCommand = new TurnLeftCommand(gw);
		turnRightCommand = new TurnRightCommand(gw);
		turnLauncherLeftCommand = new TurnLauncherLeftCommand(gw);
		turnLauncherRightCommand = new TurnLauncherRightCommand(gw);
		firePsMissileCommand = new FirePsMissileCommand(gw);
		fireNpsMissileCommand = new FireNpsMissileCommand(gw);
		hyperJumpCommand = new HyperJumpCommand(gw);
		reloadMissilesCommand = new ReloadMissilesCommand(gw);
		tickCommand = new TickCommand(gw);
		quitCommand = new QuitCommand(gw);
		pauseCommand = new PauseCommand(this, gw);
		refuelCommand = new RefuelCommand(gw);
		
		CheckBox cbSound = new CheckBox("Sound");
		
		newCommand = new NewCommand(gw);
		saveCommand = new SaveCommand(gw);
		undoCommand = new UndoCommand(gw);
		soundCommand = new SoundCommand(this, gw, cbSound);
		aboutCommand = new AboutCommand(gw);
		/*
		Command addAsteroidCommand = new AddAsteroidCommand(gw);
		Command addNpsCommand = new AddNpsCommand(gw);
		Command addSpaceStationCommand = new AddSpaceStationCommand(gw);
		Command addPsCommand = new AddPsCommand(gw);
		Command accelerateCommand = new AccelerateCommand(gw);
		Command decelerateCommand = new DecelerateCommand(gw);
		Command turnLeftCommand = new TurnLeftCommand(gw);
		Command turnRightCommand = new TurnRightCommand(gw);
		Command turnLauncherLeftCommand = new TurnLauncherLeftCommand(gw);
		Command turnLauncherRightCommand = new TurnLauncherRightCommand(gw);
		Command firePsMissileCommand = new FirePsMissileCommand(gw);
		Command fireNpsMissileCommand = new FireNpsMissileCommand(gw);
		Command hyperJumpCommand = new HyperJumpCommand(gw);
		Command reloadMissilesCommand = new ReloadMissilesCommand(gw);
		Command tickCommand = new TickCommand(gw);
		Command quitCommand = new QuitCommand(gw);
		Command pauseCommand = new PauseCommand(this, gw);
		Command refuelCommand = new RefuelCommand(gw);
		
		CheckBox cbSound = new CheckBox("Sound");
		
		Command newCommand = new NewCommand(gw);
		Command saveCommand = new SaveCommand(gw);
		Command undoCommand = new UndoCommand(gw);
		Command soundCommand = new SoundCommand(this, gw, cbSound);
		Command aboutCommand = new AboutCommand(gw);
		*/
		
		//-------------------------------------------------------------------
		//-------------------------- Key Bindings ---------------------------
		//-------------------------------------------------------------------
		// Set the key bindings for commands
		// Binding up arrow to increase ship speed (up arrow key code = -91)
		addKeyListener(-91, accelerateCommand);
		// Binding down arrow to decrease ship speed (down arrow key code = -92)
		addKeyListener(-92, decelerateCommand);
		// Binding left arrow to turn the ship left (left arrow key code = -93)
		addKeyListener(-93, turnLeftCommand);
		// Binding right arrow to turn the ship right (right arrow key code = -94)
		addKeyListener(-94, turnRightCommand);
		// Binding the space bar to fire a ship missile (space bar key code = -90) (this also binds the enter key)
		addKeyListener(-90, firePsMissileCommand);
		// Binding < to turn launcher left (< key code = 44)
		addKeyListener(44, turnLauncherLeftCommand);
		// Binding > to turn launcher right (> key code = 46)
		addKeyListener(46, turnLauncherRightCommand);
		// Binding j to hyperjump (j key code = 106)
		addKeyListener(106, hyperJumpCommand);
		
		//-------------------------------------------------------------------
		//-------------------------- Side Menu ------------------------------
		//-------------------------------------------------------------------
		/* Add commands to the SideMenu ('hamburger' button in top left)
		*  SideMenu should include:
		*  "a" (add new asteroid), "b" (add new space station), "s" (add player ship), 
		*  "n" (reload missiles), "k" (PS missile hit Asteroid), "c" (PS crashed into asteroid), 
		*  "x" (2 Asteroids collide), "t" (tick), "q" (quit)
		*/
		toolbar.addCommandToSideMenu(addAsteroidCommand);
		toolbar.addCommandToSideMenu(addSpaceStationCommand);
		toolbar.addCommandToSideMenu(addPsCommand);
		toolbar.addCommandToSideMenu(reloadMissilesCommand);
		toolbar.addCommandToSideMenu(tickCommand);
		cbSound.setSelected(gw.isSoundOn()); 
		cbSound.setCommand(soundCommand);
		toolbar.addComponentToSideMenu(cbSound);		
		toolbar.addCommandToSideMenu(quitCommand);
		
		//-------------------------------------------------------------------
		//------------------------ Overflow Menu ----------------------------
		//-------------------------------------------------------------------
		// Add commands to the Overflow Menu (3 dots in top right).
		// Should include: New, Save, Undo, Sound, About, Quit.
		toolbar.addCommandToOverflowMenu(newCommand);
		toolbar.addCommandToOverflowMenu(saveCommand);
		toolbar.addCommandToOverflowMenu(undoCommand);
		toolbar.addCommandToOverflowMenu(soundCommand);
		toolbar.addCommandToOverflowMenu(aboutCommand);
		toolbar.addCommandToOverflowMenu(quitCommand);
		
		//-------------------------------------------------------------------
		//-------------------- Left Command Container -----------------------
		//-------------------------------------------------------------------
		// create command container and set layout to box, with y_axis
		Container leftContainer = new Container(new BoxLayout(BoxLayout.Y_AXIS));
		leftContainer.getAllStyles().setBorder(Border.createLineBorder(4, ColorUtil.WHITE));
		
		CustomButton bAddAsteroid = new CustomButton(addAsteroidCommand, true, false);
		leftContainerButtons.add(bAddAsteroid);
		CustomButton bAddNps = new CustomButton(addNpsCommand, true, false);
		leftContainerButtons.add(bAddNps);
		CustomButton bAddSpaceStation = new CustomButton(addSpaceStationCommand, true, false);
		leftContainerButtons.add(bAddSpaceStation);
		CustomButton bAddPs = new CustomButton(addPsCommand, true, false);
		leftContainerButtons.add(bAddPs);
		CustomButton bAccelerate = new CustomButton(accelerateCommand, true, false);
		leftContainerButtons.add(bAccelerate);
		CustomButton bDecelerate = new CustomButton(decelerateCommand, true, false);
		leftContainerButtons.add(bDecelerate);
		CustomButton bTurnLeft = new CustomButton(turnLeftCommand, true, false);
		leftContainerButtons.add(bTurnLeft);
		CustomButton bTurnRight = new CustomButton(turnRightCommand, true, false);
		leftContainerButtons.add(bTurnRight);
		CustomButton bTurnLauncherLeft = new CustomButton(turnLauncherLeftCommand, true, false);
		leftContainerButtons.add(bTurnLauncherLeft);
		CustomButton bTurnLauncherRight = new CustomButton(turnLauncherRightCommand, true, false);
		leftContainerButtons.add(bTurnLauncherRight);
		CustomButton bFirePsMissile = new CustomButton(firePsMissileCommand, true, false);
		leftContainerButtons.add(bFirePsMissile);
		CustomButton bFireNpsMissile = new CustomButton(fireNpsMissileCommand, true, false);
		leftContainerButtons.add(bFireNpsMissile);
		CustomButton bHyperJump = new CustomButton(hyperJumpCommand, true, false);
		leftContainerButtons.add(bHyperJump);
		CustomButton bReloadMissiles = new CustomButton(reloadMissilesCommand, true, false);
		leftContainerButtons.add(bReloadMissiles);
		CustomButton bTick = new CustomButton(tickCommand, true, false);
		leftContainerButtons.add(bTick);
		CustomButton bPause = new CustomButton(pauseCommand, true, true);
		leftContainerButtons.add(bPause);
		CustomButton bQuit = new CustomButton(quitCommand, true, true);
		leftContainerButtons.add(bQuit);
		CustomButton bRefuel = new CustomButton(refuelCommand, false, true);
		leftContainerButtons.add(bRefuel);
		
		// add commands to command container
		for (CustomButton b : leftContainerButtons)
			leftContainer.add(b);
		/*
		leftContainer.add(new CustomButton(addAsteroidCommand));
		leftContainer.add(new CustomButton(addNpsCommand));
		leftContainer.add(new CustomButton(addSpaceStationCommand));
		leftContainer.add(new CustomButton(addPsCommand));
		leftContainer.add(new CustomButton(accelerateCommand));
		leftContainer.add(new CustomButton(decelerateCommand));
		leftContainer.add(new CustomButton(turnLeftCommand));
		leftContainer.add(new CustomButton(turnRightCommand));
		leftContainer.add(new CustomButton(turnLauncherLeftCommand));
		leftContainer.add(new CustomButton(turnLauncherRightCommand));
		leftContainer.add(new CustomButton(firePsMissileCommand));
		leftContainer.add(new CustomButton(fireNpsMissileCommand));
		leftContainer.add(new CustomButton(hyperJumpCommand));
		leftContainer.add(new CustomButton(reloadMissilesCommand));
		leftContainer.add(new CustomButton(tickCommand));
		leftContainer.add(new CustomButton(pauseCommand));
		leftContainer.add(new CustomButton(quitCommand));
		leftContainer.add(new CustomButton(refuelCommand));
		*/
					
		// add command container to west portion of border layout
		this.add(BorderLayout.WEST, leftContainer);
		resume();
		
		this.show();
		
		// initialize the game world
		gw.init();
	}	
	
	/**
	 * Gets the width of the map (in pixels). 
	 * Determined by the width of the map view container.
	 * 
	 * @return width of map
	 */
	public static int getMapWidth() { return mapView.getWidth(); }
	
	/**
	 * Gets the height of the map (in pixels). 
	 * Determined by the height of the map view container.
	 * 
	 * @return height of map
	 */
	public static int getMapHeight() { return mapView.getHeight(); }

	/**
	 * Gets the number of milliseconds for each frame, based on the FPS
	 * @return number of milliseconds in each frame
	 */
	private int msPerFrame() {
		double ms = FPS;
		ms = Math.ceil(1000.0 / ms);
		return (int)ms;
	}
	
	public void pause() {
		for (CustomButton b : leftContainerButtons) {
			if (b.isEnabledOnPause())
				b.setEnabled(true);
			else
				b.setEnabled(false);			
		}		
		
		// Binding up arrow to increase ship speed (up arrow key code = -91)
		removeKeyListener(-91, accelerateCommand);
		// Binding down arrow to decrease ship speed (down arrow key code = -92)
		removeKeyListener(-92, decelerateCommand);
		// Binding left arrow to turn the ship left (left arrow key code = -93)
		removeKeyListener(-93, turnLeftCommand);
		// Binding right arrow to turn the ship right (right arrow key code = -94)
		removeKeyListener(-94, turnRightCommand);
		// Binding the space bar to fire a ship missile (space bar key code = -90) (this also binds the enter key)
		removeKeyListener(-90, firePsMissileCommand);
		// Binding < to turn launcher left (< key code = 44)
		removeKeyListener(44, turnLauncherLeftCommand);
		// Binding > to turn launcher right (> key code = 46)
		removeKeyListener(46, turnLauncherRightCommand);
		// Binding j to hyperjump (j key code = 106)
		removeKeyListener(106, hyperJumpCommand);
	}
	
	public void resume() {
		for (CustomButton b : leftContainerButtons) {
			if (b.isEnabledOnPlay())
				b.setEnabled(true);
			else
				b.setEnabled(false);			
		}	
		
		// Binding up arrow to increase ship speed (up arrow key code = -91)
		addKeyListener(-91, accelerateCommand);
		// Binding down arrow to decrease ship speed (down arrow key code = -92)
		addKeyListener(-92, decelerateCommand);
		// Binding left arrow to turn the ship left (left arrow key code = -93)
		addKeyListener(-93, turnLeftCommand);
		// Binding right arrow to turn the ship right (right arrow key code = -94)
		addKeyListener(-94, turnRightCommand);
		// Binding the space bar to fire a ship missile (space bar key code = -90) (this also binds the enter key)
		addKeyListener(-90, firePsMissileCommand);
		// Binding < to turn launcher left (< key code = 44)
		addKeyListener(44, turnLauncherLeftCommand);
		// Binding > to turn launcher right (> key code = 46)
		addKeyListener(46, turnLauncherRightCommand);
		// Binding j to hyperjump (j key code = 106)
		addKeyListener(106, hyperJumpCommand);
	}
	
	@Override
	public void run() {
		gw.tick(msPerFrame());
	}
		
}
