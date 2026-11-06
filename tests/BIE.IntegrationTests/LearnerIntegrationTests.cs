using System.Net;
using System.Net.Http.Json;
using BIE.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bie.IntegrationTests;

[TestClass]
public class LearnerIntegrationTests
{
    [TestMethod]
    public async Task TouristAt30DaysIsSavedAsApproved()
    {
        using var app = new BieFactory();
        using var client = app.CreateClient();
        var entry = new Entry("Sam Test", "Orion", "Tourismus", 30, false, false, false, true);
        var response = await client.PostAsJsonAsync("/api/cases", entry);
        var records = await client.GetFromJsonAsync<List<Case>>("/api/cases");
        if (records == null)
            Assert.Fail("No records created or found.");
        // TODO: HTTP-Status, gespeicherte Werte und Entscheidung prüfen.
        //Assert.Fail("TODO: Assertions gemäss Arbeitsauftrag ergänzen.");
        var httpStatus = response.StatusCode;
        Assert.AreEqual(HttpStatusCode.Created, httpStatus);
        Assert.AreEqual(1, records.Count);
        Assert.AreEqual("Sam Test", records[0].Applicant.Name);
        Assert.AreEqual("Orion", records[0].Applicant.Planet);
        Assert.AreEqual("Tourismus", records[0].Applicant.Purpose);
        Assert.AreEqual(30, records[0].Applicant.Days);
        Assert.AreEqual(1, records[0].Id);
        Assert.AreEqual("approved", records[0].Decision.Status);
        Assert.AreEqual("approved", EntryPolicy.Evaluate(entry).Status);
    }
}
