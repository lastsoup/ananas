﻿using System;
using System.Xml;

namespace Ananas.Web.Mvc.Base
{
    /// <summary>
    /// 获取~/App_GlobalResources中的资源
    /// </summary>
    internal class ResXResource : ResourceBase
    {
        private static readonly XmlReaderSettings readerSettings = new XmlReaderSettings
        {
            IgnoreComments = true,
            IgnoreWhitespace = true,
            IgnoreProcessingInstructions = true,
            CloseInput = true
        };

        private readonly string resourceLocation;

        public ResXResource(string resourceLocation)
        {
            this.resourceLocation = resourceLocation;
        }

        protected override void Load()
        {
            using (XmlReader reader = XmlReader.Create(resourceLocation, readerSettings))
            {
                while (reader.Read())
                {
                    if (reader.LocalName.Equals("data", StringComparison.OrdinalIgnoreCase) && reader.HasAttributes)
                    {
                        string name = reader.GetAttribute("name");

                        if (!string.IsNullOrEmpty(name) && reader.ReadToDescendant("value"))
                        {
                            CurrentResources.Add(name, reader.ReadElementContentAsString());
                        }
                    }
                }
            }
        }
    }

}
