using System;
using System.Globalization;
using Transportation.Models;
using Xunit;

namespace Transportation.Tests;

public class ShipsUnitTests
{
    [Fact]
    public void TitanicSankSpecificYear()
    {
        var expected = 1912;
        var ships = new Ships();
        var actual = ships.EndOfTitanic().Year;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TitanicSankSpecificIsoDate()
    {
        var expected = "1912-04-15";
        var ships = new Ships();
        var actual = ships.EndOfTitanic().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void BritannicSankSpecificMonth()
    {
        // ChatGPT
        var ships = new Ships();
        var date = ships.EndOfBritannic();

        // Britannic se hundió en noviembre (mes 11)
        Assert.Equal(11, date.Month);
    }

    [Fact]
    public void BrittanicSankSpecificYearsAgo()
    {
        var current = DateTime.Now.Year;
        var expected = current - 1916;
        var ships = new Ships();
        var actual = current - ships.EndOfBritannic().Year;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void OlympicWasOutOfServiceSpecificDay()
    {
        // ChatGPT
        var ships = new Ships();
        var date = ships.EndOfOlympic();

        // Olympic salió de servicio el día 12 del mes correspondiente
        Assert.Equal(12, date.Day);
    }
}
