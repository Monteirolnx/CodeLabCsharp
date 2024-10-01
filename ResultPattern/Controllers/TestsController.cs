namespace ResultPattern.Controllers;

[ApiController]
[Route("[controller]")]
public class TestsController(ILogger<TestsController> logger) : ControllerBase
{
    [HttpPost("authenticate-success")]
    public async Task<IActionResult> AuthenticateSuccess()
    {
        logger.LogInformation("AuthenticateSuccess endpoint was called.");

        var account = await SimulateAccountService(true);

        return account.Map<IActionResult>(onSuccess: Ok, onFailure: BadRequest);
    }

    [HttpPost("authenticate-failure")]
    public async Task<IActionResult> AuthenticateFailure()
    {
        logger.LogInformation("AuthenticateFailure endpoint was called.");

        var account = await SimulateAccountService(false);

        return account.Map<IActionResult>(onSuccess: Ok, onFailure: BadRequest);
    }

    [HttpGet("test-exception")]
    public IActionResult TestException()
    {
        throw new Exception("Test exception");
    }

    private static async Task<Result<Account>> SimulateAccountService(bool isSuccess)
    {
        await Task.Delay(100); // Simulate a delay

        var account = new Account();

        return isSuccess
            ? Result<Account>.Success(account)
            : Result<Account>.Failure(new Error("Invalid PIN", "InvalidPIN"));
    }
}

internal class Account { }
