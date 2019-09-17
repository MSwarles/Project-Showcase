package com.mycompany.a3.interfaces;

import com.codename1.ui.Graphics;
import com.codename1.ui.geom.Point;

/**
 * Interface that provides objects with the ability to draw themselves.
 * @author Matt
 *
 */

public interface IDrawable {
	public void draw(Graphics g, Point pCmpRelPrnt);
}
