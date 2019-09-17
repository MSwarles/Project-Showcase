package com.mycompany.a3;

import java.io.InputStream;

import com.codename1.media.Media;
import com.codename1.media.MediaManager;
import com.codename1.ui.Display;

/**
 * This class encapsulates a sound file as Media inside of a Sound object,
 * and provides a method for playing the sound.
 * 
 * @author Matt
 */
public class Sound {
	/**
	 * The sound file
	 */
	private Media m;
	
	/**
	 * Class constructor.
	 * @param fileName name of the sound file
	 */
	public Sound(String fileName) {
		try {
			InputStream is = Display.getInstance().getResourceAsStream(getClass(), "/" + fileName);
			
			m = MediaManager.createMedia(is, "audio/wav");
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * Plays the sound
	 */
	public void play(GameWorld gw) {
		// if sound is on, play the sound
		if (gw.isSoundOn()) {
			// start playing sound from time zero (beginning of the sound file)
			m.setTime(0);
			m.play();
		}
	}
}
