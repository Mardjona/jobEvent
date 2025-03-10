using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using Avalonia.VisualTree;
using jobEvent.Models;
using Microsoft.EntityFrameworkCore;

namespace jobEvent;

public partial class MainWindow : Window
{
    public List<NewsItem>? NewsItems { get; set; }
    List<DateTime> listDate = new List<DateTime>()
    {
            new DateTime(2025, 03, 04),
            new DateTime(2025, 03, 11),
            new DateTime(2025, 03, 14),
            new DateTime(2025, 04, 5),
            new DateTime(2025, 04, 19),
            new DateTime(2025, 06, 8),
            new DateTime(2025, 06, 01),
     };

    List<EventItems> events = new List<EventItems>()
    {
        new EventItems(0,"Общее совещание в актовом зале", "Все сотрудники отдела <Администраторы> собираемся", "Петров", "26.05.2024" ),
        new EventItems(1,"Общее совещание в актовом зале", "Все сотрудники отдела <Администраторы> собираемся", "Петров", "26.05.2024" ),
        new EventItems(2,"Общее совещание в актовом зале", "Все сотрудники отдела <Администраторы> собираемся", "Петров", "26.05.2024" ),
        new EventItems(3,"Общее совещание в актовом зале", "Все сотрудники отдела <Администраторы> собираемся", "Петров", "26.05.2024" ),
        new EventItems(3,"Общее совещание в актовом зале", "Все сотрудники отдела <Администраторы> собираемся", "Петров", "26.05.2024" ),
        new EventItems(3,"Общее совещание в актовом зале", "Все сотрудники отдела <Администраторы> собираемся", "Петров", "26.05.2024" ),
      
    };

    public MainWindow()
    {
        InitializeComponent();
        EmployeeJobLoad();
        LoadNewsData();
        var today = DateTime.Today;
        DataContext = this;
        MainCalendar.Loaded += OnCalendarLoaded;
        MainCalendar.DisplayDateChanged += CustomCalendar_DisplayDateChanged;
    }
    private void CustomCalendar_DisplayDateChanged(object? sender, CalendarDateChangedEventArgs e)
    {
        BrushesCalendar();
    }


    private void BrushesCalendar()
    {
        foreach (var child in MainCalendar.GetVisualDescendants())
        {
            if (child is CalendarDayButton dayButton)
            {
                var dateNow = (MainCalendar as Calendar).DisplayDate;

                string vv = dayButton.Content!.ToString()!;

                try
                {
                    if (listDate.Contains(new DateTime(dateNow.Year, dateNow.Month, int.Parse(vv))))
                    {
                        dayButton.Background = Brushes.LightYellow;
                        dayButton.Foreground = Brushes.Red;
                        ToolTip.SetTip(dayButton, "i love you");
                    }
                    else
                    {
                        dayButton.Background = Brushes.LightGray;
                        dayButton.Foreground = Brushes.Black;
                    }
                }
                catch
                {
                    dayButton.Background = Brushes.LightGray;
                    dayButton.Foreground = Brushes.Black;
                }
            }
        }
    }

    private void OnCalendarLoaded(object sender, EventArgs e)
    {
        BrushesCalendar();
    }
    private void EmployeeJobLoad()
    {
        List<Employee> employees = Helper.DataBase.Employees.Include(x => x.PositionNavigation).ToList();

        if (SourseTextBox == null) return;

        if (!string.IsNullOrEmpty(SourseTextBox.Text))
        {
            employees = employees.Where(x => x.Fullname.ToLower().Contains(SourseTextBox.Text.ToLower()) || x.Workphone.Contains(SourseTextBox.Text) || x.GetPosition.ToLower().Contains(SourseTextBox.Text.ToLower())) .ToList();
        }
        else { employees = employees.ToList(); }

        employeeDesk.ItemsSource = employees;
        //ListBox_imp.ItemsSource = NewsItems;
        EventListBox.ItemsSource = events;

    }
    private void LoadNewsData()
    {
        try
        {
            // Путь к JSON-файлу
            var jsonFilePath = ("./Assets/news_response.json");
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("Файл news_response.json не найден.");
                return;
            }
            // Чтение JSON-файла
            var json = File.ReadAllText(jsonFilePath);

            // Десериализация JSON в список объектов NewsItem
            NewsItems = JsonSerializer.Deserialize<List<NewsItem>>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
        }
        ListBox_imp.ItemsSource = NewsItems;
    }
    public void TextBox_TextChanged(object? sender, TextChangedEventArgs e) => EmployeeJobLoad();


}















