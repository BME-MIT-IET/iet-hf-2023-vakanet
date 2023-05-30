package effect;

import agent.Recipe;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.mockito.Mockito;
import virologist.Virologist;

import java.lang.reflect.MalformedParameterizedTypeException;
import java.util.ArrayList;
import java.util.Collection;

public class AmnesiaTest {

    Virologist virologist;

    Amnesia amnesia;

    Collection<Recipe> recipes;

    @Test
    public void applyEffect() {

        amnesia.applyEffect(virologist);
        Assert.assertTrue(recipes.isEmpty());

    }


    @Before
    public void setUp(){
        virologist = Mockito.mock(Virologist.class);


        recipes = new ArrayList<>();

        recipes.add(Mockito.mock(Recipe.class));
        recipes.add(Mockito.mock(Recipe.class));
        recipes.add(Mockito.mock(Recipe.class));

        amnesia = new Amnesia();


        Mockito.when(virologist.getRecipes()).thenReturn(recipes);

        Mockito.doAnswer(invocation -> {
             recipes.remove(invocation.getArgument(0));
             return 0;
        }).when(virologist).removeRecipe(Mockito.eq(((ArrayList<Recipe>)recipes).get(0)));


    }


}