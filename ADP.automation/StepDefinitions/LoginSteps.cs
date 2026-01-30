using NUnit.Framework;
using OrangeHRM.Automation.Pages;
using TechTalk.SpecFlow;

namespace OrangeHRM.Automation.StepDefinitions;

[Binding]
public class LoginSteps
{
	private LoginPage LoginPage => new(Hooks.Driver, Hooks.Wait);
	private DashboardPage DashboardPage => new(Hooks.Driver, Hooks.Wait);

	[Given(@"I open the OrangeHRM login page")]
	public void GivenIOpenLogin()
	{
		LoginPage.Open();
	}

	[When(@"I login using the credentials shown on the page")]
	public void WhenILoginUsingCredentialsOnPage()
	{
		var (user, pass) = LoginPage.ReadCredentialsFromPage();
		LoginPage.Login(user, pass);
	}

	[Then(@"I should be on the Dashboard page")]
	public void ThenIShouldBeOnDashboard()
	{
		Assert.That(DashboardPage.IsAt(), Is.True, "Expected Dashboard page after login.");
	}
}