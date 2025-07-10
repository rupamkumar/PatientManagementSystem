using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Infrastructure.Behaviors
{
   public static class ChangeBehaviors
    {

        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty IsChangedProperty;
        public static readonly DependencyProperty OriginalValueProperty;
        public static readonly DependencyProperty OriginalValueConverterProperty;

        private static readonly Dictionary<Type, DependencyProperty> _defaultProperties;

        static ChangeBehaviors()
        {
            IsActiveProperty = DependencyProperty.RegisterAttached("IsActive", typeof(bool), typeof(ChangeBehaviors), new PropertyMetadata(false, OnIsActivePropertyChanged));
            IsChangedProperty = DependencyProperty.RegisterAttached("IsChanged", typeof(bool), typeof(ChangeBehaviors), new PropertyMetadata(false));
            OriginalValueProperty = DependencyProperty.RegisterAttached("OriginalValue", typeof(object), typeof(ChangeBehaviors), new PropertyMetadata(null));
            OriginalValueConverterProperty = DependencyProperty.RegisterAttached("OriginalValueConverter", typeof(IValueConverter), typeof(ChangeBehaviors), new PropertyMetadata(null, OnOriginalValueConverterPropertyChanged));

            _defaultProperties = new Dictionary<Type, DependencyProperty>
            {
                [typeof(TextBox)] = TextBox.TextProperty,
                [typeof(CheckBox)] = CheckBox.IsCheckedProperty,
                [typeof(DatePicker)] = DatePicker.SelectedDateProperty,
                [typeof(ComboBox)] = Selector.SelectedValueProperty
            };

        }

        private static void OnOriginalValueConverterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var originalValueBinding = BindingOperations.GetBinding(d, OriginalValueProperty) as Binding;
            if (originalValueBinding != null)
            {
                CreateOriginalValueBinding(d, originalValueBinding.Path.Path);
            }
        }

        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (_defaultProperties.ContainsKey(d.GetType()))
            {
                var defaultProperty = _defaultProperties[d.GetType()];
                if ((bool)e.NewValue)
                {
                    var binding = BindingOperations.GetBinding(d, defaultProperty);
                    if (binding != null)
                    {
                        string bindingPath = binding.Path.Path;
                        BindingOperations.SetBinding(d, IsChangedProperty, new Binding(bindingPath + "IsChanged"));
                        CreateOriginalValueBinding(d, bindingPath + "OriginalValue");
                    }
                    else
                    {
                        BindingOperations.ClearBinding(d, IsChangedProperty);
                        BindingOperations.ClearBinding(d, OriginalValueProperty);
                    }
                }
            }
        }

        private static void CreateOriginalValueBinding(DependencyObject d, string originalValueBindingPath)
        {
            var newBinding = new Binding(originalValueBindingPath)
            {
                Converter = GetOriginalValueConverter(d),
                ConverterParameter = d
            };
            BindingOperations.SetBinding(d, OriginalValueProperty, newBinding);
        }

        public static bool GetIsActive(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsActiveProperty);
        }

        public static void SetIsActive(DependencyObject obj, bool value)
        {
            obj.SetValue(IsActiveProperty, value);
        }
        public static bool GetIsChanged(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsChangedProperty);
        }

        public static void SetIsChanged(DependencyObject obj, bool value)
        {
            obj.SetValue(IsChangedProperty, value);
        }

        public static object GetOriginalValue(DependencyObject obj)
        {
            return (object)obj.GetValue(OriginalValueProperty);
        }

        public static void SetOriginalValue(DependencyObject obj, object value)
        {
            obj.SetValue(OriginalValueProperty, value);
        }

        public static IValueConverter GetOriginalValueConverter(DependencyObject obj)
        {
            return (IValueConverter)obj.GetValue(OriginalValueConverterProperty);
        }

        public static void SetOriginalValueConverter(DependencyObject obj, IValueConverter value)
        {
            obj.SetValue(OriginalValueConverterProperty, value);
        }
    }
}
