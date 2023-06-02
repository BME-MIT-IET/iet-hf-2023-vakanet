package Projlab;

import Projlab.util.ObjectMap;
import ingredient.Ingredient;
import io.cucumber.java.bs.A;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.junit.Assert;
import virologist.Virologist;

import java.lang.reflect.InvocationTargetException;

import static org.junit.Assert.*;

public class StealStepDefinitions {
    ObjectMap objectMap = ObjectMap.getInstance();
    @Given("{string} has {string} {int}")
    public void has(String virologistName, String ingredientName, Integer count) throws ClassNotFoundException, NoSuchMethodException, InvocationTargetException, InstantiationException, IllegalAccessException {
        Class<?> clazz = Class.forName("ingredient."+ingredientName);
        Ingredient ingredient = (Ingredient)clazz.getDeclaredConstructor().newInstance();
        ingredient.add(count);
        var virologist = objectMap.<Virologist>getObject(virologistName);
        virologist.getInventory().addIngredient(ingredient);
        objectMap.addObject(ingredientName,virologist.getInventory().getIngredients().getView().stream().findAny().orElseThrow());
    }

    @When("{string} steals {string} from {string}")
    public void steals(String virologistName1, String ingredientName, String virologistName2) {
        var virologist1 = objectMap.<Virologist>getObject(virologistName1);
        var virologist2 = objectMap.<Virologist>getObject(virologistName2);
        var ingredient = objectMap.<Ingredient>getObject(ingredientName);
        virologist1.steal(ingredient, virologist2);
        objectMap.addObject(ingredientName,virologist1.getInventory().getIngredients().getView().stream().findAny().orElseThrow());

    }

    @Then("{string} must have {string} {int}")
    public void mustHave(String virologistName, String ingredientName, Integer count) throws ClassNotFoundException {
        Class<?> clazz = Class.forName("ingredient."+ingredientName);
        var virologist = objectMap.<Virologist>getObject(virologistName);

        var ingredients = virologist.getInventory().getIngredients().getView();
        var anyMatch = ingredients.stream().anyMatch(i -> clazz.isInstance(i) && i.getQuantity() == count);
        if(count == 0)
            assertTrue(ingredients.size() == 0 || anyMatch);
        else
            Assert.assertTrue(anyMatch);
    }
}
