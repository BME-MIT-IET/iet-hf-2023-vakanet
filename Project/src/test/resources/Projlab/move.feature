Feature: move
  Scenario: move
    Given create field "f1"
    Given create field "f2"
    Given "f2" is neighbour of "f1"
    Given create virologist "v1" on field "f1"
    When move "v1" to "f2"
    Then "v1" is on "f2"