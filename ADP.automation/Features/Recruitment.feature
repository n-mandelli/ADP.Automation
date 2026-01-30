Feature: Recruitment

  Background:
    Given I open the OrangeHRM login page
    When I login using the credentials shown on the page
    Then I should be on the Dashboard page

  Scenario: Create Candidate
    When I navigate to Recruitment
    And I add a new candidate with required fields
    Then I should see a success message for candidate creation

  Scenario: Edit Candidate
    Given I have created a candidate
    When I navigate to Recruitment
    And I search the created candidate
    And I edit the candidate profile and save
    Then I should see a success message for candidate update
