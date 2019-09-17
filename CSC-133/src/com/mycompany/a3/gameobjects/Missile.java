package com.mycompany.a3.gameobjects;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.Graphics;
import com.codename1.ui.geom.Point;
import com.codename1.ui.geom.Point2D;
import com.mycompany.a3.Game;
import com.mycompany.a3.GameWorld;
import com.mycompany.a3.interfaces.ICollider;
import com.mycompany.a3.interfaces.IDrawable;
import com.mycompany.a3.interfaces.ISelectable;

/**
 * Missiles are movable objects that are fired from a ship's MissileLauncher.
 * When a missile is fired, its direction matches its ship's MissileLauncher,
 * and has a speed that is slightly faster than the ship. 
 * <p>
 * Missiles also have the attribute <code>fuelLevel</code>, a positive integer that
 * decrements as time passes. When fuel reaches 0, the missile is removed
 * from the game.
 * <p>
 * Missiles cannot hit the ship type that fired them.
 * 
 * @author Matt
 *
 */
public class Missile extends MovableObject implements IDrawable, ICollider, ISelectable {
	/**
	 * Constant that is added to a missiles speed to make missiles faster than
	 * the ship that fired it.
	 */
	private final int EXTRA_SPEED = 200;
	/**
	 * Amount of fuel the missile currently has.
	 */
	private float fuelLevel;
	/**
	 * Constant used for max fuel level
	 */
	private final float MAX_FUEL = 10f;
	/**
	 * Tells whether the missile was fired by a player or a non-player.
	 * Set to <code>true</code> if fired by Player Ship, otherwise 
	 * <code>false</code>.
	 */
	private boolean playerMissile;
	/**
	 * Whether the missile is currently selected
	 */
	private boolean selected = false;
	/**
	 * Color of object when selected
	 */
	private int selectedColor = ColorUtil.CYAN;
	
	/**
	 * Class constructor
	 * @param initialX - Specifies x-value of initial location. 
	 * @param initialY - Specifies y-value of initial location.
	 * @param shipSpeed - Speed of ship that fired the missile.
	 * @param dir - Direction the missile will travel.
	 * @param firedByPlayer - <code>true</code> if missile was fired by Player Ship.
	 */
	public Missile(double initialX, double initialY, int shipSpeed, int dir, 
			boolean firedByPlayer) {
		// adds speed to the missile so it's faster than the ship that fired it
		this.speed = shipSpeed / 10 + EXTRA_SPEED;
		this.direction = dir;
		this.size = 30;
		this.playerMissile = firedByPlayer;
		this.color = (firedByPlayer ? ColorUtil.WHITE : ColorUtil.MAGENTA);
		this.fuelLevel = MAX_FUEL;
		
		this.location = new Point2D(initialX - size / 4, initialY + size / 2);
	}
	
	/**
	 * Gets the current fuel level.
	 * 
	 * @return current fuel level
	 */
	public float getFuel() { return fuelLevel; }
	
	/**
	 * Gets whether the missile was fired by a PlayerShip.
	 * 
	 * @return If it was fired by a PlayerShip, returns <code>true</code>.
	 * Otherwise returns <code>false</code>.
	 */
	public boolean isPlayerMissile() { return playerMissile; }
	
	/**
	 * Resets the fuelLevel to the maximum.
	 */
	public void refuel() { fuelLevel = MAX_FUEL; }
	
	/**
	 * Decrements fuel level by 1, and updates location based on 
	 * speed and direction.
	 */
	public void move(int elapsedMillisecs){
		// make missile lose fuel
		fuelLevel -= (float)elapsedMillisecs / 400;
		
		// adjust location based on speed, direction, and elapsed time
		int theta = 90 - this.direction;
		double deltaX = Math.cos(Math.toRadians(theta)) * this.speed * ((double)elapsedMillisecs / 1000);
		double deltaY = Math.sin(Math.toRadians(theta)) * this.speed * ((double)elapsedMillisecs / 1000);
		this.location.setX(this.location.getX() + deltaX);
		this.location.setY(this.location.getY() + deltaY);
		
		// make missile loop to other side if it hits window bounds
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
	 * Draws the missile
	 * @param g
	 * @param pCmpRelPrnt
	 */
	@Override
	public void draw(Graphics g, Point pCmpRelPrnt) {
		int drawColor = isSelected() ? selectedColor : color;
		g.setColor(drawColor);
		int x = (int)(pCmpRelPrnt.getX() + getLocation().getX());
		int y = (int)(pCmpRelPrnt.getY() + getLocation().getY());
		g.fillRect(x, y, size/2, size);		
	}

	/**
	 * Checks if missile is colliding with otherObject. returns <code>true</code>
	 * if collision is detected, otherwise returns <code>false</code>.
	 * @param otherObject
	 * @return true if collision, false if not
	 */
	@Override
	public boolean collidesWith(ICollider otherObject) {
		// ignore space stations
		if (otherObject instanceof SpaceStation)
			return false;
		// ignore player ship if player missile
		if (otherObject instanceof PlayerShip && this.isPlayerMissile())
			return false;
		// ignore non player ship if non player missile
		if (otherObject instanceof NonPlayerShip && !this.isPlayerMissile())
			return false;
		// stop missiles from same team from hitting each other
		if (otherObject instanceof Missile) {
			Missile m = (Missile)otherObject;
			if (this.isPlayerMissile() == m.isPlayerMissile())
				return false;
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
		gw.removeObject(this);
	}

	@Override
	public void setSelected(boolean selected) { this.selected = selected; }

	@Override
	public boolean isSelected() { return this.selected; }

	@Override
	public boolean contains(int x, int y) {
		boolean result = false;
		if (x >= this.getLeft() && x <= this.getRight() &&
				y <= this.getBottom() && y >= this.getTop())
			result = true;
		
		return result;
	}
	
	
	/**
	 * Override of GameObject.toString(). 
	 * 
	 * @return Who fired object and object type, along with super.toString() 
	 * concatenated with missile speed, direction, and fuel level.
	 */
	public String toString() {
		String parentDesc = super.toString();
		double rdFuelLevel = Math.round(fuelLevel * 100.0) / 100.0;
		String myDesc = " speed=" + this.speed + " dir=" + this.direction + " fuel=" + rdFuelLevel;
		return (playerMissile ? "PS's Missile: " : "NPS's Missile: ") + parentDesc + myDesc;
	}
	
}