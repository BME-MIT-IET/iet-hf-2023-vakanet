package Projlab;

import Projlab.util.ObjectMap;
import agent.Recipe;
import city.Field;
import city.Laboratory;
import effect.Effect;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import virologist.Virologist;

import java.lang.reflect.InvocationTargetException;

import static org.junit.Assert.assertSame;
import static org.junit.Assert.assertTrue;

public class MoveStepDefinitions {

    ObjectMap objectMap = ObjectMap.getInstance();

    @Given("{string} is neighbour of {string}")
    public void add_neighbour(String f1Name, String f2Name){
        var field1 = objectMap.<Field>getObject(f1Name);
        var field2 = objectMap.<Field>getObject(f2Name);
        field1.addNeighbour(field2);
        field2.addNeighbour(field1);
    }

    @When("move {string} to {string}")
    public void move(String virologistName, String fieldName){
        var virologist = objectMap.<Virologist>getObject(virologistName);
        var field = objectMap.<Field>getObject(fieldName);
        virologist.move(field);
    }

    @Then("{string} is on {string}")
    public void is_on(String virologistName, String fieldName) {
        var virologist = objectMap.<Virologist>getObject(virologistName);
        var field = objectMap.<Field>getObject(fieldName);
        assertSame(virologist.getField(), field);

    }


}
