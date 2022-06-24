Feature: Metrics

Background:
	Given my api key is 'yyy'

@ignore
Scenario: Checking for count accepted
	When I query my deliverability for count_accepted
	Then it should return a 200
	And it should return some metrics count
@ignore	
Scenario: Bounce reasons
	When I query my bounce reasons
	Then it should return a 200
	And it should return some metrics count