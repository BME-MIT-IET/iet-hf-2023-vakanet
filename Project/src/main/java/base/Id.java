package base;

import java.util.Objects;

/**
 * Class to provide unique id-s to objects
 */
public class Id {
    private final long id;

    /**
     * Get a new unique id from the Game singleton
     */
    public Id() {
        this.id = Game.getInstance().getNewUniqueId();
    }

    public Id(long id) {
        this.id = id;
    }

    /**
     * @return an invalid Id (0)
     */
    public static Id getInvalid() {
        return new Id(0);
    }

    /**
     * @return the same id, but negative
     */
    public Id getNegative() {
        return new Id(-id);
    }

    /**
     * @return the id number
     */
    public long getId() {
        return id;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Id id1 = (Id) o;
        return id == id1.id;
    }

    public boolean equalsNegative(Id i) {
        return id == -i.id;
    }

    @Override
    public int hashCode() {
        return Objects.hash(id);
    }
}
