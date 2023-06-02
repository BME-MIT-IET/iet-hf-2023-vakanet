package Projlab;

import Projlab.util.ObjectMap;
import agent.Recipe;
import city.Field;
import city.Laboratory;
import city.Shelter;
import city.Warehouse;
import effect.Effect;
import equipment.Equipment;
import ingredient.Ingredient;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import virologist.Virologist;

import java.lang.reflect.InvocationTargetException;

import static org.junit.Assert.assertTrue;

public class FieldStepDefinitions {

    ObjectMap objectMap = ObjectMap.getInstance();
    @Given("create laboratory {string} with {string}")
    public void create_laboratory(String laboratory, String effect) throws ClassNotFoundException, NoSuchMethodException, InvocationTargetException, InstantiationException, IllegalAccessException {

        Class<?> clazz = Class.forName("effect."+effect);

        Recipe recipe = new Recipe((Class<? extends Effect>) clazz,null);

        objectMap.addObject(effect,recipe);
        objectMap.addObject(laboratory,new Laboratory(recipe));
    }
    @Given ("create field {string}")
    public void create_field(String field){
        objectMap.addObject(field,new Field());
    }

    @Given ("create virologist {string} on field {string}")
    public void create_virologist(String s1, String s2){
        objectMap.createVirologist(s1,s2);
    }


    @When("collect {string}")
    public void collect(String s1){
        objectMap.collect(s1);
    }

    @Then("{string} has recipe {string}")
    public void has_recipe(String string1, String string2) {
        var recipes = objectMap.<Virologist>getObject(string1).getRecipes();
        assertTrue(recipes.contains(objectMap.<Recipe>getObject(string2)));

    }


}
