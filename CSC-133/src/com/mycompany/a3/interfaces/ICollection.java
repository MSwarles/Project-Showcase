package com.mycompany.a3.interfaces;

/**
 * Interface that allows adding of GameObjects to a collection, and access to
 * an iterator for that collection.
 * 
 * @author Matt
 *
 */
public interface ICollection {
	/**
	 * Adds an object to the collection
	 * @param newObject the object to be added
	 */
	public void add(Object newObject);
	/**
	 * Removes an object from the collection
	 * @param object the object to be removed
	 */
	public void remove(Object object);
	/**
	 * Gets the iterator for the collection, which allows
	 * iteration of the collection.
	 * @return
	 */
	public IIterator getIterator();
	
}
