package virologist;

import base.Item;

import java.util.Collection;

/**
 * Interface for a collection of items
 *
 * @param <T> the type if Item to store
 */
public interface ItemCollection<T extends Item> {
    /**
     * Add item to the collection.
     *
     * @param item the item to add
     */
    void add(T item);

    /**
     * Remove an item from the collection.
     *
     * @param item the item to remove
     * @return the removed item
     */
    T remove(T item);

    /**
     * Provide a way to view the items in the collection.
     * Collections.unmodifiableCollection(collection) is encouraged.
     *
     * @return the collection of items (preferably read-only)
     */
    Collection<T> getView();
}
