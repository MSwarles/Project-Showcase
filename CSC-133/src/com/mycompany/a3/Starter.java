package com.mycompany.a3;

import static com.codename1.ui.CN.*;
import com.codename1.ui.Display;
import com.codename1.ui.Form;
import com.codename1.ui.Dialog;
import com.codename1.ui.Label;
import com.codename1.ui.plaf.UIManager;
import com.codename1.ui.util.Resources;
import com.codename1.io.Log;
import com.codename1.ui.Toolbar;
import java.io.IOException;
import com.codename1.ui.layouts.BoxLayout;
import com.codename1.io.NetworkEvent;

/**
 * <h1>Asteroids</h1>
 * This project is a recreation of the game Asteroids. 
 * <p>
 * Its goal is to teach good programming etiquette and design 
 * patterns through game design. 
 * 
 * @author Matt Swart
 * @version 2.0.0
 * @since 09/09/2018
 * 
 */
@SuppressWarnings("unused")
public class Starter {
	/**
	 * The form that is currently displayed.
	 */
    private Form current;
    /**
     * The theme of the current form.
     */
	private Resources theme;

	// invoked when app is first launched from a Not Running state
    public void init(Object context) {
        // use two network threads instead of one
        updateNetworkThreadCount(2);

        theme = UIManager.initFirstTheme("/theme");

        // Enable Toolbar on all Forms by default
        Toolbar.setGlobalToolbar(true);

        // Pro only feature, uncomment if you have a pro subscription
        Log.bindCrashProtection(true);

        addNetworkErrorListener(err -> {
            // prevent the event from propagating
            err.consume();
            if(err.getError() != null) {
                Log.e(err.getError());
            }
            Log.sendLogAsync();
            Dialog.show("Connection Error", "There was a networking error in the connection to " + err.getConnectionRequest().getUrl(), "OK", null);
        });        
    }
    
    // invoked after a 'cold start', or after app is restored from a Suspended state.
    public void start() {
        if(current != null){
            current.show();
            return;
        }
        new Game();
    }

    // invoked when app is minimized 
    public void stop() {
        current = getCurrentForm();
        if(current instanceof Dialog) {
            ((Dialog)current).dispose();
            current = getCurrentForm();
        }
    }
    
    // invoked when app is destroyed, e.g. killed by user in task manager
    public void destroy() { }

}