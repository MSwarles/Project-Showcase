package com.mycompany.a3;

import java.util.ArrayList;
import java.util.Observable;

import com.codename1.ui.Dialog;
import com.mycompany.a3.gameobjects.Asteroid;
import com.mycompany.a3.gameobjects.GameObject;
import com.mycompany.a3.gameobjects.Missile;
import com.mycompany.a3.gameobjects.NonPlayerShip;
import com.mycompany.a3.gameobjects.PlayerShip;
import com.mycompany.a3.gameobjects.SpaceStation;
import com.mycompany.a3.interfaces.ICollider;
import com.mycompany.a3.interfaces.IGameWorld;
import com.mycompany.a3.interfaces.IIterator;
import com.mycompany.a3.interfaces.IMovable;
import com.mycompany.a3.interfaces.ISelectable;

/**
 * Holds a collection of @GameObjects and other state variables.
 * 
 * @author Matt
 *
 */
public class GameWorld extends Observable implements IGameWorld {
	/**
	 * Amount of lives player has remaining.
	 */
	private int playerLives = 3; 
	/**
	 * Current score of the player.
	 */
	private int playerScore = 0;
	/**
	 * Current round
	 */
	private int round = 0;
	/**
	 * New round timer
	 */
	private int newRoundTimer = 0;
	/**
	 * Delay for new round (in ms)
	 */
	private int newRoundDelay = 4000;
	/**
	 * Number of remaining asteroids
	 */
	private int asteroidCount = 0;
	/**
	 * Number of remaining nps
	 */
	private int npsCount = 0;
	/**
	 * Nps spawn timer
	 */
	private int npsSpawnTimer = 0;
	/**
	 * Time between Nps spawns (in ms)
	 */
	private int npsSpawnDelay = 15000;
	/**
	 * Player respawn timer
	 */
	private int psRespawnTimer = 0;
	/**
	 * Player respawn delay
	 */
	private int psRespawnDelay = 2000;
	/**
	 * Is player alive
	 */
	private boolean playerAlive = false;
	/**
	 * Total amount of time that's elapsed since game start.
	 */
	private int elapsedGameTime = 0;
	/**
	 * Amount of time since last 'tick'
	 */
	private int elapsedMs = 0;
	/**
	 * Current status of sound. True means on.
	 */
	private boolean soundOn = true;
	/**
	 * Whether the game is paused or not
	 */
	private boolean paused = false;
	/**
	 * Collection of GameObjects currently spawned.
	 */
	private GameObjectCollection goc;
	/**
	 * List of objects queued for removal from game object collection
	 */
	private ArrayList<Object> removalList;
	
	/**
	 * Sounds
	 */
	private Sound playerFire;
	private Sound npsFire;
	private Sound asteroidDeath;
	private Sound playerDeath;
	private Sound npsDeath;
	private Sound hyperJump;
	private BGSound music;
	
	/**
	 * Class constructor.
	 */
	public GameWorld() { 
		goc = new GameObjectCollection();
		playerFire = new Sound("playerFire.wav");
		npsFire = new Sound("npsFire.wav");
		asteroidDeath = new Sound("asteroidDeath.wav");
		playerDeath = new Sound("playerDeath.wav");
		npsDeath = new Sound("npsDeath.wav");
		hyperJump = new Sound("hyperJump.wav");
		//music = new BGSound ("music");
	}
	
	/**
	 * Sets the initial state of the game.
	 */
	public void init() { 
		goc = new GameObjectCollection();
		removalList = new ArrayList<Object>();
		
		PlayerShip.getShip().init();
		spawnPs();
		spawnSpaceStation();
		playerLives = 3;
		playerScore = 0;
		round = 0;
		newRoundTimer = 0;
		asteroidCount = 0;
		npsCount = 0;
		npsSpawnTimer = 0;
		elapsedGameTime = 0;
		elapsedMs = 0;
	}
	
