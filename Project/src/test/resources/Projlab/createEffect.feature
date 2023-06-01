Feature: createEffect
  Scenario: createAmnesia
    Given create effect "Amnesia"
    Then I should be told "Amnesia objektum sikeresen létrehozva."
  Scenario: createParalysis
    Given create effect "Paralysis"
    Then I should be told "Paralysis objektum sikeresen létrehozva."
  Scenario: createBearDance
    Given create effect "BearDance"
    Then I should be told "BearDance objektum sikeresen létrehozva."
  Scenario: createChorea
    Given create effect "Chorea"
    Then I should be told "Chorea objektum sikeresen létrehozva."
  Scenario: createDeath
    Given create effect "Death"
    Then I should be told "Death objektum sikeresen létrehozva."
  Scenario: createRepelling
    Given create effect "Repelling"
    Then I should be told "Repelling objektum sikeresen létrehozva."