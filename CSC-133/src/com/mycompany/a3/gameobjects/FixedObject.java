package com.mycompany.a3.gameobjects;

/**
 * Fixed objects have unique integer identification (id)
 * numbers; every fixed object has an id which is different
 * from every other fixed object.
 * 
 * @author Matt
 *
 */
public abstract class FixedObject extends GameObject {
	/**
	 * Unique ID used to identify each FixedObject
	 */
	protected static int id = 0;
	
	/**
	 * Class constructor. Assigns a value to id.
	 */
	public FixedObject() {
		id = id++;
	}
	
}