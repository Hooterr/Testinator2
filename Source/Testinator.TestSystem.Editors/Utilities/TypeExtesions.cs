using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Editors
{
    public static class TypeExtesions
    {
        public static PropertyInfo[] GetAllProperties(this Type type)
        {
            if (type.IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();

                var considered = new List<Type>();
                var queue = new Queue<Type>();
                considered.Add(type);
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces())
                    {
                        if (considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties
                        .Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                return propertyInfos.ToArray();
            }

            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        
        internal static BaseNode GetHandlersTree(this Type type)
        {
            var rootNode = new EditorNode()
            {
                Name = "",
                Parent = null,
            };
            rootNode.Children = type.GetHandlersBranches(rootNode);
            return rootNode;
        }

        private static BaseNode[] GetHandlersBranches(this Type type, BaseNode parent = null)
        {
            if (!type.IsInterface)
                throw new ArgumentException("Only editor interface is valid");

            var children = new List<BaseNode>();
            var allProperties = type.GetAllProperties();
            var editorProperties = allProperties
                .Where(x => x.GetCustomAttributes<EditorPropertyAttribute>(inherit: true).Any())
                .Select(x => new PropertyNode()
                {
                    Name = x.Name,
                    Parent = parent,
                });

            children.AddRange(editorProperties);

            var editors = allProperties
                .Where(x => x.GetCustomAttributes<EditorAttribute>(inherit: true).Any())
                .Select(x => new
                {
                    handler = new EditorNode()
                    {
                        Name = x.Name,
                        Parent = parent,
                    },
                    type = x.PropertyType,
                })
                .Select(x =>
                {
                    x.handler.Children = x.type.GetHandlersBranches(parent: x.handler);
                    return x.handler;
                });

            children.AddRange(editors);

            return children.ToArray();
        }

    }
}
