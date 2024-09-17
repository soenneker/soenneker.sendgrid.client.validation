using Soenneker.SendGrid.Client.Validation.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.SendGrid.Client.Validation.Tests;

[Collection("Collection")]
public class SendGridValidationClientUtilTests : FixturedUnitTest
{
    private readonly ISendGridValidationClientUtil _util;

    public SendGridValidationClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<ISendGridValidationClientUtil>(true);
    }
}
