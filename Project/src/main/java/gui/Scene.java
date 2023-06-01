package gui;

public enum Scene {
    MAIN_MENU("Main Menu"),
    GAME_CREATION("Game Creation"),
    GAME("Game");

    public final String name;

    Scene(String name) {
        this.name = name;
    }

    public String getName() {
        return this.name;
    }
}
