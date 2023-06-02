package base;

/**
 * Exception class for game related exceptions, messages.
 */
public class GameException extends RuntimeException {
    public GameException(String msg) {
        super(msg);
    }
}
