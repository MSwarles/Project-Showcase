package com.mycompany.a3.gameobjects;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.Graphics;
import com.codename1.ui.geom.Point;
import com.mycompany.a3.Game;
import com.mycompany.a3.GameWorld;
import com.mycompany.a3.interfaces.ICollider;
import com.mycompany.a3.interfaces.IDrawable;
import com.mycompany.a3.interfaces.ISteerable;

/**
 * PlayerShip is a ship whose speed and direction may be controlled 
 * by a player. It's able to fire missiles from their missile 
 * launchers (if they have missiles).
 * <p>
 * <b>Note:</b> there should only ever be one player ship in the game 
 * at any one time, so it implements the Singleton design pattern. 
 * Its initial location should be the center of the world, with 
 * speed of 0 and direction of 0.
 * 
 * @author Matt
 * 
 */
public class PlayerShip extends Ship implements ISteerable, IDrawable, ICollider {
	/**
	 * Singleton instance of Player Ship.
	 */
	private static PlayerShip playerShip;
	/**
	 * Constant for the maximum speed of a Player Ship.
	 */
	private final int MAX_SPEED = 180;
	/**
	 * Constant used when using accelerate / decelerate
	 */
	private final int SPEED_INCREMENT = 30;
	/**
	 * Constant representing the maximum amount of missiles a PS can have.
	 */
	private final int MAX_MISSILES = 10;
	/**
	 * Constant used for turning the ship a fixed amount. 
	 */
	private final int TURN_SPEED = 15;
	/**
	 * Launcher used to fire missiles from the ship.
	 */
	private SteerableMissileLauncher missileLauncher;
	
	/**
	 * Class constructor that sets initial location of the ship to the
	 * center of the map, sets its speed and direction to 0, and gives
	 * it a color. 
	 * Also creates the ship's missile launcher.
	 * 
	 * @param mapWidth  Used to get the center x-coordinate of world
	 * @param mapHeight  Used to get the center y-coordinate of world
	 */
	private PlayerShip(int mapWidth, int mapHeight) {
		// set initial location to the center of the map
		this.setLocation(mapWidth / 2, mapHeight / 2);
		// set initial speed to 0
		this.speed = 0;
		// set initial direction to 0
		this.direction = 0;
		// set initial size
		this.size = 75;
		// set color to GREEN
		this.color = ColorUtil.GREEN;
		// set missile count to max capacity
		missileCount = MAX_MISSILES;
		// create the missile launcher, giving it the ship's location and speed.
		missileLauncher = new SteerableMissileLauncher(this.location, this.speed, this.direction);
	}
	
	/**
	 * Returns the instance of PlayerShip. If one doesn't exist, creates one first.
	 * 
	 * @return the Player Ship.
	 */
	public static PlayerShip getShip() {
		if (playerShip == null)
			playerShip = new PlayerShip(Game.getMapWidth(), Game.getMapHeight());
		
		playerShip.speed = 0;
		return playerShip;
	}
	
	/**
	 * Get the MissileLauncher's current direction
	 * 
	 * @return  The direction of the ship's missile launcher
	 */
	public int getLauncherDirection() { return missileLauncher.getDirection(); }
	
	/**
	 * Get the current number of missiles on the ship
	 * 
	 * @return  The amount of missiles on the ship
	 */	
	public int getMissileCount() { return missileCount; }
			
	/**
	 * Refills the missiles on the ship.
	 * 
	 * Sets missileCount to its maximum value. Maximum value is provided 
	 * by MAX_MISSILES.
	 */
	public void reloadMissiles() {
		missileCount = MAX_MISSILES;
	}
	
	/**
	 * Increments the speed of the ship by 1.
	 * <b>Note:</b> enforces a maximum speed of MAX_SPEED.
	 */
	public void increaseSpeed() {
		if (this.speed < MAX_SPEED) this.speed += SPEED_INCREMENT;
	}
	
	/**
	 * Decrements the speed of the ship by 1.
	 * <b>Note:</b> enforces minimum speed of 0.
	 */
	public void decreaseSpeed() {
		if (this.speed > 0) this.speed -= SPEED_INCREMENT;
		
		if (this.speed < 0) this.speed = 0;
	}
	
