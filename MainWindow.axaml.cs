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

    public MainWindow()
    {
        InitializeComponent();
        EmployeeJobLoad();
        LoadNewsData();
        //DataContext = this;
      



    }

    private void EmployeeJobLoad()
    {
        List<Employee> employees = Helper.DataBase.Employees.Include(x => x.PositionNavigation).ToList();
        
       
        
      
        employeeDesk.ItemsSource = employees;
      //  ListBox_imp.ItemsSource = NewsItems;

        
    }
    private void LoadNewsData()
    {
        try
        {
            // Путь к JSON-файлу
            var jsonFilePath =("./Assets/news_response.json");
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
}






    public class Holiday
    {
        public DateTime Date { get; set; }
        public bool IsDayOff { get; set; } // true - выходной, false - рабочий

    }

    
    
    
