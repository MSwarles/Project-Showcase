package com.mycompany.a3.gameobjects;

import com.codename1.ui.geom.Point;

/**
 * Ships are movable objects with a speed and direction. They're
 * able to fire missiles from their missile launchers.
 * 
 * @author Matt
 *
 */
public abstract class Ship extends MovableObject {
	/**
	 * Number of missiles the ship has.
	 */
	protected int missileCount;
	
	/**
	 * Class constructor.
	 */
	public Ship() {	}
	
	/**
	 * Get the current speed 
	 * 
	 * @return  Speed of the ship
	 */
	public int getSpeed() {	return speed; }
	
	/**
	 * Fires a missile if the ship has any.
	 * 
	 * <b>Note:</b> this only verifies that a ship may fire a missile.
	 * A missile object must still be created in the game world.
	 * 
	 * @return If missileCount > 0, decrements missileCount by 1 and returns
	 * 		<code>true</code>. Otherwise returns <code>false</code>.
	 */
	public boolean fireMissile() {
		if (missileCount > 0) {
			missileCount--;
			return true;
		}
		
		return false;
	}
	
	/**
	 * Returns array of x points needed for drawing triangle
	 * @param pCmpRelPrnt
	 * @return
	 */
	protected int[] getXPoints(Point pCmpRelPrnt) {
		int bottomCenterX = (int)(pCmpRelPrnt.getX() + getLocation().getX());
		int topRightX = pCmpRelPrnt.getX() + getRight();
		int topLeftX = pCmpRelPrnt.getX() + getLeft();
		int leftMidX = pCmpRelPrnt.getX() + getLeft() + size / 4;
		int rightMidX = pCmpRelPrnt.getX() + getLeft() + size * 3 / 4;
		int[] xPoints = new int[] {	bottomCenterX, topRightX, rightMidX, leftMidX, topLeftX };
		return xPoints;
	}
	
	/**
	 * Returns array of y points needed for drawing triangle
	 * @param pCmpRelPrnt
	 * @return
	 */
	protected int[] getYPoints(Point pCmpRelPrnt) {
		int topY = pCmpRelPrnt.getY() + getTop();
		int bottomY = pCmpRelPrnt.getY() + getBottom();
		int topMidY = pCmpRelPrnt.getY() + getTop() + size / 4;
		int[] yPoints = new int[] { bottomY, topY, topMidY, topMidY, topY };
		return yPoints;
	}
}
