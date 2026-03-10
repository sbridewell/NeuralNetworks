// <copyright file="ClassListItem.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.WinForms
{
    /// <summary>
    /// A ListItem in a ComboBox which returns an instance of a given type
    /// and displays its display name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    public record ClassListItem<T>(T instance)
        where T : notnull, IHaveADisplayName
    {
        /// <summary>
        /// Gets the display name of the class.
        /// </summary>
        public string DisplayName => this.instance.DisplayName;

        /// <inheritdoc />
        public override string ToString() => this.DisplayName;
    }
}
