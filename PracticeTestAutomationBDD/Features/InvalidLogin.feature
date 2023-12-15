Feature: InvalidLogin

A short summary of the feature

@SmokeTest_InvalidUserName
Scenario Outline: Login with invalid username
	Given User will be on the login page
	When User will type '<username>' in the username field
	* User will type '<password>' in the password field
	* User will click submit button
	Then Invalid username message should be show on the same page
Examples:
	| username    | password        |
	| invalidUser | Password123     |

@SmokeTest_InvalidPassword
Scenario Outline: Login with invalid password
	Given User will be on the login page
	When User will type '<username>' in the username field
	* User will type '<password>' in the password field
	* User will click submit button
	Then Invalid password message should be show on the same page
Examples:
	| username | password        |
	| student  | invalidPassword |
