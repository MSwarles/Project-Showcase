package com.mycompany.a3;

import java.util.ArrayList;

import com.mycompany.a3.interfaces.ICollection;
import com.mycompany.a3.interfaces.IIterator;

/** GameObjectCollection contains a collection of game objects and implements 
 * the Iterator design pattern. 
 * <p>
 * It provides an iterator for accessing the objects in the collection.
 * 
 * @author Matt
 *
 */
public class GameObjectCollection implements ICollection {
	/**
	 * An ArrayList of game objects
	 */
	private ArrayList<Object> gameObjectList;
		
	/**
	 * Class constructor
	 */
	public GameObjectCollection() {
		gameObjectList = new ArrayList<Object>();
	}

	/**
	 * Adds a game object to the collection
	 */
	@Override
	public void add(Object newObject) {
		gameObjectList.add(newObject);
	}
	
	/**
	 * Removes a game object from the collection.
	 * 
	 * @param object the object to be removed from the collection.
	 */
	public void remove(Object object) {
		gameObjectList.remove(object);
	}
	
	/**
	 * Gets a new iterator object, which is used for iterating through the collection.
	 */
	@Override
	public IIterator getIterator() {
		return new GameObjectIterator();
	}

	private class GameObjectIterator implements IIterator {
		/**
		 * The current index of the iterator
		 */
		private int currElementIndex;
		/**
		 * Clone of the gameObjectList
		 */
		private ArrayList<Object> clone;
		
		public GameObjectIterator() {
			// initialize index to -1 so iteration begins at first index of collection
			currElementIndex = -1;
			// clone gameObjectList to avoid conflicts when removing items during iteration
			clone = new ArrayList<Object>(gameObjectList);
		}
		  
		/**
		 * Checks to see if the collection has an object at the next index.
		 * Returns <code>true</code> if it does, otherwise returns <code>false</code>.
		 * 
		 * @return If collection has an object at next index, returns <code>true</code>,
		 * otherwise returns <code>false</code>.
		 */
		@Override
		public boolean hasNext() {
			// if gameObjectList is empty, return false
			if (clone.size() <= 0) 
				return false;
			
			// if current index is at the end of the gameObjectList, return false
			if (currElementIndex == clone.size() - 1)
				return false;
			
			return true;
		}

		/**
		 * Gets the object at the next index of the collection.
		 * 
		 * @return object at next index of the collection
		 */
		@Override
		public Object getNext() {
			currElementIndex++;
			return (clone.get(currElementIndex));
		}
	} // end private iterator class
	
} // end GameObjectCollection class


