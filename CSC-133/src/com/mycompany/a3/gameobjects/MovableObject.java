package com.mycompany.a3.gameobjects;

import com.mycompany.a3.interfaces.IMovable;

/**
 * Movable objects have attributes speed and direction, which are
 * used to define how they move through the world when told to.
 * <p>
 * Speed is assigned a random value between 0 and 10, inclusive, and direction is
 * assigned a random value between 0 and 359, inclusive.
 * 
 * @author Matt
 *
 */
public abstract class MovableObject extends GameObject implements IMovable {
	/**
	 * The speed of an object
	 */
	protected int speed;
	/**
	 * The direction the object is heading
	 */
	protected int direction;
	
	/**
	 * Class constructor.
	 */
	public MovableObject() {
		// gives a speed between [25..100]
		speed = 25 + rng.nextInt(76);
		// gives a direction between [0..359]
		direction = rng.nextInt(360);
	}
}
