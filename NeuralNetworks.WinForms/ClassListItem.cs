// <copyright file="ClassListItem.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    /// <summary>
    /// A ListItem in a ComboBox which returns an instance of a given type
    /// and displays its type name without the namespace.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    public record ClassListItem<T>(T instance)
        where T : notnull
    {
        /// <summary>
        /// Gets the short type name (no namespace) of the instance.
        /// </summary>
        public string TypeName => this.instance.GetType().Name;

        /// <inheritdoc />
        public override string ToString() => this.TypeName;
    }
}
