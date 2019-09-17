package com.mycompany.a3.gameobjects;

import java.util.ArrayList;
import java.util.Random;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.geom.Point2D;
import com.mycompany.a3.Game;

/**
 * GameObject is an abstract class that provides subclasses with a location,
 * color. By default, game objects have the ability to change their location. 
 * <p>
 * GameObjects are initially created with a random location, whose x and y are
 * limited by the size of the game world.
 * 
 * @author Matt
 *
 */
public abstract class GameObject {	
	/**
	 * Location of the object
	 */
	protected Point2D location;
	/**
	 * Color of the object
	 */
	protected int color;	
	/**
	 * Size of the object 
	 */
	protected int size;
	/**
	 * Used to generate pseudo-random numbers.
	 */
	protected Random rng = new Random();
	
	/**
	 * Class constructor that gives the object a random location within the game 
	 * world.
	 */
	public GameObject() {
		location = new Point2D(
				rng.nextInt(Game.getMapWidth()), 
				rng.nextInt(Game.getMapHeight()));	
	}
	
	/**
	 * Get the gameobject's location (center).
	 * 
	 * @return  The Point2D data type that is the current location
	 */
	public Point2D getLocation() { return location; }
	
	/** 
	 * Get the value of color.
	 * 
	 * @return  The integer representation of the current color
	 */
	public int getColor() {	return color; }
	
	/**
	 * Get the gameobject's top bound
	 * @return the top bound
	 */
	public int getTop() { return (int)(location.getY() - (size / 2)); }
	
	/**
	 * Get the gameobject's right bound
	 * @return the right bound
	 */
	public int getRight() { return (int)(location.getX() + (size / 2)); }
	
	/**
	 * Get the gameobject's left bound
	 * @return the left bound
	 */
	public int getLeft() { return (int)(location.getX() - (size / 2)); }
	
	/**
	 * Get the gameobject's bottom bound
	 * @return the bottom bound
	 */
	public int getBottom() { return (int)(location.getY() + (size / 2)); }
	
	/**
	 * Set current location to a new location.
	 * 
	 * @param x  Specifies the x-coordinate of the new location
	 * @param y  Specifies the y-coordinate of the new location
	 */
	public void setLocation(double x, double y) {
		location = new Point2D(x, y);
	}
	
	/**
	 * Set the value of color.
	 * 
	 * @param newColor  The integer representation of the new color
	 */
	public void setColor(int newColor) {
		color = newColor;
	}
	
	/**
	 * Override of object.toString(). 
	 * Returns rounded x-value and y-value of location along with 
	 * value of color.
	 * 
	 * @return rounded values of location, value color 
	 */
	public String toString() {
		double rdX = Math.round(location.getX() * 100.0)/100.0;
		double rdY = Math.round(location.getY() * 100.0)/100.0;
		return "loc=" + rdX + "," + rdY + " color=[" + ColorUtil.red(color) + ","
			+ ColorUtil.green(color) + "," + ColorUtil.blue(color) + "]";
	}
	
}
