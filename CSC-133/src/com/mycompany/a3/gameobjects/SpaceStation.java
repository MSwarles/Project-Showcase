package com.mycompany.a3.gameobjects;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.Graphics;
import com.codename1.ui.geom.Point;
import com.mycompany.a3.GameWorld;
import com.mycompany.a3.interfaces.ICollider;
import com.mycompany.a3.interfaces.IDrawable;

/**
 * Space Stations are fixed objects that have an attribute
 * called blink rate; each space station blinks on and off
 * at this blink rate.
 * <p>
 * Space Stations also house an unlimited supply of missiles;
 * any ship arriving at a space station automatically gets a
 * full resupply of missiles (up to their maximum capacity).
 * 
 * @author Matt
 *
 */
public class SpaceStation extends FixedObject implements IDrawable, ICollider{
	/**
	 * Rate that station blinks
	 */
	private int blinkRate;
	/**
	 * Whether the Space Station light is currently on
	 */
	private boolean lightOn = false;
	/**
	 * Whether the Space Station has toggled its light already
	 */
	private boolean lightToggled = false;
	
	/**
	 * Class constructor.
	 */
	public SpaceStation() {
		this.size = 100;
		this.color = ColorUtil.YELLOW;
		this.blinkRate = 1 + rng.nextInt(4);
	}
	
	/**
	 * Gets the blink rate of the station.
	 * 
	 * @return value of blinkRate
	 */
	public int getBlinkRate() { return blinkRate; }
	
	/**
	 * Gets whether the light is currently on or not
	 * 
	 * @return if the light is on returns <code>true</code>,
	 * otherwise returns <code>false</code>.
	 */
	public boolean isLightOn() { return lightOn; }
	
	/**
	 * Toggles Space Station light on / off if the elapsed game time (passed
	 * in as a parameter) modded by the blink rate is equal to 0.
	 */
	public void toggleLight(int elapsedGameTime) {
		// blinkRate == 0 is to ensure elapsedGameTime isn't divided by 0.
		if (blinkRate == 0 || elapsedGameTime % blinkRate == 0) {
			if (!lightToggled) {
				lightOn = !lightOn;
				lightToggled = true;
			}
		}
		else {
			lightToggled = false;
		}
		
			
	}
	
	/**
	 * Override of GameObject.toString(). 
	 * 
	 * @return Type of object, along with super.toString() concatenated with 
	 * station blink rate.
	 */
	public String toString() {
		String parentDesc = super.toString();
		String myDesc = " rate=" + blinkRate;
		return "Station: " + parentDesc + myDesc;
	}

	// TODO: add desc
	@Override
	public void draw(Graphics g, Point pCmpRelPrnt) {
		g.setColor(color);
		int x = (int)(pCmpRelPrnt.getX() + getLocation().getX() - (size / 2));
		int y = (int)(pCmpRelPrnt.getY() + getLocation().getY() - (size / 2));
		
		if (isLightOn()) {
			g.fillArc(x, y, size, size, 0, 360);
		} else {
			g.drawArc(x, y, size, size, 0, 360);
		}
	}

	@Override
	public boolean collidesWith(ICollider otherObject) {
		// ignore space stations
		if (!(otherObject instanceof PlayerShip))
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
	public void handleCollision(ICollider otherObject, GameWorld gw) { }
	
}