﻿using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorTemplates.Core;

namespace RazorTemplates.Tests
{
    [TestClass]
    public class TemplateTests
    {
        [TestMethod]
        public void ItShouldRenderExpandoObjects()
        {
            dynamic expando = new ExpandoObject();
            var template = Template.Compile("There is @Model.Count @Model.Item in the box.");

            expando.Count = 1;
            expando.Item = "apple";

            Assert.AreEqual("There is 1 apple in the box.", template.Render(expando));

            expando.Count = 2;
            expando.Item = "apples";

            Assert.AreEqual("There is 2 apples in the box.", template.Render(expando));
        }
    }
}
