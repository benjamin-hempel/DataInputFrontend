using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUI.UIA3;

namespace DataInputt.Tests
{
    [TestClass]
    public class Projects
    {
        [TestMethod]
        public void CreateProject()
        {
            var app = FlaUI.Core.Application.Attach("DataInputt.exe");
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);

                // Find and switch to Projects tab
                var projectsTab = window.FindFirstDescendant(cf => cf.ByText("Projects")).AsTabItem();
                projectsTab.Click();

                // Get inputs
                var inputKurzbeschreibung = window.FindFirstDescendant(cf => cf.ByAutomationId("projectsInputKurzbeschreibung")).AsTextBox();
                var inputPosition = window.FindFirstDescendant(cf => cf.ByAutomationId("projectsInputPosition")).AsTextBox();
                var inputVon = window.FindFirstDescendant(cf => cf.ByAutomationId("projectsInputVon")).AsTextBox();
                var inputBis = window.FindFirstDescendant(cf => cf.ByAutomationId("projectsInputBis")).AsTextBox();
                var inputBeschreibung = window.FindFirstDescendant(cf => cf.ByAutomationId("projectsInputBeschreibung")).AsTextBox();
                var inputAufgaben = window.FindFirstDescendant(cf => cf.ByAutomationId("projectsInputAufgaben")).AsTextBox();
                var inputTools = window.FindFirstDescendant(cf => cf.ByAutomationId("projectsInputTools")).AsTextBox();
                var inputBranche = window.FindFirstDescendant(cf => cf.ByAutomationId("projectsInputBranche")).AsTextBox();

                // Set example inputs
                inputKurzbeschreibung.Text = "Unit test example";
                inputPosition.Text = "Project Leader";
                inputVon.Text = "01.12.2019";
                inputBis.Text = "31.01.2020";
                inputBeschreibung.Text = "This is a sample unit test description";
                inputAufgaben.Text = "Eat,Sleep,Code,Repeat";
                inputTools.Text = "Visual Studio 2019,FlaUI";
                inputBranche.Text = "Automotive";

                // Save information
                var buttonSave = window.FindFirstDescendant(cf => cf.ByAutomationId("projectsButtonSave")).AsButton();
                buttonSave.Click();

                // Check if the list element exists
                try
                {
                    window.FindFirstDescendant(cf => cf.ByText("Unit test example")).AsGridCell();
                } catch (NullReferenceException)
                {
                    Assert.Fail("The desired list element does not exist.");
                }           
            }
        }
    }
}
