using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace BIE.E2ETests;

[TestClass]
public class LearnerE2ETests : BrowserTestBase
{
    [TestMethod]
    public void TouristAt30DaysRemainsApprovedAfterReload()
    {
        Fill("Sam Browser", "Tourismus", "30");
        // TODO: Absenden, sichtbares Ergebnis abwarten und prüfen.
        //Assert.Fail("TODO: Assertions gemäss Arbeitsauftrag ergänzen.");
        Driver.FindElement(By.Id("submit")).Click();
        StringAssert.Contains(Decision("approved").Text, "GENEHMIGT");
        var caseId = Driver.FindElement(By.Id("result")).GetDomAttribute("data-application-id");
        Driver.Navigate().Refresh();
        var row = Wait.Until(d => d.FindElements(By.CssSelector("#application-list tr")).FirstOrDefault(r => r.GetDomAttribute("data-id") == caseId));
        var cells = row.FindElements(By.TagName("td"));
        // TODO: Name, Herkunft, Tage und Entscheidung nach Neuladen prüfen.
        //Assert.Fail("TODO: Assertions gemäss Arbeitsauftrag ergänzen.");
        StringAssert.Contains(cells[1].Text, "Sam Browser");
        StringAssert.Contains(cells[2].Text, "Orion");
        StringAssert.Contains(cells[3].Text, "30 Tage");
        StringAssert.Contains(cells[4].Text, "GENEHMIGT");
    }
}
