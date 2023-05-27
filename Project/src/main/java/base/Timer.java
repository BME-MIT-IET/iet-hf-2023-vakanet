package base;

import java.util.*;

/**
 * Singleton class, responsible for keeping a list of updatable
 * objects, and updating them periodically. Thread safe.
 */
public class Timer {
    /**
     * Singleton instance.
     */
    private static Timer instance;

    /**
     * The list of objects to keep updated.
     */
    final Set<Updatable> updatables = Collections.synchronizedSet(new HashSet<>());

    /**
     * A timer that executes the task of updating periodically.
     */
    final java.util.Timer timer = new java.util.Timer();

    private Timer() {
        // start updating the objects periodically
        timer.scheduleAtFixedRate(new TimerTask() {
            @Override
            public void run() {
                update();
            }
        }, 1000, 1000);
    }

    /**
     * Get the singleton instance.
     *
     * @return the singleton instance
     */
    public static Timer getInstance() {
        if (instance == null) instance = new Timer();
        return instance;
    }

    /**
     * Update the objects which need to be updated.
     */
    private synchronized void update() {
        (new ArrayList<>(updatables)).forEach(Updatable::update);
    }

    /**
     * Add an object to be updated periodically.
     *
     * @param u the object to be updated
     */
    public synchronized void add(Updatable u) {
        updatables.add(u);
    }

    /**
     * Remove an object from the updated objects.
     *
     * @param u the object to be removed
     */
    public synchronized void remove(Updatable u) {
        updatables.remove(u);
    }
}
