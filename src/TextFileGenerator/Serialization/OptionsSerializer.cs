using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization.EntityTranslators;

namespace DustInTheWind.TextFileGenerator.Serialization
{
    public class OptionsSerializer
    {
        public void Serialize(Stream outputStream, GeneratorOptions generatorOptions)
        {
            textFileGenerator textFileGenerator = new GeneratorOptionsTranslator(generatorOptions).Create();

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "    "
            };

            using (XmlWriter sw = XmlWriter.Create(outputStream, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(textFileGenerator));
                serializer.Serialize(sw, textFileGenerator);
            }
        }

        public GeneratorOptions Deserialize(Stream inputStream)
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                ConformanceLevel = ConformanceLevel.Document,
                ValidationType = ValidationType.Schema,
            };

            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "DustInTheWind.TextFileGenerator.Serialization.TextFileGenerator.xsd";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                XmlSchema xmlSchema = XmlSchema.Read(stream, (sender, args) => { });
                settings.Schemas.Add(xmlSchema);
            }

            using (XmlReader xmlReader = XmlReader.Create(inputStream, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(textFileGenerator));
                textFileGenerator textFileGenerator = (textFileGenerator)serializer.Deserialize(xmlReader);

                GeneratorOptions generatorOptions = new GeneratorOptions();

                if (textFileGenerator.sections != null)
                    foreach (section sourceSection in textFileGenerator.sections)
                    {
                        Section destinationSection = Translate(sourceSection);

                        generatorOptions.Sections.Add(destinationSection);
                    }

                return generatorOptions;
            }
        }

        private static Section Translate(section sourceSection)
        {
            Section destinationSection = new Section
            {
                Name = sourceSection.name,
                RepeatCount = int.Parse(sourceSection.repeat),
                Separator = sourceSection.separator,
                SeparatorType = Translate(sourceSection.separatorType),
            };

            if (sourceSection.Items != null && sourceSection.Items.Length > 0)
            {
                foreach (object item in sourceSection.Items)
                {
                    if (item == null)
                        continue;

                    Type itemType = item.GetType();

                    if (itemType == typeof(string))
                        destinationSection.Template = (string)item;

                    if (itemType == typeof(section))
                    {
                        Section destinationSubsection = Translate((section)item);
                        destinationSection.Sections.Add(destinationSubsection);
                    }

                    if (sourceSection.parameter != null)
                    {
                        destinationSection.Parameters.Add(new Parameter());
                    }
                }
            }

            return destinationSection;
        }

        private static SeparatorType Translate(separatorType sourceSeparatorType)
        {
            switch (sourceSeparatorType)
            {
                case separatorType.Infix:
                    return SeparatorType.Infix;

                case separatorType.Prefix:
                    return SeparatorType.Prefix;

                case separatorType.Postfix:
                    return SeparatorType.Postfix;

                default:
                    return SeparatorType.Infix;
            }
        }
    }
}
