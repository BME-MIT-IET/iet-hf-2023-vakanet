package ingredient;

import base.Id;

/**
 * A class representing an amount of amino-acid.
 */
public class AminoAcid extends Ingredient {
    /**
     * The unique id of AminoAcid
     */
    private static final Id id = new Id();

    public AminoAcid() {
        this(0);

    }
    public AminoAcid(int quantity) {
        super("AminoAcid", quantity);
    }

    @Override
    Ingredient remove(int quantity) {
        subtract(quantity);
        return new AminoAcid(quantity);
    }

    @Override
    public Id getId() {
        return id;
    }
}
