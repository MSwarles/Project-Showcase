package com.mycompany.a3.gameobjects;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.Graphics;
import com.codename1.ui.geom.Point;
import com.codename1.ui.geom.Point2D;
import com.mycompany.a3.interfaces.IDrawable;
import com.mycompany.a3.interfaces.ISteerable;

/**
 * Player Missile Launcher is a steerable object used to determine which 
 * direction to fire missiles. Its speed and location match that of a 
 * PlayerShip, while its direction may be different.
 *
 * @author Matt
 *
 */
public class SteerableMissileLauncher extends MissileLauncher implements ISteerable, IDrawable {
	/**
	 * Constant used for turning the launcher by a fixed amount.
	 */
	private final int TURN_SPEED = 15;
	/**
	 * Class constructor that sets launcher's location and speed to the
	 * same as the ship it belongs to, since launchers are 'attached' to 
	 * ships.
	 * 
	 * @param shipLoc  Location of the ship that the launcher belongs to.
	 * @param shipSpeed  Speed of the ship that the launcher belongs to. 
	 * @param shipDirection  Direction of the ship that the launcher belongs to.
	 */
	public SteerableMissileLauncher(Point2D shipLoc, int shipSpeed, int shipDirection) {
		super(shipLoc, shipSpeed, shipDirection);
		// sets launcher's location to the ship's location
		this.location = shipLoc;
		this.speed = 0;
		this.direction = 0;
		this.color = ColorUtil.WHITE;
	}
	
	/**
	 * Turns launcher to the left by adjusting its direction a fixed amount.
	 * <b>Note:</b>Ensures direction is a valid degree between [0..359].
	 */
	public void turnLeft() {
		this.direction = (this.direction + TURN_SPEED) % 360;
	}
	
	/**
	 * Turns launcher to the right by adjusting its direction a fixed amount.
	 * <b>Note:</b>Ensures direction is a valid degree between [0..359].
	 */
	public void turnRight() {
		this.direction = (this.direction - TURN_SPEED) % 360;
		
		if (this.direction < 0) {
			this.direction += 360;
		}
	}
	
	/**
	 * Updates location based on speed and direction.
	 */
	public void move() { }

	@Override
	public void draw(Graphics g, Point pCmpRelPrnt) {
		int lineLength = 75;
		int theta = 90 - this.direction;
		double deltaX = getLocation().getX() + (lineLength * Math.cos(Math.toRadians(theta)));
		double deltaY = getLocation().getY() + (lineLength * Math.sin(Math.toRadians(theta)));
		g.setColor(color);
		g.drawLine((int)(pCmpRelPrnt.getX() + getLocation().getX()), 
				(int)(pCmpRelPrnt.getY() + getLocation().getY()), 
				(int)(pCmpRelPrnt.getX() + deltaX), 
				(int)(pCmpRelPrnt.getY() + deltaY));		
	}
}