	/**
	 * Gets the amount of lives the player has remaining.
	 * 
	 * @return amount of lives
	 */
	public int getLives() {	return playerLives;	}
	
	/**
	 * Gets the amount of missiles the player ship has.
	 * 
	 * @return amount of missiles
	 */
	public int getMissiles() {
		int missileCount = 0;
		IIterator i = goc.getIterator();
		
		// search collection for PS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof PlayerShip) {
				missileCount = ((PlayerShip) g).getMissileCount();
				break;
			}
		}
			
		return missileCount;
	}
	
	/**
	 * Gets the amount of game time that has passed.
	 * 
	 * @return elapsed game time
	 */
	public int getGameTime() { return elapsedGameTime; }
	
	/**
	 * Gets the amount of points the player has earned.
	 * 
	 * @return points earned
	 */
	public int getPoints() { return playerScore; }
	
	/**
	 * Gets the iterator from game object collection
	 * @return the iterator
	 */
	public IIterator getIterator() { return goc.getIterator(); }
	
	/**
	 * Gets the current sound status, <code>true</code> if ON,
	 * <code>false</code> if OFF.
	 * 
	 * @return current sound status
	 */
	public boolean isSoundOn() { return soundOn; }
	
	/**
	 * Adds points to the player's score
	 */
	public void addPoints(int points) { playerScore += points; }
	
	/**
	 * Decrements player lives.
	 */
	public void removePlayerLife() { playerLives --; }
	
	/**
	 * Whether the game is paused or not
	 */
	public boolean isPaused() { return paused; }
	/**
	 * Turns sound ON if b is <code>true<code>, turns sound OFF if b is
	 * <code>false<code>.
	 *  
	 * @param on <code>true</code> to turn sound ON, <code>false</code> to turn sound OFF. 
	 */
	public void setSound(boolean on) {
		soundOn = on;
		
		System.out.println("Sound turned " + (soundOn ? "ON" : "OFF") + ".");
		
		// update the views
		this.setChanged();
		this.notifyObservers(new GameWorldProxy(this));
	}
	public void setPaused(boolean paused) { 
		this.paused = paused;
		
		IIterator i = goc.getIterator();
		while (i.hasNext()) {
			Object o = i.getNext();
			if (o instanceof ISelectable) {
				ISelectable s = (ISelectable)o;
				s.setSelected(false);
			}
		}
	}
	
	/**
	 * Removes a game object from the collection
	 */
	public void removeObject(Object o) {
		removalList.add(o);
	}
	
	/**
	 * If objectList contains a PlayerShip, tells user error: PS already exists.
	 * otherwise creates a PlayerShip object and tells user PS object was created.
	 * <b>Note:</b> only one PlayerShip may exist.
	 */
	public void spawnPs() {
		IIterator i = goc.getIterator();		
		
		// search collection for PS before spawning, to be sure one doesn't exist
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof PlayerShip) {
				// PS ship already exists, so print error and return.
				System.out.println("ERROR - PS already exists!");
				return;
			}
		}
		
		// PS ship doesn't exist, so it's OK to spawn one
		goc.add(PlayerShip.getShip());
		playerAlive = true;
		System.out.println("New PS created.");
		
		// update the views
		this.setChanged();
		this.notifyObservers(new GameWorldProxy(this));
	}
	
	/**
	 * Creates a NonPlayerShip object and adds it to the storage list
	 * and tells user NPS object was created. 
	 */
	public void spawnNps() {
		goc.add(new NonPlayerShip());
		npsCount++;
		System.out.println("New NPS created.");
		
		// update the views
		this.setChanged();
		this.notifyObservers(new GameWorldProxy(this));
	}
	
	/**
	 * Creates an Asteroid object and adds it to the storage list
	 * and tells user Asteroid object was created.
	 */
	public void spawnAsteroid() {
		goc.add(new Asteroid());
		asteroidCount++;
		System.out.println("New Asteroid created.");
		
		// update the views
		this.setChanged();
		this.notifyObservers(new GameWorldProxy(this));
	}
	
	/**
	 * Creates a Space Station object and adds it to the storage list
	 * and tells user SpaceStation object was created.
	 */
	public void spawnSpaceStation() {
		goc.add(new SpaceStation());
		System.out.println("New Space Station created.");
		
		// update the views
		this.setChanged();
		this.notifyObservers(new GameWorldProxy(this));
	}
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If a PlayerShip exists, then increases PS speed and tells user PS speed 
	 * was increased. Otherwise tells user error: PS doesn't exist.
	 */
	public void increasePsSpeed() {
		IIterator i = goc.getIterator();		
		
		// search collection for PS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			
			if (g instanceof PlayerShip) {
				PlayerShip ps = (PlayerShip)g;
				ps.increaseSpeed();
				System.out.println("PS speed increased.");
				
				// update the views
				this.setChanged();
				this.notifyObservers(new GameWorldProxy(this));
				return;
			}
		}
		
		System.out.println("ERROR - Unable to [increase PS speed]: PS doesn't exist.");
	}
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If a PlayerShip exists, then decreases PS speed and tells user PS speed 
	 * was decreased. Otherwise tells user error: PS doesn't exist.
	 */
	public void decreasePsSpeed() {
		IIterator i = goc.getIterator();		
		
		// search collection for PS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof PlayerShip) {
				PlayerShip ps = (PlayerShip)g;
				ps.decreaseSpeed();
				System.out.println("PS speed decreased.");
				
				// update the views
				this.setChanged();
				this.notifyObservers(new GameWorldProxy(this));
				return;
			}
		}
		
		System.out.println("ERROR - Unable to [decrease PS speed]: PS doesn't exist.");
	}
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PlayerShip exists, then turns PS ship left and tells user PS was 
	 * turned left. Otherwise tells user error: PS doesn't exist.
	 */
	public void turnPsLeft() {
		IIterator i = goc.getIterator();		
		
		// search collection for PS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof PlayerShip) {
				PlayerShip ps = (PlayerShip)g;
				ps.turnLeft();
				System.out.println("PS turned left.");
				
				// update the views
				this.setChanged();
				this.notifyObservers(new GameWorldProxy(this));			
				return;
			}
		}
		
		System.out.println("ERROR - Unable to [turn left]: PS doesn't exist.");
	}
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PlayerShip exists, then turns PS ship right and tells user PS was 
	 * turned right. Otherwise tells user error: PS doesn't exist.
	 */
	public void turnPsRight() {
		IIterator i = goc.getIterator();		
		
		// search collection for PS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof PlayerShip) {
				PlayerShip ps = (PlayerShip)g;
				ps.turnRight();
				System.out.println("PS turned right.");
				
				// update the views
				this.setChanged();
				this.notifyObservers(new GameWorldProxy(this));				
				return;
			}
		}
		
		System.out.println("ERROR - Unable to [turn right]: PS doesn't exist.");
	}
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PlayerShip exists, then turns PS MissileLauncher left and tells 
	 * user PS Launcher was turned left. Otherwise tells user error: PS doesn't 
	 * exist.
	 */
	public void turnPsLauncherLeft() {
		IIterator i = goc.getIterator();		
		
		// search collection for PS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof PlayerShip) {
				PlayerShip ps = (PlayerShip)g;
				ps.turnLauncherLeft();
				System.out.println("PS Launcher turned left.");
				
				// update the views
				this.setChanged();
				this.notifyObservers(new GameWorldProxy(this));				
				return;
			}
		}
		
		System.out.println("ERROR - Unable to [turn Launcher left]: PS doesn't exist.");
	}
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PlayerShip exists, then turns PS MissileLauncher right and tells 
	 * user PS Launcher was turned right. Otherwise tells user error: PS doesn't 
	 * exist.
	 */
	public void turnPsLauncherRight() {
		IIterator i = goc.getIterator();		
		
		// search collection for PS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof PlayerShip) {
				PlayerShip ps = (PlayerShip)g;
				ps.turnLauncherRight();
				System.out.println("PS Launcher turned right.");
				
				// update the views
				this.setChanged();
				this.notifyObservers(new GameWorldProxy(this));				
				return;
			}
		}
		
		System.out.println("ERROR - Unable to [turn Launcher right]: PS doesn't exist.");
	}
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PlayerShip exists, then checks to see if PS has missiles.
	 * If PS has missiles, 'fires missile' by creating a new missile at
	 * PS location with PS speed and launcher direction, and then tells user 
	 * missile has been fired. 
	 * If PS is out of missiles, tells user PS couldn't fire.
	 * If PS doesn't exist, tells user error: PS doesn't exist.
	 */
	public void firePsMissile() {
		IIterator i = goc.getIterator();		
		
		// search collection for PS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof PlayerShip) {
				PlayerShip ps = (PlayerShip)g;
				// if PS has missiles
				if (ps.fireMissile()) {
					// create a new missile at PS location
					Missile m = new Missile(
							ps.getLocation().getX(), 
							ps.getLocation().getY(), 
							ps.getSpeed(), 
							ps.getLauncherDirection(), 
							true);
					// add missile to collection
					goc.add(m);
					playerFire.play(this);
					System.out.println("PS fired missile.");
					
					// update the views
					this.setChanged();
					this.notifyObservers(new GameWorldProxy(this));
				} else {
					System.out.println("Unable to [fire] missile: PS is out of missiles!");
				}
				return;
			}
		}		
		System.out.println("ERROR - Unable to [fire] missile: PS doesn't exist.");
	}
	
	/**
	 * First verifies that a NonPlayerShip object exists.
	 * If NPS exists, checks to see if NPS has missiles.
	 * If NPS has missiles, creates new missile at NPS location with NPS speed 
	 * and launcher direction, and tells user NPS missile has been fired.  
	 * If NPS is out of missiles, looks to see if another NPS exists
	 * If no NPS exists, tells user error: NPS doesn't exist.
	 */
	public void fireNpsMissile() {
		NonPlayerShip nps = null;
		IIterator i = goc.getIterator();		
		
		// search collection for NPS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			
			if (g instanceof NonPlayerShip) {
				nps = (NonPlayerShip)g;
				
				// if the NPS has missiles left
				if (nps.fireMissile()) {
					// create a new missile at NPS location
					Missile m = new Missile(nps.getLocation().getX(), nps.getLocation().getY(), 
							nps.getSpeed(), nps.getLauncherDirection(),	false);
					// add the missile to collection
					goc.add(m);
					npsFire.play(this);
					System.out.println("NPS fired missile.");
					
					// update the views
					this.setChanged();
					this.notifyObservers(new GameWorldProxy(this));
					return;
				} 
			}
		}
		
		if (nps != null)
			System.out.println("Unable to [Launch] missile: Current NPS's are out of missiles!");
		else
			System.out.println("ERROR - Unable to [Launch] missile: NPS doesn't exist!");
	}
	
	/**
	 * First verifies that a PlayerShip object exists.
	 * If PS exists, sets PS location to center of map and tells user hyperjump 
	 * succeeded. Otherwise tells user error: no PS exists.
	 */
	public void hyperJump() {
		IIterator i = goc.getIterator();
		
		// search collection for PS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof PlayerShip) {
				PlayerShip ps = (PlayerShip)g;
				
				// reset PS location to the center of the map
				ps.setLocation(Game.getMapWidth() / 2, Game.getMapHeight() / 2);
				hyperJump.play(this);
				System.out.println("Hyperjump successful.");
				
				// update the views
				this.setChanged();
				this.notifyObservers(new GameWorldProxy(this));				
				return;
			}
		}		
		System.out.println("ERROR - Unable to [hyperjump]: PS doesn't exist!");
	}
	
	/**
	 * First verifies that both PlayerShip and SpaceStation object exist.
	 * If both exist, refills PlayerShip missiles and tells user missiles
	 * were refilled.
	 * If either PS or SpaceStation don't exist, prints error message to user
	 * with which one doesn't exist.
	 */
	public void reloadPsMissiles() {
		PlayerShip ps = null;
		SpaceStation station = null;	
		IIterator i = goc.getIterator();		
		
		// search collection for PS
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof PlayerShip) {
				ps = (PlayerShip)g;
			} else if (g instanceof SpaceStation) {
				station = (SpaceStation)g;
			}
		}
		
		if (ps == null || station == null) {
			System.out.println("ERROR - Unable to [reload PS missiles]: "
					+ (ps == null ? "PS " : "Space Station ") + "doesn't exist!");
			return;
		}
		
		// reload the PS's missiles
		ps.reloadMissiles();
		System.out.println("PS missiles reloaded.");
		
		// update the views
		this.setChanged();
		this.notifyObservers(new GameWorldProxy(this));
	}
	
	/**
	 * Refuels currently selected missile.
	 */
	public void refuelMissile() {
		
		IIterator i = goc.getIterator();
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			if (g instanceof Missile) {
				Missile m = (Missile)g;
				if (m.isSelected())	{
					m.refuel();
					break;
				}
			}
		}
	}
	
	/**
	 * Handles nps death by removing it from goc and playing death sound.
	 * @param nps nps that was killed
	 */
	public void npsDeath(NonPlayerShip nps) {
		removeObject(nps);
		npsCount--;
		npsDeath.play(this);
	}
	
	/**
	 * Handles asteroid death by removing it from goc and playing death sound.
	 * @param a asteroid that was killed
	 */
	public void asteroidDeath(Asteroid a) {
		removeObject(a);
		asteroidCount--;
		asteroidDeath.play(this);
	}
	
	/**
	 * Handles player ship death by removing it from goc and playing death sound.
	 * Also decrements player lives.
	 * @param ps player ship that was killed
	 */
	public void playerDeath(PlayerShip ps) {
		removeObject(ps);
		playerLives--;
		playerAlive = false;
		playerDeath.play(this);
	}
	
	
	/**
	 * Checks that player has lives left. If not, displays 'Game over' dialog and ends game.
	 * <p> 
	 * If player is out of lives, displays a 'Game Over' dialog.
	 */
	private void checkGameOver() {
		if (playerLives == 0) {
			// player is out of lives, so display Game Over dialog.
			System.out.println("Player is out of lives! Game Over!");
			boolean quit = Dialog.show("Game Over", "Final Score: " + playerScore, "Quit", "Play Again");
			
			if (quit)
				System.exit(0);
			else
				init();
		}
	}
	
	/**
	 * Increments elapsedGameTime and tells user clock was ticked. 
	 * Iterates through objectList looking for instances of IMovable.
	 * If instance of IMovable is found, calls their move() method.
	 * <p>
	 * Also checks for instances of Missile. If Missile is found,
	 * checks if missile has fuel. If Missile is out of fuel, removes it 
	 * from objectList and tells user Missile exploded.
	 * @param elapsedTime 
	 */
	public void tick(int elapsedMillisecs) {
		if (!isPaused()) {
			
			elapsedMs += elapsedMillisecs;
			
			if (elapsedMs >= 1000) {
				elapsedMs -= 1000;
				// increment the game clock
				elapsedGameTime++;
			}
			
			if (!playerAlive) {
				psRespawnTimer += elapsedMillisecs;
				
				if (psRespawnTimer >= psRespawnDelay) {
					psRespawnTimer = 0;
					spawnPs();
				}
				
			}
			
			// if there are no asteroids or nps left, count down to start new round
			if (asteroidCount == 0 && npsCount == 0) {
				// increment new round timer
				newRoundTimer += elapsedMillisecs;
				
				// if new round delay has been met, start new round
				if (newRoundTimer >= newRoundDelay) {
					newRoundTimer = 0;
					startNewRound();
				}
			} else if (asteroidCount > 0){	// asteroids are alive, so increment nps spawn timer
				// increment nps spawn timer
				npsSpawnTimer += elapsedMillisecs;
				// if nps delay has been met, spawn new nps
				if (npsSpawnTimer >= npsSpawnDelay) {
					npsSpawnTimer -= npsSpawnDelay;
					spawnNps();
				}				
			}
				
			//-----------------------------------------------------------------
			//---------------------- Move Game Objects ------------------------
			//-----------------------------------------------------------------
				
			IIterator i = goc.getIterator();		
			
			// iterate through object list
			while(i.hasNext()) {
				GameObject g = (GameObject)i.getNext();
				// if current object is movable
				if (g instanceof IMovable) {
					IMovable mObj = (IMovable)g;
					// move current object
					mObj.move(elapsedMillisecs);
				}
				
				// if current object is a missile
				if (g instanceof Missile) {
					Missile m = (Missile)g;
					// check to see if missile has fuel left
					if (m.getFuel() <= 0)
					{
						// the missile ran out of fuel, so remove it from collection and print message that it exploded.
						goc.remove(g);
						System.out.println("A Missile ran out of fuel and exploded.");
					}				
				}
				
				// if current object is a space station
				if (g instanceof SpaceStation) {
					SpaceStation s = (SpaceStation)g;
					// toggle its light on and off
					s.toggleLight(elapsedGameTime);
				}
					
			}
			
			//-----------------------------------------------------------------
			//-------------------- Check for Collisions -----------------------
			//-----------------------------------------------------------------
			
			i = goc.getIterator();
			
			// iterate through object list
			while(i.hasNext()) {
				GameObject g = (GameObject)i.getNext();
				if (g instanceof ICollider) {
					ICollider cObj = (ICollider)g;	// get a collidable object
					
					IIterator i2 = goc.getIterator();
					while (i2.hasNext()) {
						GameObject g2 = (GameObject)i2.getNext();
						if (g2 instanceof ICollider) {
							ICollider cObj2 = (ICollider)g2;	// get another collidable object
							// check for collision
							if (cObj != cObj2) {	// make sure they're not the same object
								if (cObj.collidesWith(cObj2)) {
									cObj.handleCollision(cObj2, this);
								}
							}
						}
					}
				}
			}
			
			// remove objects involved in collisions
			for(Object o : removalList)
				goc.remove(o);
			
			// clear the removal list
			removalList.clear();
		}
		
		//-----------------------------------------------------------------
		//----------------------- Repaint Objects -------------------------
		//-----------------------------------------------------------------
		// update the views
		this.setChanged();
		this.notifyObservers(new GameWorldProxy(this));
		
		// check if player is out of lives
		checkGameOver();
	}
	
	/**
	 * Begins a new round by spawning asteroids (amount based on current round number)
	 */
	private void startNewRound() {
		// increment the round
		round++;
		
		// reset nps spawn timer
		npsSpawnTimer = 0;
		
		// spawn asteroids (more for each round)
		for (int i = 0; i < round + 5; i++)
			spawnAsteroid();
	}
	
	/**
	 * Iterates through objectList and calls each GameObject's toString() 
	 * method, which displays pertinent information about each object.
	 */
	public void printMap() {
		System.out.println("--------------------------------- GAME MAP -------------------------------------");
		
		IIterator i = goc.getIterator();				
		while(i.hasNext()) {
			GameObject g = (GameObject)i.getNext();
			System.out.println(g.toString());
		}
		
		System.out.println("--------------------------------------------------------------------------------");
	}
}
