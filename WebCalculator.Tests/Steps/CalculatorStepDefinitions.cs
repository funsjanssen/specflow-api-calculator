using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebCalculator.Tests.Steps;

[Binding]
public sealed class CalculatorStepDefinitions
{
    private readonly HttpClient _httpClient;
    private readonly CalculatorContext _calculatorContext;
    
    private HttpResponseMessage _response = default!;

    public CalculatorStepDefinitions(WebApplicationFactory<Program> httpClientFactory, CalculatorContext calculatorContext)
    {
        _httpClient = httpClientFactory.CreateClient();
        _calculatorContext = calculatorContext;
    }

    [Given("the first number is (.*)")]
    public void GivenTheFirstNumberIs(int number)
    {
        _calculatorContext.FirstNumber = number;
    }

    [Given("the second number is (.*)")]
    public void GivenTheSecondNumberIs(int number)
    {
        _calculatorContext.SecondNumber = number;
    }

    [When("the two numbers are added")]
    public async Task WhenTheTwoNumbersAreAdded()
    {
        _response = await _httpClient.GetAsync($"/{_calculatorContext.FirstNumber}/add/{_calculatorContext.SecondNumber}");
    }

    [Then("the result should be (.*)")]
    public async Task ThenTheResultShouldBe(int result)
    {
        var actualResult = Convert.ToInt32(await _response.Content.ReadAsStringAsync());
        
        actualResult.Should().Be(result, "it's simple mathematics");
    }
}