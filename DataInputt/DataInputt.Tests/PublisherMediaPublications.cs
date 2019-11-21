using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUI.UIA3;

namespace DataInputt.Tests
{
    [TestClass]
    public class PublisherMediaPublications
    {
        [TestMethod]
        public void CreatePublisher()
        {
            var app = FlaUI.Core.Application.Attach("DataInputt.exe");
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);

                // Find and switch to Publisher 
                var publisherTab = window.FindFirstDescendant(cf => cf.ByAutomationId("publisherTab"));
                publisherTab.Click(); // When on the Media tab, the test would select the TextBlock "Publisher" instead of the TabItem

                // Find inputs
                var inputBezeichnung = window.FindFirstDescendant(cf => cf.ByAutomationId("publisherInputBezeichnung")).AsTextBox();
                var inputUrl = window.FindFirstDescendant(cf => cf.ByAutomationId("publisherInputUrl")).AsTextBox();

                // Set example inputs
                inputBezeichnung.Text = "Lame Publishing Ltd.";
                inputUrl.Text = "http://lame-publishing.com";

                // Save information
                var buttonSave = window.FindFirstDescendant(cf => cf.ByAutomationId("publisherButtonSave")).AsButton();
                buttonSave.Click();

                // Check if the list element exists
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("Lame Publishing Ltd.")).AsGridCell();
                }
                catch (NullReferenceException)
                {
                    Assert.Fail("The desired list element does not exist.");
                }
            }
        }


        [TestMethod]
        public void CreateMedium()
        {
            var app = FlaUI.Core.Application.Attach("DataInputt.exe");
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);

                // Find and switch to Media tab
                var mediaTab = window.FindFirstDescendant(cf => cf.ByText("Media")).AsTabItem();
                mediaTab.Click();

                // Find inputs
                var inputBoxes = window.FindAllDescendants(cf => cf.ByClassName("TextBox"));
                var inputPublisher = window.FindFirstDescendant(cf => cf.ByClassName("ComboBox")).AsComboBox();

                // Set example inputs
                inputBoxes[3].AsTextBox().Text = "A Lame Book";
                inputBoxes[4].AsTextBox().Text = "http://lame-publishing.com/books/a-lame-book";
                try
                {
                    inputPublisher.Select(0);
                }
                catch (IndexOutOfRangeException)
                {
                    var resetButton = window.FindFirstDescendant(cf => cf.ByText("Zurücksetzen")).AsButton();
                    resetButton.DoubleClick();
                    Assert.Fail("No publisher could be found. Please run CreatePublisher first and make sure that it completes without any errors.");
                }

                // Save information
                var saveButton = window.FindFirstDescendant(cf => cf.ByText("Speichern")).AsButton();
                saveButton.DoubleClick();

                // Check if the list element exists
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("A Lame Book")).AsGridCell();
                }
                catch (NullReferenceException)
                {
                    Assert.Fail("The desired list element does not exist.");
                }
            }
        }

        [TestMethod]
        public void CreatePublication()
        {
            var app = FlaUI.Core.Application.Attach("DataInputt.exe");
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);

                // Find and switch to Publications tab
                var publicationsTab = window.FindFirstDescendant(cf => cf.ByText("Publikationen")).AsTabItem();
                publicationsTab.Click();

                // Find inputs
                var inputName = window.FindFirstDescendant(cf => cf.ByAutomationId("publicationsInputName")).AsTextBox();
                var inputTyp = window.FindFirstDescendant(cf => cf.ByAutomationId("publicationsInputTyp")).AsComboBox();
                var inputDatum = window.FindFirstDescendant(cf => cf.ByAutomationId("publicationsInputDatum")).AsTextBox();
                var inputBeschreibung = window.FindFirstDescendant(cf => cf.ByAutomationId("publicationsInputBeschreibung")).AsTextBox();
                var inputLink = window.FindFirstDescendant(cf => cf.ByAutomationId("publicationsInputLink")).AsTextBox();
                var inputMedium = window.FindFirstDescendant(cf => cf.ByAutomationId("publicationsInputMedium")).AsComboBox();

                // Set example inputs
                inputName.Text = "My Lame Presentation";
                inputTyp.Select(1);
                inputDatum.Text = "19.11.2019";
                inputBeschreibung.Text = "A presentation about being lame";
                inputLink.Text = "http://mysite.example/publications/20191119-my-lame-presentation";
                try
                {
                    inputMedium.Select(0);
                }
                catch (IndexOutOfRangeException)
                {
                    var resetButton = window.FindFirstDescendant(cf => cf.ByText("Zurücksetzen")).AsButton();
                    resetButton.DoubleClick();
                    Assert.Fail("No medium could be found. Please run CreateMedium first and make sure that it completes without any errors.");                   
                }

                // Save information
                var saveButton = window.FindFirstDescendant(cf => cf.ByAutomationId("publicationsButtonSave"));
                saveButton.DoubleClick();

                // Check if the list element exists
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("My Lame Presentation")).AsGridCell();
                } 
                catch (NullReferenceException)
                {
                    Assert.Fail("The desired list element does not exist.");
                }
            }
        }
    }
}
