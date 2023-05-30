package Projlab;

import agent.Recipe;
import city.Field;
import city.Laboratory;
import effect.Effect;
import ingredient.Ingredient;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import virologist.Virologist;

import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;

public class FieldStepDefinitions {
    String answer = "";
    Object object = null;

    Class<?> clazz = null;
    static HashMap<String , Object> hashmap = new HashMap<>();

     <T> T getObject(String s) {
        return (T) hashmap.get(s);
    }

    @Given("create laboratory {string} with {string}")
    public void create_laboratory(String laboratory, String effect) throws ClassNotFoundException, NoSuchMethodException, InvocationTargetException, InstantiationException, IllegalAccessException {

        clazz = Class.forName("effect."+effect);
        object = clazz.getDeclaredConstructor().newInstance();

        Recipe recipe = new Recipe((Class<? extends Effect>) clazz,null);

        hashmap.put(effect,recipe);
        hashmap.put(laboratory,new Laboratory(recipe));

    }

    @Given ("create object {string} on field {string}")
        public void create_virologist(String s1, String s2){
        hashmap.put(s1, new Virologist(this.getObject(s2),s1) );

    }

    @When("collect {string}")
    public void collect(String s1){
         this.<Virologist>getObject(s1).collect();
    }

    @Then("{string} has {string}")
    public void has_effect(String string1, String string2) {

        assertTrue(this.<Virologist>getObject(string1).getRecipes().contains(this.<Recipe>getObject(string2)));

    }
}
