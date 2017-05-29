using System.Collections.Generic;
using System;
using RazorTemplates.Core;

namespace RazorTemplates.Samples
{
    class Program
    {
        // 一些使用技巧 https://github.com/aspnet/Razor/issues/788
        static void Main()
        {
            SimpleTemplate();
            TemplateWithCustomNamespaces();
            TemplateWithFunction();
            TemplateWithHelper();

            Console.ReadLine();
        }

        public static void SimpleTemplate()
        {
            var template = Template.Compile("Hello @Model.Name!");
            Console.WriteLine(template.Render(new { Name = "world" }));
        }

        public static void TemplateWithCustomNamespaces()
        {
            var template = Template
                .WithBaseType<TemplateBase>()
                .AddNamespace("RazorTemplates.Samples")
                .Compile(@"There is @Model.Apples @Plural.Form(Model.Apples, new [] { ""apple"", ""apples"" }) in the box.");

            Console.WriteLine(template.Render(new { Apples = 1 }));
            Console.WriteLine(template.Render(new { Apples = 2 }));
        }

        public static void TemplateWithFunction()
        {
            var template = Template
                .WithBaseType<TemplateBase>()
                .AddNamespace("RazorTemplates.Samples")
                .Compile(@"
                    @functions
                    {
                        public string GetSomeString()
                        {
                            var str = string.Empty;
                            var events = (List<EventModel>)Model.Events;
                            for(var i = 0; i < events.Count; i++)
                            {
                                str += events[i].Name;
                            }

                            return str;
                        }
                    }
                    
@GetSomeString()
                ");

            Console.WriteLine(template.Render(new { Events = new List<EventModel> { new EventModel { Name = "apple - " }, new EventModel { Name = "apples" } } }));
        }

        public static void TemplateWithHelper()
        {
            var template = Template
                .WithBaseType<TemplateBase>()
                .AddNamespace("RazorTemplates.Samples")
                .Compile(@"
                    @{
                        Func<string, string> func = str => { return str + 123.ToString(); };
                    }
@func(Model.Name)
                ");

            Console.WriteLine(template.Render(new { Name = "JRoger - " }));
        }
    }

    public static class Plural
    {
        public static string Form(int value, string[] forms)
        {
            var form = value == 1 ? 0 : 1;
            return forms[form];
        }
    }

    public class EventModel
    {
        public string Name { get; set; }
    }
}
