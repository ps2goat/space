using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceApi.Models;

namespace SpaceApiTests
{
    [TestClass]
    public class LaunchPadFilterOptionsTests
    {
        [TestMethod]
        public void OptionsDoesNotHaveFiltersIfNoFiltersAreSpecified()
        {
            LaunchPadFilterOptions options = new LaunchPadFilterOptions();

            Assert.IsFalse(options.HasFilters(), "No filters were specified, it should not have filters.");
        }
        
        [TestMethod]
        public void OptionsDoesNotHaveFiltersIfFiltersAreEmpty()
        {
            LaunchPadFilterOptions options = new LaunchPadFilterOptions
            {
                FullName = string.Empty,
                Status = string.Empty
            };

            Assert.IsFalse(options.HasFilters(), "Filters were empty, the options object should not have filters.");
        }        
        
        [TestMethod]
        public void OptionsHasFiltersIfFullNameIsSpecified()
        {
            LaunchPadFilterOptions options = new LaunchPadFilterOptions
            {
                FullName = "Test"
            };

            Assert.IsTrue(options.HasFilters(), "The options object should have filters; the full name was specified.");
        }
        
        [TestMethod]
        public void OptionsHasFiltersIfStatusIsSpecified()
        {
            LaunchPadFilterOptions options = new LaunchPadFilterOptions
            {
                Status = "Active"
            };

            Assert.IsTrue(options.HasFilters(), "The options object should have filters; the status was specified.");
        }        
        
        [TestMethod]
        public void OptionsHasFiltersIfStatusAndFullNameAreSpecified()
        {
            LaunchPadFilterOptions options = new LaunchPadFilterOptions
            {
                FullName = "Test",
                Status = "Active"
            };

            Assert.IsTrue(options.HasFilters(), "The options object should have filters; the status and full name were specified.");
        }
    }
}
