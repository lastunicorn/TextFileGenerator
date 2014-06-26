using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Serialization
{
    public class OptionsSerializer
    {
        public void Serialize(Stream outputStream, GeneratorOptions generatorOptions)
        {
            var textFileGenerator = CreateObject(generatorOptions);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";

            using (XmlWriter sw = XmlWriter.Create(outputStream, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(textFileGenerator));
                serializer.Serialize(sw, textFileGenerator);
            }
        }

        private static textFileGenerator CreateObject(GeneratorOptions generatorOptions)
        {
            textFileGenerator textFileGenerator = new textFileGenerator();

            //if (generatorOptions.Sections.Count > 0 || generatorOptions.Sections.Separator != null || generatorOptions.Sections.SeparatorType != SeparatorType.Infix)
            //{
            //    textFileGenerator.sections = new sections();
            //}

            //if (generatorOptions.Sections.Separator != null)
            //{
            //    textFileGenerator.Item.separator = generatorOptions.Sections.Separator;
            //}

            //if (generatorOptions.Sections.SeparatorType != SeparatorType.Infix)
            //{
            //    switch (generatorOptions.Sections.SeparatorType)
            //    {
            //        case SeparatorType.Infix:
            //            textFileGenerator.Item.separatorType = separatorType.Infix;
            //            break;

            //        case SeparatorType.Prefix:
            //            textFileGenerator.Item.separatorType = separatorType.Prefix;
            //            break;

            //        case SeparatorType.Postfix:
            //            textFileGenerator.Item.separatorType = separatorType.Postfix;
            //            break;
            //    }
            //}

            bool existsSections = generatorOptions.Sections.Count > 0;

            if (existsSections)
            {
                textFileGenerator.sections = new section[generatorOptions.Sections.Count];

                for (int i = 0; i < generatorOptions.Sections.Count; i++)
                {
                    Section sourceSection = generatorOptions.Sections[i];
                    section destinationSection = CreateSection(sourceSection);

                    textFileGenerator.sections[i] = destinationSection;
                }
            }

            //textFileGenerator.Item = new sections();
            //textFileGenerator.Item.separator = "";
            //textFileGenerator.Item.separatorType = sectionSeparatorType.Infix;
            //textFileGenerator.Item.section = new[]
            //{
            //    new section
            //    {
            //        name = "Header",
            //        count = "1",
            //        separator = "",
            //        separatorType = sectionSeparatorType.Infix,
            //        separatorTypeSpecified = true,
            //        template = "This is a file.\r\n===============\r\n\r\n"
            //    },
            //    new section
            //    {
            //        name = "Body",
            //        count = "100",
            //        separator = "\r\n",
            //        separatorType = sectionSeparatorType.Infix,
            //        separatorTypeSpecified = true,
            //        template = "My email address is {email}. This is a test abc {p1}; def {p2}; ghi {p3}; jkl {p4}",
            //        parameters = new[]
            //        {
            //            new parameter
            //            {
            //                key = "email",
            //                Item = new parameterConstant {value = "me@alez.ro"}
            //            },
            //            new parameter
            //            {
            //                key = "p1",
            //                Item = new parameterCounter {format = "000", startValue = "0", step = "1"}
            //            }
            //        }
            //    },
            //    new section
            //    {
            //        name = "Footer",
            //        count = "1",
            //        separator = "",
            //        separatorType = sectionSeparatorType.Infix,
            //        separatorTypeSpecified = true,
            //        template = "Copyleft lastunicorn."
            //    }
            //};

            return textFileGenerator;
        }

        private static section CreateSection(Section sourceSection)
        {
            section destinationSection = new section();

            destinationSection.name = sourceSection.Name;
            destinationSection.repeat = sourceSection.RepeatCount.ToString(CultureInfo.InvariantCulture);
            destinationSection.separator = sourceSection.Separator;

            switch (sourceSection.SeparatorType)
            {
                case SeparatorType.Infix:
                    destinationSection.separatorType = separatorType.Infix;
                    break;

                case SeparatorType.Prefix:
                    destinationSection.separatorType = separatorType.Prefix;
                    break;

                case SeparatorType.Postfix:
                    destinationSection.separatorType = separatorType.Postfix;
                    break;
            }

            if (sourceSection.Template != null)
            {
                destinationSection.Items = new object[]
                {
                    sourceSection.Template
                };
            }

            bool existsParameters = sourceSection.Parameters.Count > 0;

            if (existsParameters)
            {
                destinationSection.parameters = new parameter[sourceSection.Parameters.Count];

                for (int i = 0; i < sourceSection.Parameters.Count; i++)
                {
                    IParameter sourceParameter = sourceSection.Parameters[i];

                    parameter destinationParameter = new parameter();
                    destinationParameter.key = sourceParameter.Key;

                    destinationSection.parameters[i] = destinationParameter;
                }
            }

            return destinationSection;
        }
    }
}
