using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Text.RegularExpressions;

public class GuestUserGeneratorTest
{
    TypeOfUser userType = TypeOfUser.instance;

    [Test]
    public void CREATE_DEFAULT_Test()
    {
        // ARRANGE
        string pattern = @"default(\d+){5}";
        Regex rg = new Regex(pattern);

        // ACT
        string user = userType.CREATE_DEFAULT();
        bool isMatched = rg.IsMatch(user);

        // ASSERT
        Assert.IsTrue(isMatched);
    }
}
