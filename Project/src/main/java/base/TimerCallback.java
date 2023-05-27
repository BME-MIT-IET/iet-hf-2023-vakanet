package base;

/**
 * An updatable class to keep track of the remaining time, and
 * run a function when the time is expired.
 */
public class TimerCallback implements Updatable {
    /**
     * The function to call when expired.
     */
    final Runnable callback;
    /**
     * Remaining time in milliseconds.
     */
    long remaining;
    /**
     * The time when update was last called.
     */
    long lastUpdate;

    /**
     * The object becomes inactive when the time expires.
     */
    boolean active = true;

    public TimerCallback(long millis, Runnable f) {
        // register for updating
        Timer.getInstance().add(this);

        remaining = millis;
        callback = f;
        lastUpdate = System.currentTimeMillis();
    }

    @Override
    public void update() {
        if (active) {
            // decrement remaining time, update lastUpdate
            var current = System.currentTimeMillis();
            remaining -= current - lastUpdate;
            lastUpdate = current;

            if (remaining <= 0) {
                active = false;

                // run callback
                callback.run();

                // unregister, no more updates
                Timer.getInstance().remove(this);
            }
        }
    }

    /**
     * Get the remaining time.
     *
     * @return the remaining time
     */
    public long getRemaining() {
        return remaining;
    }
}
