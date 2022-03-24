using System;
using InvokeWithRetry;
using NUnit.Framework;

namespace TestsInvokeWithRetry;

public class Tests
{
    private static int attempts;
    private static int attemptsExceptions;
    private static int numberExceptionsThrown = 10;

    private readonly Action countingAction = () => attempts++;

    private readonly Action throwExceptionsAction = () =>
    {
        if (attemptsExceptions < numberExceptionsThrown)
        {
            attemptsExceptions++;
            throw new Exception();
        }
    };

    [Test]
    public void TestWithThrowExceptionsAction()
    {
        var lessNumberExceptionsThrown = Program.InvokeWithRetry(throwExceptionsAction, 6);
        var moreNumberExceptionsThrown = Program.InvokeWithRetry(throwExceptionsAction, 14);
        Assert.AreEqual(false, lessNumberExceptionsThrown);
        Assert.AreEqual(true, moreNumberExceptionsThrown);
    }

    [Test]
    public void TestWithСountingAction()
    {
        Assert.AreEqual(true, Program.InvokeWithRetry(countingAction, 4));
        Assert.AreEqual(1, attempts);
    }
}