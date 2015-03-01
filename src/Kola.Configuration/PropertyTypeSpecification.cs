﻿
namespace Kola.Configuration
{
    using System;

    public class PropertyTypeSpecification
    {
        public PropertyTypeSpecification(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public string EditorName { get; set; }
    }
}