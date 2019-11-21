using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUI.UIA3;

namespace DataInputt.Tests
{
    [TestClass]
    public class DatabaseExportImport
    {
        [TestMethod]
        public void ExportImport()
        {
            var app = FlaUI.Core.Application.Attach("DataInputt.exe");

            using(var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);

                // Check if all tests from Projects and PublisherMediaPublications have been run
                var projectsTab = window.FindFirstDescendant(cf => cf.ByText("Projects")).AsTabItem();
                projectsTab.Click();
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("Unit test example")).AsGridCell();
                }
                catch (NullReferenceException)
                {
                    Assert.Fail("Please run Projects.CreateProject first and make sure that it completes without any errors.");
                }

                var publisherTab = window.FindFirstDescendant(cf => cf.ByAutomationId("publisherTab"));
                publisherTab.Click();
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("Lame Publishing Ltd.")).AsGridCell();
                }
                catch (NullReferenceException)
                {
                    Assert.Fail("Please run PublisherMediaPublications.CreatePublisher first and make sure that it completes without any errors.");
                }

                var mediaTab = window.FindFirstDescendant(cf => cf.ByText("Media")).AsTabItem();
                mediaTab.Click();
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("A Lame Book")).AsGridCell();
                }
                catch (NullReferenceException)
                {
                    Assert.Fail("Please run PublisherMediaPublications.CreateMedium first and make sure that it completes without any errors.");
                }

                var publicationsTab = window.FindFirstDescendant(cf => cf.ByText("Publikationen")).AsTabItem();
                publicationsTab.Click();
                try
                {
                   window.FindFirstDescendant(cf => cf.ByText("My Lame Presentation")).AsGridCell();
                }
                catch (NullReferenceException)
                {
                    Assert.Fail("Please run PublisherMediaPublications.CreatePublication first and make sure that it completes without any errors.");
                }

                // Export database
                var exportButton = window.FindFirstDescendant(cf => cf.ByText("Exportieren")).AsMenuItem();
                exportButton.Click();
                var exportButtonDatenbank = window.FindFirstDescendant(cf => cf.ByText("Datenbank")).AsMenuItem();
                exportButtonDatenbank.Click();

                // Delete all entries created by the other tests
                var deleteButton = window.FindFirstDescendant(cf => cf.ByText("Löschen")).AsButton();
                var publicationData = window.FindFirstDescendant(cf => cf.ByText("My Lame Presentation")).AsGridCell();
                publicationData.DoubleClick();
                deleteButton.DoubleClick();

                mediaTab.Click();
                deleteButton = window.FindFirstDescendant(cf => cf.ByText("Löschen")).AsButton();
                deleteButton.DoubleClick();

                publisherTab.Click();
                deleteButton = window.FindFirstDescendant(cf => cf.ByText("Löschen")).AsButton();
                deleteButton.DoubleClick();

                projectsTab.Click();
                var deleteButtons = window.FindAllDescendants(cf => cf.ByText("Löschen"));
                deleteButtons[3].AsButton().DoubleClick();

                // Import database
                var importButton = window.FindFirstDescendant(cf => cf.ByText("Importieren")).AsMenuItem();
                importButton.Click();
                var importButtonDatenbankNeu = window.FindFirstDescendant(cf => cf.ByText("Datenbank (Neu)")).AsMenuItem();
                importButtonDatenbankNeu.Click();

                // Check if all entries have been restored
                projectsTab.Click();
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("Unit test example")).AsGridCell();
                }
                catch (NullReferenceException)
                {
                    Assert.Fail("The example project has not been imported from the database.");
                }

                publisherTab.Click();
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("Lame Publishing Ltd.")).AsGridCell();
                }
                catch (NullReferenceException)
                {
                    Assert.Fail("The example publisher has not been imported from the database.");
                }

                mediaTab.Click();
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("A Lame Book")).AsGridCell();
                }
                catch (NullReferenceException)
                {
                    Assert.Fail("The example medium has not been imported from the database.");
                }

                publicationsTab.Click();
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("My Lame Presentation")).AsGridCell();
                }
                catch (NullReferenceException)
                {
                    Assert.Fail("The example publication has not been imported from the database.");
                }
            }
        }
    }
}