	/**
	 * Turns ship to the left by adjusting its direction a fixed amount.
	 * <b>Note:</b>Ensures direction is a valid degree between [0..359].
	 */
	public void turnLeft() {
		this.direction = (this.direction + TURN_SPEED) % 360;
	}
	
	/**
	 * Turns ship to the right by adjusting its direction a fixed amount.
	 * <b>Note:</b>Ensures direction is a valid degree between [0..359].
	 */
	public void turnRight() {
		this.direction = (this.direction - TURN_SPEED) % 360;
		
		if (this.direction < 0) {
			this.direction += 360;
		}
	}
	
	/**
	 * Turns ship's missile launcher to the left.
	 */
	public void turnLauncherLeft() {
		missileLauncher.turnLeft();
	}
	
	/**
	 * Turns ship's missile launcher to the right.
	 */
	public void turnLauncherRight() {
		missileLauncher.turnRight();
	}
	
	/**
	 * Updates location based on speed and direction.
	 */
	public void move(int elapsedMillisecs) {
		int theta = 90 - this.direction;
		double deltaX = Math.cos(Math.toRadians(theta)) * this.speed * ((double)elapsedMillisecs / 1000);
		double deltaY = Math.sin(Math.toRadians(theta)) * this.speed * ((double)elapsedMillisecs / 1000);
		this.location.setX(this.location.getX() + deltaX);
		this.location.setY(this.location.getY() + deltaY);
		
		// make object loop to other side if it hits window bounds
		if (this.location.getX() < 0)
			this.setLocation(Game.getMapWidth() - 1, this.location.getY());
		else if (this.location.getX() > Game.getMapWidth())
			this.setLocation(1, this.location.getY());
		
		if (this.location.getY() < 0)
			this.setLocation(this.location.getX(), Game.getMapHeight());
		else if (this.location.getY() > Game.getMapHeight())
			this.setLocation(this.location.getX(), 1);
		
		missileLauncher.setLocation(this.location.getX(), this.location.getY());
	}

	/**
	 * Draws the PlayerShip (as an unfilled triangle)
	 */
	@Override
	public void draw(Graphics g, Point pCmpRelPrnt) {
		g.setColor(color);
		g.drawPolygon(getXPoints(pCmpRelPrnt), getYPoints(pCmpRelPrnt), 5);
		missileLauncher.draw(g, pCmpRelPrnt);
	}

	/**
	 * Compares this ship's location with another object. Returns
	 * <code> true </code> if they are colliding, otherwise returns
	 * <code> false </code>.
	 */
	@Override
	public boolean collidesWith(ICollider otherObject) {
		// ignore player missiles
		if (otherObject instanceof Missile) {
			Missile m = (Missile)otherObject;
			if (m.isPlayerMissile()) {
				return false;
			}
		}
		
		boolean result = false;	
		GameObject other = (GameObject)otherObject;		
		
		// check left and right overlap
		if (this.getRight() > other.getLeft() && this.getLeft() < other.getRight()) {
			// check top and bottom overlap
			if (this.getBottom() > other.getTop() && this.getTop() < other.getBottom()) {
				// both overlap, so collision is occurring
				result = true;
			}
		}
		
		return result;
	}

	@Override
	public void handleCollision(ICollider otherObject, GameWorld gw) {
		if (otherObject instanceof SpaceStation) {
			gw.reloadPsMissiles();
		}
		else {
			gw.playerDeath(this);
		}
	}
	
	public void init() {
		playerShip = new PlayerShip(Game.getMapWidth(), Game.getMapHeight());
	}
	
	/**
	 * Override of GameObject.toString(). 
	 * <p>
	 * Returns a String with a detailed description of the PS.
	 * 
	 * @return Type of object, along with super.toString() concatenated with 
	 * ship speed, direction, missile count, and launcher direction.
	 */
	public String toString() {
		String parentDesc = super.toString();
		String myDesc = " speed=" + this.speed + " dir=" + this.direction 
				+ " missiles=" + missileCount + " MissileLauncher dir=" + missileLauncher.getDirection();
		
		return "Player Ship: " + parentDesc + myDesc;		
	}
	
}