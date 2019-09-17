package com.mycompany.a3.views;

import java.util.Observable;
import java.util.Observer;

import com.codename1.charts.util.ColorUtil;
import com.codename1.ui.Container;
import com.codename1.ui.Graphics;
import com.codename1.ui.geom.Point;
import com.codename1.ui.plaf.Border;
import com.mycompany.a3.GameWorld;
import com.mycompany.a3.gameobjects.GameObject;
import com.mycompany.a3.interfaces.IDrawable;
import com.mycompany.a3.interfaces.IGameWorld;
import com.mycompany.a3.interfaces.IIterator;
import com.mycompany.a3.interfaces.ISelectable;

/**
 * MapView is a view that displays the map, which contains the GameObjects
 * within the GameWorld.
 * 
 * @author Matt
 *
 */
public class MapView extends Container implements Observer {
	private GameWorld gameWorld;
	
	public MapView(GameWorld gameWorld) {
		// Set the style of the Map View
		this.getAllStyles().setBorder(Border.createLineBorder(4, ColorUtil.WHITE));
		this.getAllStyles().setBgTransparency(255);
		this.getAllStyles().setBgColor(ColorUtil.BLACK);
		
		this.gameWorld = gameWorld;
	}
	
	public void update(Observable observable, Object data) {
		// Cast data as IGameWorld
		IGameWorld gw = (IGameWorld)data;
		
		// print the list of game objects currently in the game world
		gw.printMap();
		repaint();		
	}
	
	/**
	 * Draws the game world.
	 */
	public void paint(Graphics g) {
		super.paint(g);
		
		IIterator i = gameWorld.getIterator();
		while (i.hasNext()) {
			GameObject go = (GameObject)i.getNext();
			
			if (go instanceof IDrawable) {
				IDrawable d = (IDrawable)go;
				d.draw(g, new Point(this.getX(), this.getY()));
			}
		}
	}

	// TODO: add desc
	public void pointerPressed(int x, int y) {
		// only able to select objects when game is paused
		if (!gameWorld.isPaused())
			return;
		
		int relX = x - getAbsoluteX();
		int relY = y - getAbsoluteY();
		
		IIterator i = gameWorld.getIterator();
		while (i.hasNext()) {
			Object o = i.getNext();
			if (o instanceof ISelectable) {
				ISelectable s = (ISelectable)o;
				// check if pointer is inside object
				if (s.contains(relX, relY)) {
					s.setSelected(true);
					System.out.println("TRUE");
				} else {
					s.setSelected(false);
					System.out.println("FALSE");
				}
			}
		}
			
		System.out.println("X: " + relX + " Y: " + relY);
		
		
	}
	
}
