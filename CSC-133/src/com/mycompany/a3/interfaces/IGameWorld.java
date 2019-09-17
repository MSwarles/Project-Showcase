package com.mycompany.a3.interfaces;

/**
 * Interface that contains the essential functions of a GameWorld.
 * 
 * @author Matt
 *
 */
public interface IGameWorld {
	/**
	 * Gets the amount of lives the player has remaining.
	 * 
	 * @return amount of lives
	 */
	public int getLives();
	
	/**
	 * Gets the amount of missiles the player ship has.
	 * 
	 * @return amount of missiles
	 */
	public int getMissiles();
	
	/**
	 * Gets the amount of game time that has passed.
	 * 
	 * @return elapsed game time
	 */
	public int getGameTime();
	
	/**
	 * Gets the amount of points the player has earned.
	 * 
	 * @return points earned
	 */
	public int getPoints();
	
	/**
	 * Gets the current sound status, <code>true</code> if ON,
	 * <code>false</code> if OFF.
	 * 
	 * @return current sound status
	 */
	public boolean isSoundOn();
	
	/**
	 * Turns sound ON if b is <code>true<code>, turns sound OFF if b is
	 * <code>false<code>.
	 *  
	 * @param on true to turn sound ON, false to turn sound OFF. 
	 */
	public void setSound(boolean on);
	
	/**
	 * If objectList contains a PlayerShip, tells user error: PS already exists.
	 * otherwise creates a PlayerShip object and tells user PS object was created.
	 * <b>Note:</b> only one PlayerShip may exist.
	 */
	public void spawnPs();
	
	/**
	 * Creates a NonPlayerShip object and adds it to the storage list
	 * and tells user NPS object was created. 
	 */
	public void spawnNps();
	
	/**
	 * Creates an Asteroid object and adds it to the storage list
	 * and tells user Asteroid object was created.
	 */
	public void spawnAsteroid();
	
	/**
	 * Creates a Space Station object and adds it to the storage list
	 * and tells user SpaceStation object was created.
	 */
	public void spawnSpaceStation();
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If a PlayerShip exists, then increases PS speed and tells user PS speed 
	 * was increased. Otherwise tells user error: PS doesn't exist.
	 */
	public void increasePsSpeed();
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If a PlayerShip exists, then decreases PS speed and tells user PS speed 
	 * was decreased. Otherwise tells user error: PS doesn't exist.
	 */
	public void decreasePsSpeed();
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PlayerShip exists, then turns PS ship left and tells user PS was 
	 * turned left. Otherwise tells user error: PS doesn't exist.
	 */
	public void turnPsLeft();
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PlayerShip exists, then turns PS ship right and tells user PS was 
	 * turned right. Otherwise tells user error: PS doesn't exist.
	 */
	public void turnPsRight();
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PlayerShip exists, then turns PS MissileLauncher left and tells 
	 * user PS Launcher was turned left. Otherwise tells user error: PS doesn't 
	 * exist.
	 */
	public void turnPsLauncherLeft();
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PlayerShip exists, then turns PS MissileLauncher right and tells 
	 * user PS Launcher was turned right. Otherwise tells user error: PS doesn't 
	 * exist.
	 */
	public void turnPsLauncherRight();
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PlayerShip exists, then checks to see if PS has missiles.
	 * If PS has missiles, 'fires missile' by creating a new missile at
	 * PS location with PS speed and launcher direction, and then tells user 
	 * missile has been fired. 
	 * If PS is out of missiles, tells user PS couldn't fire.
	 * If PS doesn't exist, tells user error: PS doesn't exist.
	 */
	public void firePsMissile();
	
	/**
	 * First verifies that a NonPlayerShip object exists.
	 * If NPS exists, checks to see if NPS has missiles.
	 * If NPS has missiles, creates new missile at NPS location with NPS speed 
	 * and launcher direction, and tells user NPS missile has been fired.  
	 * If NPS is out of missiles, looks to see if another NPS exists
	 * If no NPS exists, tells user error: NPS doesn't exist.
	 */
	public void fireNpsMissile();
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PS exists, sets PS location to center of map and tells user hyperjump 
	 * succeeded. Otherwise tells user error: no PS exists.
	 */
	public void hyperJump();
	
	/**
	 * First verifies that both PlayerShip and SpaceStation object exist.
	 * If both exist, refills PlayerShip missiles and tells user missiles
	 * were refilled.
	 * If either PS or SpaceStation don't exist, prints error message to user
	 * with which one doesn't exist.
	 */
	public void reloadPsMissiles();
	
	/**
	 * Increments elapsedGameTime and tells user clock was ticked. 
	 * Iterates through objectList looking for instances of IMovable.
	 * If instance of IMovable is found, calls their move() method.
	 * <p>
	 * Also checks for instance of Missile. If instance of Missile is found,
	 * checks if missile has fuel. If Missile is out of fuel, removes it 
	 * from objectList and tells user Missile exploded.
	 */
	public void tick(int elapsedMs);
	
	/**
	 * Iterates through objectList and calls each GameObject's toString() 
	 * method, which displays pertinent information about each object.
	 */
	public void printMap();
}
