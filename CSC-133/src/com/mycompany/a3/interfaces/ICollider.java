package com.mycompany.a3.interfaces;

import com.mycompany.a3.GameWorld;

/**
 * Interface that allows objects to collide with other objects that also implement it.
 * @author Matt
 *
 */
public interface ICollider {
	public boolean collidesWith(ICollider otherObject);
	public void handleCollision(ICollider otherObject, GameWorld gw);
}
