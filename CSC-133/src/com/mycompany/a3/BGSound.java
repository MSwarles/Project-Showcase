package com.mycompany.a3;

import java.io.InputStream;

import com.codename1.media.Media;
import com.codename1.media.MediaManager;
import com.codename1.ui.Display;

/**
 * This class creates a Media object which loops while playing the sound
 * 
 * @author Matt
 */
public class BGSound implements Runnable {
	/**
	 * The sound
	 */
	private Media m;
	
	/**
	 * Class constructor.
	 * @param fileName name of the sound
	 */
	public BGSound(String fileName) {
		try {
			InputStream is = Display.getInstance().getResourceAsStream(getClass(), "/" + fileName);
			
			// attach a runnable to run when media has finished playing
			m = MediaManager.createMedia(is,  "audio/wav", this);
			} catch(Exception e) {
			e.printStackTrace();
		}
	}

	// entered when media has finished playing
	@Override
	/**
	 * Plays sound again after it has finished playing
	 */
	public void run() {
		// start playing from time zero (beginning of the sound file)
		m.setTime(0);
		m.play();
	}
}
