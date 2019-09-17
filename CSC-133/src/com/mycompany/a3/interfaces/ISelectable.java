package com.mycompany.a3.interfaces;

import com.codename1.ui.Graphics;
import com.codename1.ui.geom.Point;

/**
 * Interface that allows objects to be selected via mouse click.
 * @author Matt
 *
 */
public interface ISelectable {
	// a way to mark an object as "selected" or not
	public void setSelected(boolean selected);
	
	// a way to test whether an object is selected
	public boolean isSelected();
	
	// a way to determine if a pointer is "in" an object
	public boolean contains(int x, int y);
	
	// a way to "draw" the object that knows about drawing
	// different ways depending on "isSelected"
	public void draw(Graphics g, Point pCmpRelPrnt);
}
