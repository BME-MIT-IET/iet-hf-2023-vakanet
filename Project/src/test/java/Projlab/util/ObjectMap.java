package Projlab.util;

import virologist.Virologist;

import java.util.HashMap;

public class ObjectMap {

    private static ObjectMap instance = new ObjectMap();
    public static ObjectMap getInstance() {
        return instance;
    }
    private final HashMap<String , Object> hashmap = new HashMap<>();

    public <T> T getObject(String s) {
        return (T) hashmap.get(s);
    }

    public void addObject(String key, Object obj) {
        hashmap.put(key, obj);
    }

    public void createVirologist(String name, String fieldName) {
        this.addObject(name, new Virologist(this.getObject(fieldName),name) );
    }

    public void collect(String virologistName) {
        this.<Virologist>getObject(virologistName).collect();
    }
}
