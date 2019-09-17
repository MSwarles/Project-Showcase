package com.mycompany.a3.interfaces;

/**
 * Interface that allows other classes to tell the implementing object
 * to change its direction.
 */
public interface ISteerable {
	/**
	 * Method that causes implementing object to adjust its direction left.
	 */
	public void turnLeft();
	/**
	 * Method that causes implementing object to adjust its direction right.
	 */
	public void turnRight();
}
