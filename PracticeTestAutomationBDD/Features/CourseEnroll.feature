Feature: CourseEnroll

A short summary of the feature

@E2E_Course_Enroll
Scenario Outline: Login and enroll for a course
	Given User will be on the login page
	When User will type '<username>' in the username field
	* User will type '<password>' in the password field
	* User will click submit button
	Then User will be redirected to success page
	When User will click on courses link
	Then User will be redirected to the courses page
	When User will click on course on position '<courseindex>'
	Then Another tab will open containing the course user clicked
Examples:
	| username | password    | courseindex |
	| student  | Password123 | 1           |
	| student  | Password123 | 2           |

