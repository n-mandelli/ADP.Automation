Feature: Login

  Scenario: Successful login
    Given I open the OrangeHRM login page
    When I login using the credentials shown on the page
    Then I should be on the Dashboard page
