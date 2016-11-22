using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace VKbrd
{

    public class VKeyboard : Window
    {
        public static RoutedUICommand ButtonCallPressedCommand = new RoutedUICommand("Call","Call",typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.Enter) } ));
        public static RoutedUICommand ButtonEndPressedCommand = new RoutedUICommand("End call", "End call", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.Delete), new KeyGesture(Key.Back) }));
        public static RoutedUICommand Button1PressedCommand = new RoutedUICommand("1", "1", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.NumPad1)/*, new KeyGesture(Key.D1)*/ }));
        public static RoutedUICommand Button2PressedCommand = new RoutedUICommand("2", "2", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.NumPad2)/*, new KeyGesture(Key.D2)*/  }));
        public static RoutedUICommand Button3PressedCommand = new RoutedUICommand("3", "3", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.NumPad3)/*, new KeyGesture(Key.D3)*/  }));
        public static RoutedUICommand Button4PressedCommand = new RoutedUICommand("4", "4", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.NumPad4)/*, new KeyGesture(Key.D4)*/  }));
        public static RoutedUICommand Button5PressedCommand = new RoutedUICommand("5", "5", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.NumPad5)/*, new KeyGesture(Key.D5)*/  }));
        public static RoutedUICommand Button6PressedCommand = new RoutedUICommand("6", "6", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.NumPad6)/*, new KeyGesture(Key.D6) */ }));
        public static RoutedUICommand Button7PressedCommand = new RoutedUICommand("7", "7", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.NumPad7)/*, new KeyGesture(Key.D7)*/  }));
        public static RoutedUICommand Button8PressedCommand = new RoutedUICommand("8", "8", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.NumPad8)/*, new KeyGesture(Key.D8) */ }));
        public static RoutedUICommand Button9PressedCommand = new RoutedUICommand("9", "9", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.NumPad9)/*, new KeyGesture(Key.D9)*/  }));
        public static RoutedUICommand Button0PressedCommand = new RoutedUICommand("0", "0", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.NumPad0)/*, new KeyGesture(Key.D0)*/  }));
        public static RoutedUICommand ButtonStarPressedCommand = new RoutedUICommand("Star", "Star", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.Multiply)/*, new KeyGesture(Key.D8, ModifierKeys.Shift)*/ }));
        public static RoutedUICommand ButtonHashPressedCommand = new RoutedUICommand("Hash", "Hash", typeof(Button),
            new InputGestureCollection(new KeyGesture[] { new KeyGesture(Key.Divide)/*, new KeyGesture(Key.D3,ModifierKeys.Shift)*/ }));
        public VKeyboard()
        {
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;            
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        static VKeyboard()
        {
            
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VKeyboard), new FrameworkPropertyMetadata(typeof(VKeyboard)));
            
            CommandBinding bCall = new CommandBinding(ButtonCallPressedCommand, ExecutedButtonPressedCommand);
            CommandBinding bEnd = new CommandBinding(ButtonEndPressedCommand, ExecutedButtonPressedCommand);
            CommandBinding b1 = new CommandBinding(Button1PressedCommand, ExecutedButtonPressedCommand);
            CommandBinding b2 = new CommandBinding(Button2PressedCommand, ExecutedButtonPressedCommand);
            CommandBinding b3 = new CommandBinding(Button3PressedCommand, ExecutedButtonPressedCommand);
            CommandBinding b4 = new CommandBinding(Button4PressedCommand, ExecutedButtonPressedCommand);
            CommandBinding b5 = new CommandBinding(Button5PressedCommand, ExecutedButtonPressedCommand);
            CommandBinding b6 = new CommandBinding(Button6PressedCommand, ExecutedButtonPressedCommand);
            CommandBinding b7 = new CommandBinding(Button7PressedCommand, ExecutedButtonPressedCommand);
            CommandBinding b8 = new CommandBinding(Button8PressedCommand, ExecutedButtonPressedCommand);
            CommandBinding b9 = new CommandBinding(Button9PressedCommand, ExecutedButtonPressedCommand);
            CommandBinding b0 = new CommandBinding(Button0PressedCommand, ExecutedButtonPressedCommand);
            CommandBinding bStar = new CommandBinding(ButtonStarPressedCommand, ExecutedButtonPressedCommand);
            CommandBinding bHash = new CommandBinding(ButtonHashPressedCommand, ExecutedButtonPressedCommand);

            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), bCall);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), bEnd);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), b1);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), b2);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), b3);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), b4);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), b5);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), b6);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), b7);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), b8);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), b9);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), b0);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), bStar);
            CommandManager.RegisterClassCommandBinding(typeof(VKeyboard), bHash);
        }

       

        static void ExecutedButtonPressedCommand(object s, ExecutedRoutedEventArgs e)
        {
            VKeyboard kbd = s as VKeyboard;
            if (kbd != null)
            {
                if (e.Command == Button0PressedCommand)
                {
                    kbd.DialedNumber += "0";
                }
                else if (e.Command == Button1PressedCommand)
                {
                    kbd.DialedNumber += "1";
                }
                else if (e.Command == Button2PressedCommand)
                {
                    kbd.DialedNumber += "2";
                }
                else if (e.Command == Button3PressedCommand)
                {
                    kbd.DialedNumber += "3";
                }
                else if (e.Command == Button4PressedCommand)
                {
                    kbd.DialedNumber += "4";
                }
                else if (e.Command == Button5PressedCommand)
                {
                    kbd.DialedNumber += "5";
                }
                else if (e.Command == Button6PressedCommand)
                {
                    kbd.DialedNumber += "6";
                }
                else if (e.Command == Button7PressedCommand)
                {
                    kbd.DialedNumber += "7";
                }
                else if (e.Command == Button8PressedCommand)
                {
                    kbd.DialedNumber += "8";
                }
                else if (e.Command == Button9PressedCommand)
                {
                    kbd.DialedNumber += "9";
                }
                else if (e.Command == ButtonHashPressedCommand)
                {
                    kbd.DialedNumber += "#";
                }
                else if (e.Command == ButtonStarPressedCommand)
                {
                    kbd.DialedNumber += "*";
                }
                else if (e.Command == ButtonCallPressedCommand)
                {
                    kbd.DialedNumber = default(string);
                    kbd.RaiseCallEvent();
                }
                else if (e.Command == ButtonEndPressedCommand)
                {
                    kbd.DialedNumber = default(string);
                    kbd.Hide();
                }
            }
        }



        public string DialedNumber
        {
            get { return (string)GetValue(DialedNumberProperty); }
            private set { SetValue(DialedNumberPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey DialedNumberPropertyKey =
            DependencyProperty.RegisterReadOnly("DialedNumber", typeof(string), typeof(VKeyboard), new UIPropertyMetadata(default(string)));
        public static readonly DependencyProperty DialedNumberProperty = DialedNumberPropertyKey.DependencyProperty;



        public static readonly RoutedEvent CallEvent = EventManager.RegisterRoutedEvent("Call", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(VKeyboard));

        public event RoutedEventHandler Call
        {
            add { AddHandler(CallEvent, value); }
            remove { RemoveHandler(CallEvent, value); }
        }

        void RaiseCallEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(VKeyboard.CallEvent);
            RaiseEvent(newEventArgs);
        }

        public static bool GetAttachVKeyboard(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachVKeyboardProperty);
        }

        public static void SetAttachVKeyboard(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachVKeyboardProperty, value);
        }

        public static readonly DependencyProperty AttachVKeyboardProperty =
            DependencyProperty.RegisterAttached("AttachVKeyboard", typeof(bool), typeof(VKeyboard), new UIPropertyMetadata(default(bool), AttachVKeyboardPropertyChanged));

        

        static void AttachVKeyboardPropertyChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            if (s is Panel)
            {
                Panel p = s as Panel;
                if (p.IsLoaded)
                {
                    OnMultiHostLoaded(p, null);
                }
                else
                {
                    p.AddHandler(Panel.LoadedEvent, new RoutedEventHandler(OnMultiHostLoaded), true);
                }
            }
            else if (s is Decorator)
            {
                Decorator d = s as Decorator;
                if (d.IsLoaded)
                {
                    OnSingleHostLoaded(d, null);
                }
                else
                {
                    d.AddHandler(Panel.LoadedEvent, new RoutedEventHandler(OnSingleHostLoaded), true);
                }
                
            }
            else if (s is TextBoxBase)
            {
                TextBoxBase control = s as TextBoxBase;
                if ((bool)e.NewValue)
                {
                    control.AddHandler(TextBoxBase.GotFocusEvent, new RoutedEventHandler(OnHostFocused), true);
                    control.AddHandler(TextBoxBase.LostFocusEvent, new RoutedEventHandler(OnHostUnFocused), true);
                }
                else
                {
                    control.RemoveHandler(TextBoxBase.GotFocusEvent, new RoutedEventHandler(OnHostFocused));
                    control.RemoveHandler(TextBoxBase.LostFocusEvent, new RoutedEventHandler(OnHostUnFocused));
                }
            }
        }

        static void OnSingleHostLoaded(object s, RoutedEventArgs e)
        {
            Decorator d = s as Decorator;
            bool val = GetAttachVKeyboard(d);
            DependencyPropertyChangedEventArgs ev = new DependencyPropertyChangedEventArgs(VKeyboard.AttachVKeyboardProperty, !val, val);
            AttachVKeyboardPropertyChanged(d.Child, ev);
            d.RemoveHandler(Decorator.LoadedEvent, new RoutedEventHandler(OnSingleHostLoaded));

        }

        static void OnMultiHostLoaded(object s, RoutedEventArgs e)
        {
            Panel p = s as Panel;
            bool val = GetAttachVKeyboard(p);
            DependencyPropertyChangedEventArgs ev = new DependencyPropertyChangedEventArgs(VKeyboard.AttachVKeyboardProperty, !val, val);
            for (int i = 0; i < p.Children.Count; i++)
            {
                AttachVKeyboardPropertyChanged(p.Children[i], ev);
            }
            p.RemoveHandler(Panel.LoadedEvent, new RoutedEventHandler(OnMultiHostLoaded));
        }

        static void OnHostFocused(object s, RoutedEventArgs e)
        {
            TextBox tb = s as TextBox;
            if (CurrentKeyboard == null)
            {
                CurrentKeyboard = new VKeyboard();
                tb.Unloaded += new RoutedEventHandler(tb_Unloaded);
                CurrentKeyboard.HookToHandle((HwndSource)PresentationSource.FromVisual(tb));
            }
            CurrentKeyboard.SetValue(VKeyboard.DialedNumberPropertyKey, tb.Text);
            Binding b = new Binding();
            b.Source = CurrentKeyboard;
            b.Path = new PropertyPath(VKeyboard.DialedNumberProperty);
            b.Mode = BindingMode.OneWay;
            tb.SetBinding(TextBox.TextProperty, b);
            CurrentKeyboard.Client = tb;
            
        }

        

        static void OnHostUnFocused(object s, RoutedEventArgs e)
        {
            TextBox tb = s as TextBox;
            string str = tb.Text;
            BindingOperations.ClearBinding(tb,TextBox.TextProperty);
            tb.Text = str;
            if (CurrentKeyboard != null)
            {
                CurrentKeyboard.DialedNumber = default(string);
                CurrentKeyboard.Client = null;
            }
        }

        static void tb_Unloaded(object sender, RoutedEventArgs e)
        {
            if (CurrentKeyboard != null)
                CurrentKeyboard.Close();
        }

        static VKeyboard CurrentKeyboard;

        internal void HookToHandle(HwndSource source)
        {
            source.AddHook(new HwndSourceHook(WindowProc));
        }

        FrameworkElement m_client = null;
        public FrameworkElement Client
        {
            set
            {
                m_client = value;
                setPosition();
                
            }
        }

        void setPosition()
        {
            if (m_client != null)
            {

                Point p = new Point(0, m_client.ActualHeight + 2);
                Point sp = m_client.PointToScreen(p);
                this.Left = sp.X;
                this.Top = sp.Y;
                this.Show();
            }
            else
            {
                this.Hide();
            }
        }

        System.IntPtr WindowProc(
              System.IntPtr hwnd,
              int msg,
              System.IntPtr wParam,
              System.IntPtr lParam,
              ref bool handled)
        {
            switch (msg)
            {
                case 0x0003:/* WM_MOVE  */
                    setPosition();
                    break;
            }

            return IntPtr.Zero;
        }


    }
}
