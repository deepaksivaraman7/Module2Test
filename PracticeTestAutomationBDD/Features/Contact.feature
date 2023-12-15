Feature: Contact

A short summary of the feature

@E2E_Contact_Details
Scenario Outline: Login and enroll for a course
	Given User will be on the login page
	When User will type '<username>' in the username field
	* User will type '<password>' in the password field
	* User will click submit button
	Then User will be redirected to success page
	When User will click on contact link
	Then User will be redirected to the contact page
	
Examples:
	| username | password    |
	| student  | Password123 |
