using NUnit.Framework;
using OrangeHRM.Automation.Pages;
using OrangeHRM.Automation.Support;
using TechTalk.SpecFlow;

namespace OrangeHRM.Automation.StepDefinitions;

[Binding]
public class RecruitmentSteps
{
	private readonly TestContext _ctx;
	private RecruitmentPage Recruitment => new(Hooks.Driver, Hooks.Wait);

	public RecruitmentSteps(TestContext ctx) => _ctx = ctx;

	[When(@"I navigate to Recruitment")]
	public void WhenINavigateToRecruitment()
	{
		Recruitment.GoTo();
	}

	[When(@"I add a new candidate with required fields")]
	public void WhenIAddNewCandidate()
	{
		Recruitment.ClickAdd();

		var first = "John";
		var middle = "QA";
		var last = "Candidate" + Guid.NewGuid().ToString("N")[..6];
		var email = $"john.{Guid.NewGuid().ToString("N")[..6]}@test.com";

		Recruitment.FillRequiredFields(first, middle, last, email);
		Recruitment.Save();

		_ctx.CreatedCandidateFullName = $"{first} {middle} {last}";
	}

	[Then(@"I should see a success message for candidate creation")]
	public void ThenIShouldSeeSuccessCreation()
	{
		Assert.That(Recruitment.IsSuccessToastVisible(), Is.True, "Expected success toast after candidate creation.");
	}

	[Given(@"I have created a candidate")]
	public void GivenIHaveCreatedACandidate()
	{
		if (!string.IsNullOrWhiteSpace(_ctx.CreatedCandidateFullName))
			return;

		Recruitment.GoTo();
		WhenIAddNewCandidate();
		ThenIShouldSeeSuccessCreation();
	}

	[When(@"I search the created candidate")]
	public void WhenISearchCreatedCandidate()
	{
		Assert.That(_ctx.CreatedCandidateFullName, Is.Not.Null.And.Not.Empty, "No candidate stored in context.");
		Recruitment.SearchCandidate(_ctx.CreatedCandidateFullName!);
		Recruitment.OpenFirstResult();
	}

	[When(@"I edit the candidate profile and save")]
	public void WhenIEditCandidateAndSave()
	{
		var newEmail = $"updated.{Guid.NewGuid().ToString("N")[..6]}@test.com";
		Recruitment.EditEmail(newEmail);
		Recruitment.Save();
	}

	[Then(@"I should see a success message for candidate update")]
	public void ThenIShouldSeeSuccessUpdate()
	{
		Assert.That(Recruitment.IsSuccessToastVisible(), Is.True, "Expected success toast after candidate update.");
	}
}
