﻿namespace Kola.Domain.Composition
{
    using System;
    using System.Linq;

    using Kola.Domain.Composition.Amendments;
    using Kola.Domain.Composition.PropertyValues;
    using Kola.Domain.Extensions;
    using Kola.Domain.Specifications;

    public class AmendmentApplyingVisitor : IAmendmentVisitor
    {
        private readonly Template template;

        private readonly IComponentSpecificationLibrary specificationLibrary;

        public AmendmentApplyingVisitor(Template template, IComponentSpecificationLibrary specificationLibrary)
        {
            this.template = template;
            this.specificationLibrary = specificationLibrary;
        }

        public void Visit(AddComponentAmendment amendment)
        {
            var specification = this.specificationLibrary.Lookup(amendment.ComponentName);
            var component = specification.Create();

            var parent = this.template.FindCollection(amendment.TargetPath.TakeAllButLast());
            var index = amendment.TargetPath.Last();

            parent.Insert(index, component);
        }

        public void Visit(MoveComponentAmendment amendment)
        {
            var component = this.template.FindComponent(amendment.SourcePath);
            var sourceParent = this.template.FindCollection(amendment.SourcePath.TakeAllButLast());
            var targetParent = this.template.FindCollection(amendment.TargetPath.TakeAllButLast());
            var targetIndex = amendment.TargetPath.Last();

            sourceParent.RemoveAt(amendment.SourcePath.Last());
            targetParent.Insert(targetIndex, component);
        }

        public void Visit(RemoveComponentAmendment amendment)
        {
            var index = amendment.ComponentPath.Last();
            var parent = this.template.FindCollection(amendment.ComponentPath.TakeAllButLast());

            parent.RemoveAt(index);
        }

        public void Visit(DuplicateComponentAmendment amendment)
        {
            var component = this.template.FindComponent(amendment.ComponentPath);
            var parent = this.template.FindCollection(amendment.ComponentPath.TakeAllButLast());

            var clone = component.Clone();
            var index = amendment.ComponentPath.Last() + 1;

            parent.Insert(index, clone);
        }

        public void Visit(SetPropertyFixedAmendment amendment)
        {
            var component = this.template.FindComponentWithProperties(amendment.ComponentPath);
            var specification = this.specificationLibrary.Lookup(component.Name);

            var property = component.FindOrCreateProperty(specification.Properties.Find(amendment.PropertyName));

            property.Value = new FixedPropertyValue(amendment.FixedValue);
        }

        public void Visit(SetPropertyInheritedAmendment amendment)
        {
            var component = this.template.FindComponentWithProperties(amendment.ComponentPath);
            var specification = this.specificationLibrary.Lookup(component.Name);

            var property = component.FindOrCreateProperty(specification.Properties.Find(amendment.PropertyName));

            property.Value = new InheritedPropertyValue(amendment.Key);
        }

        public void Visit(SetPropertyMultilingualAmendment amendment)
        {
            var component = this.template.FindComponentWithProperties(amendment.ComponentPath);
            var specification = this.specificationLibrary.Lookup(component.Name);
        }

        public void Visit(SetCommentAmendment amendment)
        {
            var component = this.template.FindComponentWithProperties(amendment.ComponentPath);
            
            component.Comment = amendment.Comment;
        }

        public void Visit(ResetPropertyAmendment amendment)
        {
            var component = this.template.FindComponentWithProperties(amendment.ComponentPath);

            var specification = this.specificationLibrary.Lookup(component.Name);
            var propertySpecification = specification.Properties.Find(amendment.PropertyName);

            if (!string.IsNullOrEmpty(propertySpecification.DefaultValue))
            {
                var property = component.FindOrCreateProperty(propertySpecification);

                property.Value = new FixedPropertyValue(propertySpecification.DefaultValue);
            }
            else
            {
                var property = component.Properties.Find(amendment.PropertyName);

                if (property != null)
                {
                    component.RemoveProperty(property);
                }
            }
        }
    }
}