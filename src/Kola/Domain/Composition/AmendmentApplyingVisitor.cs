﻿namespace Kola.Domain.Composition
{
    using System;
    using System.Linq;

    using Kola.Domain.Composition.Amendments;
    using Kola.Domain.Composition.ParameterValues;
    using Kola.Domain.Extensions;
    using Kola.Extensions;

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

        public void Visit(SetParameterFixedAmendment amendment)
        {
            var component = this.template.FindParameterisedComponent(amendment.ComponentPath);
            var specification = this.specificationLibrary.Lookup(component.Name);

            var parameter = component.Parameters.Find(amendment.ParameterName) ?? component.AddParameter(amendment.ParameterName, specification);

            parameter.Value = new FixedParameterValue(amendment.FixedValue);
        }

        public void Visit(SetParameterInheritedAmendment amendment)
        {
            throw new NotImplementedException();
        }

        public void Visit(SetParameterMultilingualAmendment amendment)
        {
            var component = this.template.FindParameterisedComponent(amendment.ComponentPath);
            var specification = this.specificationLibrary.Lookup(component.Name);

            //component.SetParameter(amendment.ParameterName, new MultilingualParameterValue(amendment.FixedValue), specification);
        }
    }
}