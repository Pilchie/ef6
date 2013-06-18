﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.ModelConfiguration.Conventions
{
    using System.Data.Entity.Config;
    using System.Data.Entity.ModelConfiguration.Configuration.Types;
    using System.Data.Entity.ModelConfiguration.Utilities;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    ///     Base class for conventions that process CLR attributes found in the model.
    /// </summary>
    /// <typeparam name="TAttribute"> The type of the attribute to look for. </typeparam>
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class TypeAttributeConfigurationConvention<TAttribute>
        : Convention
        where TAttribute : Attribute
    {
        private readonly AttributeProvider _attributeProvider = DbConfiguration.GetService<AttributeProvider>();

        protected TypeAttributeConfigurationConvention()
        {
            Types().Having(t => _attributeProvider.GetAttributes(t).OfType<TAttribute>())
                .Configure((configuration, attributes) =>
                    {
                        foreach (var attribute in attributes)
                        {
                            Apply(configuration, attribute);
                        }
                    });
        }

        public abstract void Apply(LightweightTypeConfiguration configuration, TAttribute attribute);
    }
}