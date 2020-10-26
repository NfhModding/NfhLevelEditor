using System;
using System.Collections.Generic;
using System.Text;

namespace Format.Xml
{
    /// <summary>
    /// A helper-type to construct XML.
    /// </summary>
    internal class XmlBuilder
    {
        private class Node
        {
            public string Name { get; set; }
            public int AttributesIndex { get; set; }
            public bool HasChildren { get; set; }
        }

        private StringBuilder result = new StringBuilder();
        private Stack<Node> nodes = new Stack<Node>();

        /// <summary>
        /// Creates and initializes a new <see cref="XmlBuilder"/>.
        /// </summary>
        public XmlBuilder()
        {
            Clear();
        }

        /// <summary>
        /// Clears out the builder state, so a new XML can be built.
        /// </summary>
        public void Clear()
        {
            result.Clear();
            result.Append("<?xml version=\"1.0\"?>\n");
            nodes.Clear();
        }

        public override string ToString() => result.ToString();

        public void AddAttribute(string key, string value)
        {
            if (nodes.Count == 0)
            {
                return;
            }
            var node = nodes.Peek();
            value = XmlString.Escape(value);
            string attribute = $" {key}=\"{value}\"";
            result.Insert(node.AttributesIndex, attribute);
            node.AttributesIndex += attribute.Length;
        }

        public void AddTextNode(string name, string content)
        {
            DisableCompoundClose();
            Indent();
            content = XmlString.Escape(content);
            var node = $"<{name}>{content}</{name}>\n";
            result.Append(node);
        }

        public void PushNode(string name)
        {
            DisableCompoundClose();
            Indent();
            result.Append($"<{name}");
            int attributesIndex = result.Length;
            result.Append($">\n");
            nodes.Push(new Node
            {
                Name = name,
                AttributesIndex = attributesIndex,
            });
        }

        public void PopNode()
        {
            var top = nodes.Pop();
            if (top.HasChildren)
            {
                Indent();
                result.Append($"</{top.Name}>\n");
            }
            else
            {
                result.Insert(top.AttributesIndex, '/');
            }
        }

        /// <summary>
        /// Writes the proper amount of indentation to the buffer.
        /// </summary>
        private void Indent()
        {
            result.Append(' ', 4 * nodes.Count);
        }

        public void DisableCompoundClose()
        {
            if (nodes.Count != 0)
            {
                // We mark the underlying node as dirty
                nodes.Peek().HasChildren = true;
            }
        }
    }
}
