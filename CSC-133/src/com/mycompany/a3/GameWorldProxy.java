package com.mycompany.a3;

import java.util.Observable;

import com.mycompany.a3.interfaces.IGameWorld;

/**
 * GameWorldProxy allows views access to the GameWorld and get access to
 * the information they need to update, while preventing them from modifying it.
 * 
 * @author Matt
 *
 */
public class GameWorldProxy extends Observable implements IGameWorld {
	/**
	 * GameWorld reference
	 */
	private GameWorld gw;
	
	/**
	 * Class constructor
	 * @param gw the game world
	 */
	public GameWorldProxy (GameWorld gw) {
		this.gw = gw;
	}

	@Override
	public int getLives() {	return gw.getLives(); }

	@Override
	public int getMissiles() { return gw.getMissiles(); }

	@Override
	public int getGameTime() { return gw.getGameTime(); }

	@Override
	public int getPoints() { return gw.getPoints(); }
	
	@Override
	public boolean isSoundOn() { return gw.isSoundOn(); }
	
	@Override
	public void setSound(boolean on) { gw.setSound(on); }
	
	@Override
	public void printMap() { gw.printMap(); }

	@Override
	public void spawnPs() { }	// no functionality in proxy
	@Override
	public void spawnNps() { }	// no functionality in proxy
	@Override
	public void spawnAsteroid() { }	// no functionality in proxy
	@Override
	public void spawnSpaceStation() { }	// no functionality in proxy
	@Override
	public void increasePsSpeed() {	}	// no functionality in proxy
	@Override
	public void decreasePsSpeed() { }	// no functionality in proxy
	@Override
	public void turnPsLeft() { }	// no functionality in proxy
	@Override
	public void turnPsRight() {	}	// no functionality in proxy
	@Override
	public void turnPsLauncherLeft() { }	// no functionality in proxy
	@Override
	public void turnPsLauncherRight() { }	// no functionality in proxy
	@Override
	public void firePsMissile() { }	// no functionality in proxy
	@Override
	public void fireNpsMissile() { }	// no functionality in proxy
	@Override
	public void hyperJump() { }	// no functionality in proxy
	@Override
	public void reloadPsMissiles() { }	// no functionality in proxy
	@Override
	public void tick(int elapsedMillisecs) { }	// no functionality in proxy

}
