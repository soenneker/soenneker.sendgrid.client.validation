using Soenneker.SendGrid.Client.Validation.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.SendGrid.Client.Validation.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class SendGridValidationClientUtilTests : HostedUnitTest
{
    private readonly ISendGridValidationClientUtil _util;

    public SendGridValidationClientUtilTests(Host host) : base(host)
    {
        _util = Resolve<ISendGridValidationClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
