package com.mycompany.a3.gameobjects;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.Graphics;
import com.codename1.ui.geom.Point;
import com.mycompany.a3.Game;
import com.mycompany.a3.GameWorld;
import com.mycompany.a3.interfaces.ICollider;
import com.mycompany.a3.interfaces.IDrawable;
import com.mycompany.a3.interfaces.ISelectable;

/**
 * Asteroids are movable objects that tumble through
 * space at a fixed speed and direction. The size, speed,
 * and direction of an Asteroid are randomly generated upon
 * its creation.
 * <p>
 * Asteroids award points when hit by a missile fired from 
 * a PlayerShip.
 * 
 * @author Matt
 *
 */
public class Asteroid extends MovableObject implements IDrawable, ICollider, ISelectable {
	/**
	 * Amount of points awarded by Asteroid if shot by PS
	 */
	private int points = 20;
	/**
	 * Whether the asteroid is currently selected
	 */
	private boolean selected = false;
	/**
	 * Color when selected
	 */
	private int selectedColor = ColorUtil.CYAN;

	/**
	 * Class constructor that gives the Asteroid a random size and color.
	 */
	public Asteroid() {
		// set size to a random number between [60..125]
		this.size = 60 + rng.nextInt(66);
		// set color to light gray
		this.color = ColorUtil.LTGRAY;
		
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
	}
	
	/**
	 * Gets amount of points Asteroid is worth.
	 * 
	 * @return amount of points
	 */
	public int getPoints() { return points; }
	
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
	}

	@Override
	public void draw(Graphics g, Point pCmpRelPrnt) {
		int drawColor = isSelected() ? selectedColor : color;
		g.setColor(drawColor);
		int x = (int)(pCmpRelPrnt.getX() + getLocation().getX() - (size / 2));
		int y = (int)(pCmpRelPrnt.getY() + getLocation().getY() - (size / 2));
		g.fillArc(x, y, size, size, 0, 360);
	}

	@Override
	public boolean collidesWith(ICollider otherObject) {
		// ignore space stations
		if (otherObject instanceof SpaceStation)
			return false;
		
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
		if (otherObject instanceof Missile) {
			Missile m = (Missile)otherObject;
			if (m.isPlayerMissile())
				gw.addPoints(this.points);
		}
		
		gw.asteroidDeath(this);
	}

	@Override
	public void setSelected(boolean selected) { this.selected = selected; }

	@Override
	public boolean isSelected() { return this.selected; }

	@Override
	public boolean contains(int x, int y) {
		boolean result = false;
		int leftX = getLeft();
		int rightX = getRight();
		int bottomY = getBottom();
		int topY = getTop();
		if (x >= this.getLeft() && x <= this.getRight() &&
				y <= this.getBottom() && y >= this.getTop())
			result = true;
		
		return result;
	}
	
	/**
	 * Override of GameObject.toString(). 
	 * 
	 * Returns a String with a detailed description of the Asteroid.
	 * 
	 * @return Type of object, along with super.toString() concatenated with 
	 * asteroid speed, direction, and size.
	 */
	public String toString() {
		String parentDesc = super.toString();
		String myDesc = " speed=" + speed + " dir=" + direction + " size=" + size;
		return "Asteroid: " + parentDesc + myDesc; 
	}
}