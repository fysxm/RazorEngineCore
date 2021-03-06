﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RazorEngineCore.Tests
{
    [TestClass]
    public class TestSaveLoad
    {
        [TestMethod]
        public void TestSaveToStream()
        {
            RazorEngine razorEngine = new RazorEngine();
            RazorEngineCompiledTemplate initialTemplate = razorEngine.Compile("Hello @Model.Name");
            
            MemoryStream memoryStream = new MemoryStream();
            initialTemplate.SaveToStream(memoryStream);
            memoryStream.Position = 0;

            RazorEngineCompiledTemplate loadedTemplate = RazorEngineCompiledTemplate.LoadFromStream(memoryStream);

            string initialTemplateResult = initialTemplate.Run(new { Name = "Alex" });
            string loadedTemplateResult = loadedTemplate.Run(new { Name = "Alex" });
            
            Assert.AreEqual(initialTemplateResult, loadedTemplateResult);
        }
        
        [TestMethod]
        public async Task TestSaveToStreamAsync()
        {
            RazorEngine razorEngine = new RazorEngine();
            RazorEngineCompiledTemplate initialTemplate = await razorEngine.CompileAsync("Hello @Model.Name");
            
            MemoryStream memoryStream = new MemoryStream();
            await initialTemplate.SaveToStreamAsync(memoryStream);
            memoryStream.Position = 0;

            RazorEngineCompiledTemplate loadedTemplate = await RazorEngineCompiledTemplate.LoadFromStreamAsync(memoryStream);

            string initialTemplateResult = await initialTemplate.RunAsync(new { Name = "Alex" });
            string loadedTemplateResult = await loadedTemplate.RunAsync(new { Name = "Alex" });
            
            Assert.AreEqual(initialTemplateResult, loadedTemplateResult);
        }
        
        [TestMethod]
        public void TestSaveToFile()
        {
            RazorEngine razorEngine = new RazorEngine();
            RazorEngineCompiledTemplate initialTemplate = razorEngine.Compile("Hello @Model.Name");
            
            initialTemplate.SaveToFile("testTemplate.dll");

            RazorEngineCompiledTemplate loadedTemplate = RazorEngineCompiledTemplate.LoadFromFile("testTemplate.dll");

            string initialTemplateResult = initialTemplate.Run(new { Name = "Alex" });
            string loadedTemplateResult = loadedTemplate.Run(new { Name = "Alex" });
            
            Assert.AreEqual(initialTemplateResult, loadedTemplateResult);
        }
        
        [TestMethod]
        public async Task TestSaveToFileAsync()
        {
            RazorEngine razorEngine = new RazorEngine();
            RazorEngineCompiledTemplate initialTemplate = await razorEngine.CompileAsync("Hello @Model.Name");
            
            await initialTemplate.SaveToFileAsync("testTemplate.dll");

            RazorEngineCompiledTemplate loadedTemplate = await RazorEngineCompiledTemplate.LoadFromFileAsync("testTemplate.dll");

            string initialTemplateResult = await initialTemplate.RunAsync(new { Name = "Alex" });
            string loadedTemplateResult = await loadedTemplate.RunAsync(new { Name = "Alex" });
            
            Assert.AreEqual(initialTemplateResult, loadedTemplateResult);
        }
    }
}