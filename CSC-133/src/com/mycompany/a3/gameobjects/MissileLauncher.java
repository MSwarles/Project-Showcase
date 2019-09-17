package com.mycompany.a3.gameobjects;

import com.codename1.ui.geom.Point2D;

/**
 * Non-player Missile Launcher is used to determine which direction to fire 
 * missiles. Its speed, location, and direction match that of a NonPlayerShip.
 * 
 * @author Matt
 *
 */
public class MissileLauncher extends MovableObject{
	/**
	 * Class constructor that sets launcher's location, speed, and direction 
	 * to the same as the ship it belongs to, as launchers are 'attached' 
	 * to ships.
	 * 
	 * @param shipLoc  Location of the ship the launcher belongs to.
	 * @param shipSpeed  Speed of the ship the launcher belongs to.
	 * @param shipDirection  Direction of the ship the launcher belongs to.
	 */
	public MissileLauncher(Point2D shipLoc, int shipSpeed, int shipDirection) {
		// sets launchers location to ship's location
		this.location = shipLoc;
		// sets launchers speed to ship's speed
		this.speed = shipSpeed;
		// set launchers direction to ship's direction
		this.direction = shipDirection;
	}

	/**
	 * Gets the launcher's current direction.
	 *  
	 * @return Direction of launcher.
	 */
	public int getDirection() { return direction; }
	
	/**
	 * Updates location based on speed and direction.
	 */
	public void move(int elapsedMillisecs) { }
}
