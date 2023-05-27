package ingredient;

import base.Id;

/**
 * A class representing an amount of Nucleotide.
 */
public class Nucleotide extends Ingredient {
    /**
     * The unique if of Nucleotide
     */
    private static final Id id = new Id();

    public Nucleotide(int quantity) {
        super("Nucleotide", quantity);
    }

    @Override
    Ingredient remove(int quantity) {
        subtract(quantity);
        return new Nucleotide(quantity);
    }

    @Override
    public Id getId() {
        return id;
    }
}
