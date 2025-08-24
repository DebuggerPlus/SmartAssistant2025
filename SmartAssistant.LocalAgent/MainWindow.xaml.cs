using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SmartAssistant.LocalAgent;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Regex _numberRegex = new Regex("[^0-9]+");

    // Windows API для работы с мышью
    [DllImport("user32.dll")]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    // Константы для mouse_event
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
    private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
    private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
    private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
    private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
    private const uint MOUSEEVENTF_MOVE = 0x0001;

    public MainWindow()
    {
        InitializeComponent();
        LogMessage("Приложение запущено. Готово к тестированию действий с мышью.");
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        e.Handled = _numberRegex.IsMatch(e.Text);
    }

    private void GetCurrentMousePosition_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (GetCursorPos(out POINT point))
            {
                XCoordinateTextBox.Text = point.X.ToString();
                YCoordinateTextBox.Text = point.Y.ToString();
                LogMessage($"Текущая позиция мыши: X={point.X}, Y={point.Y}");
            }
            else
            {
                LogMessage("Ошибка при получении позиции мыши");
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Ошибка при получении позиции мыши: {ex.Message}");
        }
    }

    private void MoveMouse_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (int.TryParse(XCoordinateTextBox.Text, out int x) &&
                int.TryParse(YCoordinateTextBox.Text, out int y))
            {
                if (SetCursorPos(x, y))
                {
                    LogMessage($"Курсор перемещен в позицию: X={x}, Y={y}");
                }
                else
                {
                    LogMessage("Ошибка при перемещении курсора");
                }
            }
            else
            {
                LogMessage("Ошибка: Некорректные координаты");
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Ошибка при перемещении курсора: {ex.Message}");
        }
    }

    private void LeftClick_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (int.TryParse(XCoordinateTextBox.Text, out int x) &&
                int.TryParse(YCoordinateTextBox.Text, out int y))
            {
                // Сначала перемещаем курсор в указанные координаты
                if (SetCursorPos(x, y))
                {
                    LogMessage($"Курсор перемещен в позицию: X={x}, Y={y}");

                    // Небольшая задержка для стабилизации позиции
                    var timer = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromMilliseconds(50)
                    };

                    timer.Tick += (s, args) =>
                    {
                        PerformMouseClick(MouseButton.Left);
                        LogMessage("Выполнен левый клик");
                        timer.Stop();
                    };

                    timer.Start();
                }
                else
                {
                    LogMessage("Ошибка при перемещении курсора");
                }
            }
            else
            {
                LogMessage("Ошибка: Некорректные координаты");
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Ошибка при выполнении левого клика: {ex.Message}");
        }
    }

    private void LeftDoubleClick_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (int.TryParse(XCoordinateTextBox.Text, out int x) &&
                int.TryParse(YCoordinateTextBox.Text, out int y))
            {
                // Сначала перемещаем курсор в указанные координаты
                if (SetCursorPos(x, y))
                {
                    LogMessage($"Курсор перемещен в позицию: X={x}, Y={y}");

                    // Небольшая задержка для стабилизации позиции
                    var timer = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromMilliseconds(50)
                    };

                    timer.Tick += (s, args) =>
                    {
                        PerformMouseDoubleClick(MouseButton.Left);
                        LogMessage("Выполнен левый двойной клик");
                        timer.Stop();
                    };

                    timer.Start();
                }
                else
                {
                    LogMessage("Ошибка при перемещении курсора");
                }
            }
            else
            {
                LogMessage("Ошибка: Некорректные координаты");
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Ошибка при выполнении левого двойного клика: {ex.Message}");
        }
    }

    private void RightClick_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (int.TryParse(XCoordinateTextBox.Text, out int x) &&
                int.TryParse(YCoordinateTextBox.Text, out int y))
            {
                // Сначала перемещаем курсор в указанные координаты
                if (SetCursorPos(x, y))
                {
                    LogMessage($"Курсор перемещен в позицию: X={x}, Y={y}");

                    // Небольшая задержка для стабилизации позиции
                    var timer = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromMilliseconds(50)
                    };

                    timer.Tick += (s, args) =>
                    {
                        PerformMouseClick(MouseButton.Right);
                        LogMessage("Выполнен правый клик");
                        timer.Stop();
                    };

                    timer.Start();
                }
                else
                {
                    LogMessage("Ошибка при перемещении курсора");
                }
            }
            else
            {
                LogMessage("Ошибка: Некорректные координаты");
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Ошибка при выполнении правого клика: {ex.Message}");
        }
    }

    private void MiddleClick_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (int.TryParse(XCoordinateTextBox.Text, out int x) &&
                int.TryParse(YCoordinateTextBox.Text, out int y))
            {
                // Сначала перемещаем курсор в указанные координаты
                if (SetCursorPos(x, y))
                {
                    LogMessage($"Курсор перемещен в позицию: X={x}, Y={y}");

                    // Небольшая задержка для стабилизации позиции
                    var timer = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromMilliseconds(50)
                    };

                    timer.Tick += (s, args) =>
                    {
                        PerformMouseClick(MouseButton.Middle);
                        LogMessage("Выполнен средний клик");
                        timer.Stop();
                    };

                    timer.Start();
                }
                else
                {
                    LogMessage("Ошибка при перемещении курсора");
                }
            }
            else
            {
                LogMessage("Ошибка: Некорректные координаты");
            }
        }
        catch (Exception ex)
        {
            LogMessage($"Ошибка при выполнении среднего клика: {ex.Message}");
        }
    }

    private void PerformMouseClick(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
                break;
            case MouseButton.Right:
                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, UIntPtr.Zero);
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, UIntPtr.Zero);
                break;
            case MouseButton.Middle:
                mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, UIntPtr.Zero);
                mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, UIntPtr.Zero);
                break;
        }
    }

    private void PerformMouseDoubleClick(MouseButton button)
    {
        // Выполняем два клика с небольшой задержкой
        PerformMouseClick(button);

        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };

        timer.Tick += (s, e) =>
        {
            PerformMouseClick(button);
            timer.Stop();
        };

        timer.Start();
    }

    private void LogMessage(string message)
    {
        var timestamp = DateTime.Now.ToString("HH:mm:ss");
        var logEntry = $"[{timestamp}] {message}";

        Dispatcher.Invoke(() =>
        {
            LogTextBox.AppendText(logEntry + Environment.NewLine);
            LogTextBox.ScrollToEnd();
        });
    }
}