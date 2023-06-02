package effect;

import agent.Recipe;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.mockito.Mockito;
import virologist.Inventory;
import virologist.Virologist;

import java.util.ArrayList;
import java.util.Collection;
import java.util.concurrent.atomic.AtomicInteger;

import static org.mockito.Mockito.*;

public class AmnesiaTest {

    Virologist virologist, spy;

    Amnesia amnesia;
    Inventory inventory;
    Collection<Recipe> recipes;

    @Test
    public void applyEffect() {

        amnesia.applyEffect(spy);
        verify(spy, times(3)).removeRecipe(any(Recipe.class));
    }

    @Before
    public void setUp(){

        recipes = new ArrayList<>();
        recipes.add(mock(Recipe.class));
        recipes.add(mock(Recipe.class));
        recipes.add(mock(Recipe.class));
        inventory = mock(Inventory.class);

        amnesia = new Amnesia();

        virologist = mock(Virologist.class);

        spy = spy(virologist);
        when(inventory.getRecipes()).thenReturn(recipes);
        when(spy.getInventory()).thenReturn(inventory);

    }

}