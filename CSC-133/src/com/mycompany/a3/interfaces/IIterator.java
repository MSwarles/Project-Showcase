package com.mycompany.a3.interfaces;

/**
 * Interface that provides functionality required of an iterator,
 * including checking to see if a collection has another object, 
 * and retrieving the next object.
 * 
 * @author Matt
 *
 */
public interface IIterator {
	/**
	 *  Checks to see if the collection has more objects
	 */
	public boolean hasNext();
	/**
	 * gets the next object in the collection
	 * @return the next object in the collection
	 */
	public Object getNext();
}
