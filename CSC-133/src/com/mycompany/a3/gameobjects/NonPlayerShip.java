package com.mycompany.a3.gameobjects;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.Graphics;
import com.codename1.ui.geom.Point;
import com.mycompany.a3.Game;
import com.mycompany.a3.GameWorld;
import com.mycompany.a3.interfaces.ICollider;
import com.mycompany.a3.interfaces.IDrawable;

/**
 * NonPlayerShips are ships that fly through space at a fixed speed and 
 * direction and CANNOT be steered. NPS also have a fixed attribute size, 
 * which gives its dimensions.  
 * <p>
 * NonPlayerShips are able to fire missiles from their missile launchers 
 * (if they have missiles).
 * <p>
 * NonPlayerShips award points when hit by a missile fired from PlayerShip.
 * 
 * @author Matt
 *
 */
public class NonPlayerShip extends Ship implements IDrawable, ICollider {
	/**
	 * Amount of points awarded by NPS if shot by PS
	 */
	private int points = 50;
	/**
	 * Launcher used to fire missiles from the ship.
	 */
	private MissileLauncher missileLauncher;
	
	/**
	 * Class constructor that gives the ship a random speed between 0 and 10,
	 * a random direction between 0 and 359, a random size between 10 and 20,
	 * and a color.
	 * Also creates the ship's missile launcher.
	 */
	public NonPlayerShip() {
		// set the size to a random number between [75..100]
		this.size = 75 + rng.nextInt(26);
		this.color = ColorUtil.argb(255, 255, 0, 0);
		missileCount = 2;
		
		// generate a spawn section
		int spawnSection = rng.nextInt(4);
		// variance when spawning (if spawn is on north border, then direction would be south-facing, 
		// between 105 to 255 degrees)
		int spawnDirectionVariance = 140;
		if (spawnSection == 0) {	// spawn on north border and face south-ish
			this.setLocation(Game.getMapWidth() / 6 + rng.nextInt(Game.getMapWidth() * 4 / 6), 1 - (size / 2));
			this.direction = 105 + rng.nextInt(spawnDirectionVariance);
		} else if (spawnSection == 1) {	// spawn on south border
			this.setLocation(Game.getMapWidth() / 6 + rng.nextInt(Game.getMapWidth() * 4 / 6), Game.getMapHeight() - 1 + (size / 2));
			this.direction = (285 + rng.nextInt(spawnDirectionVariance)) % 360;
		} else if (spawnSection == 2) {	// spawn on east border
			this.setLocation(Game.getMapWidth() - 1 + (size / 2), Game.getMapHeight() / 6 + rng.nextInt(Game.getMapHeight() * 4 / 6));
			this.direction = 15 + rng.nextInt(spawnDirectionVariance);
		} else if (spawnSection == 3) {	// spawn on west border
			this.setLocation(1 - (size / 2), Game.getMapHeight() / 6 + rng.nextInt(Game.getMapHeight() * 4 / 6));
			this.direction = 195 + rng.nextInt(spawnDirectionVariance);
		}
		
		missileLauncher = new MissileLauncher(this.location, this.speed, this.direction);
	}
	
	/**
	 * Get amount of points NPS is worth.
	 * 
	 * @return amount of points
	 */
	public int getPoints() { return points; }
	
	/**
	 * Get the MissileLauncher's current direction
	 * 
	 * @return  The direction of the ship's missile launcher
	 */
	public int getLauncherDirection() {	return missileLauncher.getDirection(); }
	
	/**
	 * Updates location based on speed and direction.
	 */
	public void move(int elapsedMillisecs) {
		int theta = 90 - this.direction;
		double deltaX = Math.cos(Math.toRadians(theta)) * this.speed * ((double)elapsedMillisecs / 1000);
		double deltaY = Math.sin(Math.toRadians(theta)) * this.speed * ((double)elapsedMillisecs / 1000);
		this.location.setX(this.location.getX() + deltaX);
		this.location.setY(this.location.getY() + deltaY);
		
		// make object loop to other side of map if it hits window bounds
		if (this.location.getX() < 0)
			this.setLocation(Game.getMapWidth() - 1, this.location.getY());
		else if (this.location.getX() > Game.getMapWidth())
			this.setLocation(1, this.location.getY());
		
		if (this.location.getY() < 0)
			this.setLocation(this.location.getX(), Game.getMapHeight());
		else if (this.location.getY() > Game.getMapHeight())
			this.setLocation(this.location.getX(), 1);
	}
	
	/**
	 * Override of GameObject.toString(). 
	 * 
	 * Returns a String with a detailed description of the NPS.
	 * 
	 * @return Type of object, along with super.toString() concatenated with 
	 * NPS speed, direction, and size.
	 */
	public String toString() {
		String parentDesc = super.toString();
		String myDesc = " speed=" + speed + " dir=" + direction 
				+ " size=" + size;
		return "Non-Player Ship: " + parentDesc + myDesc;
	}

	/**
	 * Draws the NonPlayerShip
	 */
	@Override
	public void draw(Graphics g, Point pCmpRelPrnt) {
		g.setColor(color);
		g.fillPolygon(getXPoints(pCmpRelPrnt), getYPoints(pCmpRelPrnt), 5);		
	}

	/**
	 * Checks to see if this object collides with otherObject
	 */
	@Override
	public boolean collidesWith(ICollider otherObject) {
		// ignore space stations
		if (otherObject instanceof SpaceStation)
			return false;
		
		// ignore non-player missiles
		if (otherObject instanceof Missile) {
			Missile m = (Missile)otherObject;
			if (!m.isPlayerMissile()) {
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

	/**
	 * Handles collisions after they've been detected
	 */
	@Override
	public void handleCollision(ICollider otherObject, GameWorld gw) {
		// if collision is a player missile, add points
		if (otherObject instanceof Missile) {
			Missile m = (Missile)otherObject;
			if (m.isPlayerMissile())
				gw.addPoints(this.points);
		}
		
		gw.npsDeath(this);	
	}
	
}