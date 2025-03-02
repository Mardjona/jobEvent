using System;
using System.Collections.Generic;
using System.Globalization;

namespace jobEvent.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Mobilephone { get; set; }

    public DateOnly? Birthday { get; set; }
    private static  string[] RussianMonths = new[]
    {
        "января", "февраля", "марта", "апреля", "мая", "июня",
        "июля", "августа", "сентября", "октября", "ноября", "декабря"
    };
    public string FormattedBirthDate => Birthday.HasValue 
        ? $"{Birthday.Value.Day} {RussianMonths[Birthday.Value.Month - 1]}" 
        : "Дата не указана";
   
   
    public int? Position { get; set; }

    public int? Supervisorid { get; set; }

    public int? Assistantid { get; set; }

    public string? Workphone { get; set; }

    public string? Email { get; set; }

    public int? Office { get; set; }

    public virtual ICollection<Absence> AbsenceEmployees { get; set; } = new List<Absence>();

    public virtual ICollection<Absence> AbsenceSubstituteemployees { get; set; } = new List<Absence>();

    public virtual Employee? Assistant { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Employee> InverseAssistant { get; set; } = new List<Employee>();

    public virtual ICollection<Employee> InverseSupervisor { get; set; } = new List<Employee>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual Kabinet? OfficeNavigation { get; set; }

    public virtual Department? PositionNavigation { get; set; }
    public string? GetPosition => PositionNavigation.Name;

    public virtual Employee? Supervisor { get; set; }

    public virtual ICollection<Event> EventsNavigation { get; set; } = new List<Event>();

    public virtual ICollection<Training> Training { get; set; } = new List<Training>();
}
