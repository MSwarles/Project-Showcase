package com.mycompany.a3.interfaces;

/**
 * Interface that allows other classes to tell the 
 * implementing object to move.
 */
public interface IMovable {
	/**
	 * Method that causes implementing object to update its 
	 * location based on its current speed and direction.
	 * @param elapsedMillisecs 
	 */
	public void move(int elapsedMillisecs);
}
